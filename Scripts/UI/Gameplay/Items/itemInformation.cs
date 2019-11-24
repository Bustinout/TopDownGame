using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemInformation
{
    public string itemName;
    public string itemDescription;
    public string itemSprite;

    public string flavorText;

    public itemInformation(string x, string y, string z)
    {
        itemName = x;
        itemDescription = y;
        itemSprite = z;
    }

    public itemInformation(string x, string y, string z, string f)
    {
        itemName = x;
        itemDescription = y;
        itemSprite = z;
        flavorText = f;
    }
}
