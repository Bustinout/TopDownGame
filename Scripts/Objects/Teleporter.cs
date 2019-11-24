using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : Interactable
{
    public bool bossPortal;
    public GameObject usedSFX;
    public override void use()
    {
        Instantiate(usedSFX);
        if (bossPortal)
        {
            GetComponentInParent<RoomManager>().bossPortal();
        }
        else
        {
            GetComponentInParent<RoomManager>().teleport();
        }
    }
    
}
