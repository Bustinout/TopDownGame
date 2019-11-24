using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    [Header("Item Info")]
    public string itemName;
    public string itemDescription;
    public string spriteLocation;
    public string flavorText;
    public int rarity;
    // 1 being least rare, 3 being most

    public int count = 1;

    [Header("Stats")]
    public int primaryDamage = 0;
    public float primartyAtkSped = 0;
    public int secondaryDamage = 0;
    public float secondaryCD = 0;
    public int hpUP = 0;
    public int hpDown = 0;
    public float critChance = 0;

    //[Header("Special Stats")]
    //special skills (ints indicate level of special skill)
    //public int whatnot = 0;




}
