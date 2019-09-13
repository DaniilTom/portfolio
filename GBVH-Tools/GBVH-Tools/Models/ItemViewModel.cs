using Common;
using Items;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GBVH_Tools.Models
{
    /// <summary>
    /// DTO-объект отображения предмета.
    /// </summary>
    public class ItemViewModel
    {
        /// <summary>
        /// НЕ! содержит элементы коллекции. Для получения полного ItemDto используте метод <see cref="GetItemDto()"/>
        /// </summary>
        public ItemDto TempItemDto { get; set; }
        public List<KeyValuePair<Character.Stats, int>> RequiredStats { get; set; }
        public List<KeyValuePair<Character.Stats, int>> BonusStats { get; set; }
        public List<KeyValuePair<ItemFlags, bool>> Flags { get; set; }
        public List<KeyValuePair<DamageTypes, bool>> DmgType1 { get; set; }
        public List<KeyValuePair<DamageTypes, bool>> DmgType2 { get; set; }
        public List<int> UsableInZones { get; set; }

        public ItemViewModel(ItemDto item)
        {
            TempItemDto = item;

            RequiredStats = new List<KeyValuePair<Character.Stats, int>>();
            BonusStats = new List<KeyValuePair<Character.Stats, int>>();
            Flags = new List<KeyValuePair<ItemFlags, bool>>();
            DmgType1 = new List<KeyValuePair<DamageTypes, bool>>();
            DmgType2 = new List<KeyValuePair<DamageTypes, bool>>();
            UsableInZones = new List<int>();

            var reqList = TempItemDto.RequiredStats.ToList();
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    RequiredStats.Add(reqList[i]);
                }
                catch
                {
                    RequiredStats.Add(new KeyValuePair<Character.Stats, int>());
                }
            }

            var bonList = TempItemDto.BonusStats.ToList();
            for (int i = 0; i < 6; i++)
            {
                try
                {
                    BonusStats.Add(bonList[i]);
                }
                catch
                {
                    BonusStats.Add(new KeyValuePair<Character.Stats, int>());
                }
            }

            var usableInZones = TempItemDto.UsableInZones;
            for(int i = 0; i < 3; i++)
            {
                try
                {
                    UsableInZones.Add(usableInZones[i]);
                }
                catch
                {
                    UsableInZones.Add(new int());
                }
            }

            foreach (var flag in (ItemFlags[])Enum.GetValues(typeof(ItemFlags)))
            {
                if ((TempItemDto.Flags & flag) != 0)
                    Flags.Add(new KeyValuePair<ItemFlags, bool>(flag, true));
                else
                    Flags.Add(new KeyValuePair<ItemFlags, bool>(flag, false));
            }

            foreach (var dmgType in (DamageTypes[])Enum.GetValues(typeof(DamageTypes)))
            {
                if ((TempItemDto.DmgType1 & dmgType) != 0)
                    DmgType1.Add(new KeyValuePair<DamageTypes, bool>(dmgType, true));
                else
                    DmgType1.Add(new KeyValuePair<DamageTypes, bool>(dmgType, false));

                if ((TempItemDto.DmgType2 & dmgType) != 0)
                    DmgType2.Add(new KeyValuePair<DamageTypes, bool>(dmgType, true));
                else
                    DmgType2.Add(new KeyValuePair<DamageTypes, bool>(dmgType, false));
            }
        }

        public ItemViewModel() : this(new ItemDto()) {}

        public ItemViewModel(int id) : this(new ItemDto { Id = id }) { }

        /// <summary>
        /// Метод получения полного <see cref="ItemDto"/>
        /// </summary>
        /// <returns></returns>
        public ItemDto GetItemDto()
        {
            TempItemDto.RequiredStats = RequiredStats.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            TempItemDto.BonusStats = BonusStats.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            TempItemDto.UsableInZones = UsableInZones;

            ItemFlags flags = ItemFlags.None;
            foreach(var flag in Flags)
            {
                if (flag.Value)
                    flags |= flag.Key;
            }
            TempItemDto.Flags = flags;

            DamageTypes damage1 = DamageTypes.None;
            foreach (var type in DmgType1)
            {
                if (type.Value)
                    damage1 |= type.Key;
            }
            TempItemDto.DmgType1 = damage1;

            DamageTypes damage2 = DamageTypes.None;
            foreach (var type in DmgType2)
            {
                if (type.Value)
                    damage2 |= type.Key;
            }
            TempItemDto.DmgType2 = damage2;

            return TempItemDto;
        }
    }
}
