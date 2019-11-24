using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryDisplay : MonoBehaviour
{
    public GameObject display;
    public GameObject grid;
    public GameObject itemIconPrefab;
    private itemIcon [] icons;

    void Start()
    {
        StaticManager.inventorydisplay = this;
        initializeItemIcons();
        display.SetActive(false);
    }

    public void initializeItemIcons()
    {
        for (int x = 0; x < 16; x++)
        {
            var go = Instantiate(itemIconPrefab, grid.transform);
            go.GetComponent<itemIcon>().index = x;
        }
        icons = GetComponentsInChildren<itemIcon>();
        refreshItems();
    }

    public void refreshItems()
    {
        foreach (itemIcon x in icons)
        {
            x.refresh();
        }
    }

    public void showInventory()
    {
        StaticManager.player.GetComponent<Status>().enableReticle(false);
        StaticManager.customcursor.gameObject.SetActive(true);
        StaticManager.cursorMode = true;

        display.SetActive(true);
    }

    public void hideInventory()
    {
        StaticManager.player.GetComponent<Status>().enableReticle(true);
        StaticManager.customcursor.gameObject.SetActive(false);
        StaticManager.cursorMode = false;


        foreach (GameObject x in GameObject.FindGameObjectsWithTag("HideToolTipWhenInventoryClosed"))
        {
            x.SetActive(false);
        }

        display.SetActive(false);
    }
}
