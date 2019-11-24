using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkItems : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(SaveLoad.current.pcInput[7]))
        {
            if (!GetComponent<Status>().gamePaused)
            {
                StaticManager.inventorydisplay.showInventory();
            }
        }
        if (Input.GetKeyUp(SaveLoad.current.pcInput[7]))
        {
            StaticManager.inventorydisplay.hideInventory();
        }
    }




}
