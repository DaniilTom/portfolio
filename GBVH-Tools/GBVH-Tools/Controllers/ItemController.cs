using System;
using IO = System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseWrapper;
using Items;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System.Reflection;
using GBVH_Tools.Models;
using GBVH_Tools.Common.Attributes;
using System.Collections;
using GBVH_Tools.DAL;

namespace GBVH_Tools.Controllers
{
    public class ItemController : Controller
    {


        private readonly string root = @"wwwroot/tempDB/";
        private readonly string patch = @"wwwroot/patchDB/";

        private List<ItemDto> _items;
        private List<ItemLocale> _locales;

        /// <summary>
        /// Указывает, есть ли во временных файлах какие-либо предметы.
        /// </summary>
        private bool HasChanges;

        /// <summary>
        /// Свободное временное Id.
        /// </summary>
        private int _tempId;

        /// <summary>
        /// Словарь, который будет преобразован в bootstrap классы для раскраски таблицы.
        /// </summary>
        private readonly Dictionary<ItemDto, Status> _status;

        public ItemController() : base()
        {
            ItemTemplateRepository.Init();

            _status = new Dictionary<ItemDto, Status>();

            _items = ItemTemplateRepository.GetAll().ToList();
            _locales = ItemTemplateRepository.GetItemLocales().ToList();

            // для предметов из базы устанавливаем статус Nothing
            foreach (var item in _items)
                _status.Add(item, Status.Nothing);
            
            // т.к. файлы могут быть еще не созданы, возможны исключения 
            // FileNotFoundException. Создадим эти файлы.
            try
            {
                var Create = IO.File.ReadAllText(root + "Create.json");
                if (!String.IsNullOrEmpty(Create))
                {
                    List<ItemDto> itemsCreate = JsonConvert.DeserializeObject<List<ItemDto>>(Create);
                    if(itemsCreate.Count > 0)
                        HasChanges = true;

                    // прикрепляем предметы на создание
                    _items.AddRange(itemsCreate);
                    // обновляем статусы
                    foreach (var item in itemsCreate)
                        _status.Add(item, Status.Create);
                }
            }
            catch(FileNotFoundException)
            {
                // [] при десериализации будет создавать пустой список, а не null (мне так удобней)
                byte[] emptyJSON = Encoding.ASCII.GetBytes("[]"); // пустой JSON файл
                var stream = IO.File.Create(root + "Create" + ".json");
                stream.Write(emptyJSON, 0, emptyJSON.Length);
                stream.Dispose();
            }

            try
            {
                var Update = IO.File.ReadAllText(root + "Update.json");
                if (!String.IsNullOrEmpty(Update))
                {
                    List<ItemDto> itemsUpdate = JsonConvert.DeserializeObject<List<ItemDto>>(Update);
                    if (itemsUpdate.Count > 0)
                        HasChanges = true;

                    // замена предметов на локально-измененные
                    foreach (var item in itemsUpdate)
                    {
                        int index = _items.IndexOf(_items.FirstOrDefault(i => i.Id == item.Id));
                        _items[index] = item;
                        _status.Add(item, Status.Update);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                byte[] emptyJSON = Encoding.ASCII.GetBytes("[]"); // пустой JSON файл
                var stream = IO.File.Create(root + "Update" + ".json");
                stream.Write(emptyJSON, 0, emptyJSON.Length);
                stream.Dispose();
            }

            try
            { 
                var Delete = IO.File.ReadAllText(root + "Delete.json");
                if (!String.IsNullOrEmpty(Delete))
                {
                    List<ItemDto> itemsDelete = JsonConvert.DeserializeObject<List<ItemDto>>(Delete);
                    if (itemsDelete.Count > 0)
                        HasChanges = true;

                    // помечаем предметы на удаление
                    foreach (var item in itemsDelete)
                    {
                        int index = _items.IndexOf(_items.FirstOrDefault(i => i.Id == item.Id));
                        _items[index] = item;
                        _status.Add(item, Status.Delete);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                byte[] emptyJSON = Encoding.ASCII.GetBytes("[]"); // пустой JSON файл
                var stream = IO.File.Create(root + "Delete" + ".json");
                stream.Write(emptyJSON, 0, emptyJSON.Length);
                stream.Dispose();
            }

            // рассчет первого свободного Id предмета в БД
            int max = 1;
            foreach (var item in _items)
                if (item.Id > max) max = item.Id;
            _tempId = max + 1;
        }
        // GET: Item
        public ActionResult Index()
        {
            ViewData["Title"] = "Items List";
            ViewData["HasChanges"] = HasChanges;
            ViewData["Status"] = _status.ToDictionary(kvp => kvp.Key,
                                                        kvp => 
                                                        {
                                                            if (kvp.Value == Status.Nothing)
                                                                return "light";
                                                            else if (kvp.Value == Status.Create)
                                                                return "success";
                                                            else if (kvp.Value == Status.Update)
                                                                return "warning";
                                                            else
                                                                return "danger";
                                                        });
            return View(_items);
        }

        // GET: Item/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Item/Create
        public ActionResult Create()
        {
            ItemViewModel ivm = new ItemViewModel(_tempId);
            // для Create и Edit используется одна разметка (но есть небольшие отличия)
            ViewData["IsCreate"] = true;
            return View("Edit", ivm);
        }

        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemViewModel ivm)
        {
            try
            { 
                var iDto = ivm.GetItemDto();

                CreateItem(iDto);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Edit/5
        public ActionResult Edit(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            ItemViewModel ivm = new ItemViewModel(item);
            ViewData["IsCreate"] = false;
            return View(ivm);
        }

        // POST: Item/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ItemViewModel ivm)
        {
            try
            {
                UpdateItem(ivm.GetItemDto());

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Delete/5
        public ActionResult Delete(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            DeleteItem(item);
            return RedirectToAction(nameof(Index));
        }

        // POST: Item/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add Delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Создает SQL-файлы на основе временных файлов.
        /// </summary>
        /// <returns></returns>
        public ActionResult PushChanges()
        {
            string createJSON = IO.File.ReadAllText(root + "Create.json");
            List<ItemDto> listItems = JsonConvert.DeserializeObject<List<ItemDto>>(createJSON);
            if(listItems.Count > 0)
            {
                // получаем последний ид в базе локалей
                var id = Int32.Parse(DatabaseWrapper.DatabaseWrapper.ExecuteQueryWithAnswer(@"select max(Id) from 'item_template_locale_ru';"));
                id++;
                var listLocales = listItems.Select(i =>
                    new ItemLocale
                    {
                        Id = id++,
                        ItemId = i.Id,
                        Title = i.Name,
                        FlavorText = i.FlavorText
                    }).AsEnumerable(); // выполнение запроса сразу, т.к. захват переменной id

                string createResult = SQLStatementGenerator.GenCreate(listItems);
                string localesResult = SQLStatementGenerator.GenCreate(listLocales, "item_template_locale_ru");

                string date = DateTime.Now.ToString("yyyyMMdd-HHmm");
                string patchName = date + "_world_";

                IO.File.WriteAllText(patch + patchName + "item_template" + "_data" + "_ins.sql", createResult);
                IO.File.WriteAllText(patch + patchName + "item_template_locale_ru" + "_data" + "_ins.sql", localesResult);
            }

            string updateJSON = IO.File.ReadAllText(root + "Update.json");
            listItems = JsonConvert.DeserializeObject<List<ItemDto>>(updateJSON);
            if (listItems.Count > 0)
            {
                IEnumerable<ItemLocale> listLocales = listItems.Select(i =>
                                                        new ItemLocale {
                                                            ItemId = i.Id,
                                                            Title = i.Name,
                                                            FlavorText = i.FlavorText
                                                        });

                string updateResult = SQLStatementGenerator.GenUpdate(listItems);
                string localesResult = SQLStatementGenerator.GenUpdate(listLocales, "item_template_locale_ru");

                string date = DateTime.Now.ToString("yyyyMMdd-HHmm");
                string patchName = date + "_world_";

                IO.File.WriteAllText(patch + patchName + "item_template" + "_data" + "_upd.sql", updateResult);
                IO.File.WriteAllText(patch + patchName + "item_template_locale_ru" + "_data" + "_upd.sql", localesResult);
            }

            string deleteJSON = IO.File.ReadAllText(root + "Delete.json");
            listItems = JsonConvert.DeserializeObject<List<ItemDto>>(deleteJSON);
            if (listItems.Count > 0)
            {
                
                string deleteResult = SQLStatementGenerator.GenDelete(listItems);

                string date = DateTime.Now.ToString("yyyyMMdd-HHmm");
                string patchName = date + "_world_";

                IO.File.WriteAllText(patch + patchName + "item_template" + "_del.sql", deleteResult);
            }

            try { IO.File.Delete(root + "Create.json"); } catch { }
            try { IO.File.Delete(root + "Update.json"); } catch { }
            try { IO.File.Delete(root + "Delete.json"); } catch { }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Добавление нового предмета.
        /// </summary>
        /// <param name="newItem"></param>
        private void CreateItem(ItemDto newItem)
        {
            AddItemToFile("Create", newItem);
        }

        /// <summary>
        /// Обновление предмета.
        /// </summary>
        /// <param name="newItem"></param>
        private void UpdateItem(ItemDto newItem)
        {
            // поиск старой версии предмета
            var oldItem = _items.FirstOrDefault(i => i.Id == newItem.Id);
            _status.TryGetValue(oldItem, out Status status);

            if(status == Status.Create)
            {
                RemoveItemFromFile("Create", oldItem);
                AddItemToFile("Create", newItem);
                return;
            }

            if(status == Status.Update || status == Status.Delete)
            {
                RemoveItemFromFile(status.ToString(), oldItem);
                AddItemToFile("Update", newItem);
                return;
            }

            if(status == Status.Nothing)
            {
                AddItemToFile("Update", newItem);
            }
        }

        /// <summary>
        /// Удаление предмета.
        /// </summary>
        /// <param name="item"></param>
        private void DeleteItem(ItemDto item)
        {

            _status.TryGetValue(item, out Status status);

            if(status == Status.Nothing)
            {
                AddItemToFile("Delete", item);
                return;
            }

            if (status == Status.Create)
            {
                RemoveItemFromFile("Create", item);
                return;
            }

            if (status == Status.Update)
            {
                RemoveItemFromFile("Update", item);
                AddItemToFile("Delete", item);
                return;
            }

            if (status == Status.Delete)
                return;
        }

        /// <summary>
        /// Удаление предмета из определенного файла
        /// </summary>
        /// <param name="fileName">Имя файла (без расширения)</param>
        /// <param name="item">Предмет</param>
        private void RemoveItemFromFile(string fileName, ItemDto item)
        {
            string resultFromOldFile = IO.File.ReadAllText(root + fileName + ".json");
            List<ItemDto> list = JsonConvert.DeserializeObject<List<ItemDto>>(resultFromOldFile);

            if (list is null)
                return;

            list.Remove(list.FirstOrDefault(i => i.Id == item.Id));
            IO.File.WriteAllText(root + _status[item] + ".json",
                JsonConvert.SerializeObject(list, Formatting.Indented));
        }

        /// <summary>
        /// Добавление предмета в файл.
        /// </summary>
        /// <param name="fileName">Имя файла (без расширения)</param>
        /// <param name="item">Предмет</param>
        private void AddItemToFile(string fileName, ItemDto item)
        {
            string resultFromNewFile = IO.File.ReadAllText(root + fileName + ".json");
            List<ItemDto> list = JsonConvert.DeserializeObject<List<ItemDto>>(resultFromNewFile);

            if (list is null)
                list = new List<ItemDto>();

            list.Add(item);
            IO.File.WriteAllText(root + fileName + ".json",
                JsonConvert.SerializeObject(list,Formatting.Indented));
        }

        /// <summary>
        /// Определяет состояние предмета. Так же используется для раскраски
        /// таблицы с помощью bootstrap-table стилей
        /// </summary>
        private enum Status
        {
            Nothing = 1,
            Create,
            Update,
            Delete
        }
    }
}