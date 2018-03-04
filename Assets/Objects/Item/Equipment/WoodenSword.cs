using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenSword : IItem, IWeapon {
    private float _attack;

    private float _buyPrice;
    private float _sellPrice;
    private ItemRarity _rarity;
    private int _minimumLevelToUse;

    public float attack {
        get { return _attack; }
        set { _attack = value; }
    }

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

    public WoodenSword() {
        attack = 50;

        buyPrice = 500;
        sellPrice = 250;
        rarity = ItemRarity.Common;
        minimumLevelToUse = 1;
    }
}
