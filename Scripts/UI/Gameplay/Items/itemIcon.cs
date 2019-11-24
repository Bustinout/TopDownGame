using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemIcon : MonoBehaviour
{
    public int index;
    public GameObject tooltip;
    public Text itemNameText;
    public Text itemDescriptionText;
    public Text itemCount;

    public Image itemSprite;
    
    private bool empty = true;

    private void Start()
    {
        tooltip.SetActive(false);
    }

    public void showToolTip()
    {
        if (!empty)
        {
            tooltip.SetActive(true);
        }
    }
    public void hideToolTip()
    {
        if (!empty)
        {
            tooltip.SetActive(false);
        }
    }

    public void refresh()
    {
        if (index < SaveLoad.current.currentHero.inventory.Count)
        {
            itemNameText.text = SaveLoad.current.currentHero.inventory[index].itemName + " x " + SaveLoad.current.currentHero.inventory[index].count;
            itemDescriptionText.text = SaveLoad.current.currentHero.inventory[index].itemDescription;
            itemCount.text = SaveLoad.current.currentHero.inventory[index].count + "";
            itemSprite.overrideSprite = Resources.Load<Sprite>(SaveLoad.current.currentHero.inventory[index].spriteLocation);
            empty = false;
        }
    }
}
