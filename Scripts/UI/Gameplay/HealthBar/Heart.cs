using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.TopDownEngine;

public class Heart : MonoBehaviour
{
    public Image image;
    public int heartIndex; //starts at 1, not 0

    public void refresh()
    {
        if (heartIndex > StaticManager.player.GetComponent<Health>().CurrentHealth)
        {
            image.overrideSprite = Resources.Load<Sprite>("Sprites/UI/heart2");
        }
        else
        {
            image.overrideSprite = Resources.Load<Sprite>("Sprites/UI/heart1");
        }
    }
}
