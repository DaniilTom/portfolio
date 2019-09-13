using Character;
using Common;
using GBVH_Tools.Common.Attributes;
using System.Collections.Generic;

namespace Items
{
    /// <summary>
    /// Шаблон предмета. этот класс используется для передачи данных
    /// </summary>
    public class ItemDto
    {
        [DBPrimaryKey, DBTargetKey]
        public int Id { get; set; }
        public ItemClasses Class { get; set; }
        public int ModelId { get; set; }
        public int IconId { get; set; }
        public ItemQuality Quality { get; set; }
        public ItemFlags Flags { get; set; }
        public int BuyPrice { get; set; }
        public int SellPrice { get; set; }
        public InventorySlots InventorySlot { get; set; }
        public int ItemLevel { get; set; }
        public int RequiredLevel { get; set; }
        public int MaxCount { get; set; }
        public float Weight { get; set; }
        public float AdditionalWeight { get; set; }

        [DBDictionary("RequiredStatType", "RequiredStatValue", 3)]
        public Dictionary<Stats, int> RequiredStats { get; set; } = new Dictionary<Stats, int>(3);

        [DBDictionary("StatType", "StatValue", 3)]
        public Dictionary<Stats, int> BonusStats { get; set; } = new Dictionary<Stats, int>(6);
        public int DmgMin1 { get; set; }
        public int DmgMax1 { get; set; }
        public DamageTypes DmgType1 { get; set; }
        public int DmgMin2 { get; set; }
        public int DmgMax2 { get; set; }
        public DamageTypes DmgType2 { get; set; }
        public int Armor { get; set; }
        public int Delay { get; set; }
        public ItemClasses AmmoType { get; set; }

        public float Range { get; set; }

        //TODO: Spells & Effects
        public int PageId { get; set; }
        public int StartQuest { get; set; }
        public SheathTypes Sheath { get; set; }
        public int LockId { get; set; }
        public float BaseBlockChance { get; set; }
        public int BaseBlockValue { get; set; }
        public int ItemSet { get; set; }
        public int MaxDurability { get; set; }

        [DBList("ZoneId", 3)]
        public List<int> UsableInZones { get; set; } = new List<int>();
        public float MaxDuration { get; set; }
        public string ScriptName { get; set; }
        public int MaxStack { get; set; }

        [DBNotInclude]
        public string Name { get; set; }
        [DBNotInclude]
        public string FlavorText { get; set; }

    }
}