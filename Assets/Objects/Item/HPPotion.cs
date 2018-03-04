using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPotion : IItem {
    private float _buyPrice;
    private float _sellPrice;
    private ItemRarity _rarity;
    private int _minimumLevelToUse;

    public float buyPrice {
        get { return _buyPrice; }
        set { _buyPrice = value; }
    }

    public float sellPrice {
        get { return _sellPrice; }
        set { _sellPrice = value; }
    }

    public ItemRarity rarity {
        get { return _rarity; }
        set { _rarity = value; }
    }

    public int minimumLevelToUse {
        get { return _minimumLevelToUse; }
        set { _minimumLevelToUse = value; }
    }
    
    public HPPotion() {
        buyPrice = 100;
        sellPrice = 50;
        rarity = ItemRarity.Common;
        minimumLevelToUse = 1;
    }
}
