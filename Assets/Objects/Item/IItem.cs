using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemRarity { 
    Common, Uncommon, Rare, Epic // game designer to decide
}

// such as potions, raw items, equips, etc
// should save inside database instead of populated by code
public interface IItem {
    float buyPrice { get; set; }
    float sellPrice { get; set; }
    ItemRarity rarity { get; set; }
    int minimumLevelToUse { get; set; }
    //List<SpawnableObject> dropSources { get; set; }
}
