using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Implementations;

namespace WebStore.Data
{
    public static class TestData
    {
        static TestData()
        {
            MCDescriptions = new List<MCDescription>
            {
                new MCDescription{Id = 1, ProductId = 1, DetailedDesriptionList = str1 },
                new MCDescription{Id = 2, ProductId = 2, DetailedDesriptionList = str2 },
                new MCDescription{Id = 3, ProductId = 3, DetailedDesriptionList = str3 }
            };

            Products = new List<ProductBase>
            {
                new ProductBase{ Id=1, Name="Intel 4004", ImageUrl="/img/intel4004.jpg", Price = 100, CategoryId = 1},
                new ProductBase{ Id=2, Name="Intel 80186", ImageUrl="/img/intel80186.jpg", Price = 150, CategoryId = 1},
                new ProductBase{ Id=3, Name="Intel 8086", ImageUrl="/img/intel8086.jpg", Price = 200, CategoryId = 1}
            };

            Categories = new List<Category>
            {
                new Category{ Id = 1, Name = "Microcontroller", TotalProductsCount = Products.Count },
                new Category{ Id = 2, Name = "RAM", TotalProductsCount = 0 },
                new Category{ Id = 3, Name = "Transistors", TotalProductsCount = 0 }
            };
        }


        public static List<MCDescription> MCDescriptions;
        public static List<ProductBase> Products;
        public static List<Category> Categories;

        static string[] str1 =
            {
                "Тактовая частота: от 500 до 740 кГц",
                "Разрядность шины данных: 4 бита",
                "Разрядность шины адреса: 12 бит",
                "Объём адресуемой памяти: 640 байт",
                "Память команд (ПЗУ/ROM): 4 килобайта",
                "Объём адресуемой памяти (ОЗУ/RAM): 640 байт",
                "Количество транзисторов: 2300",
                "Напряжение питания: −15 В (pMOS)",
                "Количество инструкций: 46"
            };

        static string[] str2 = 
            {
                "Тактовая частота: от 6 до 25 МГц",
                "Разрядность шины данных: 16 бит",
                "Разрядность шины адреса: 20 бит",
                "Объём адресуемой памяти: 1 Мбайт",
                "Память команд (ПЗУ/ROM): ?",
                "Объём адресуемой памяти (ОЗУ/RAM): ?",
                "Количество транзисторов: 134 000",
                "Напряжение питания: 2,9-3,3 В",
                "Количество инструкций: ?"
            };

        static string[] str3 = 
            {
                "Тактовая частота: от 4 до 16 МГц",
                "Разрядность шины данных: 16 бит",
                "Разрядность шины адреса: 20 бит",
                "Объём адресуемой памяти: 1 Мбайт",
                "Адресное пространство I/O: 64 Кбайт",
                "Количество транзисторов: 29 000",
                "Потребление: 0,65 Вт",
                "Напряжение питания: +5 В",
                "Поддерживаемые технологии: 98 инструкций"
            };
    }
}
