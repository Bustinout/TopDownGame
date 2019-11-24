using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;


public class Status : MonoBehaviour
{

    //for player only

    public bool teleporting;
    public bool dashing;
    public bool stunned;
    public bool gamePaused;
    public float stunTimer;

    public GameObject hitEffect; //spawned on damage

    public string[] componentsToDisable = new string[] { "TopDownController2D", "Character", "CharacterOrientation2D", "CharacterMovement" };

    public void spawnHitEffect()
    {
        if (hitEffect != null)
        {
            Instantiate(hitEffect, this.transform.position, Quaternion.identity, StaticManager.SpecialEffectsParent.transform);
        }
    }

    public void stun(bool x)
    {
        if (x)
        {
            stunned = true;
            GetComponent<CharacterMovement>().MovementForbidden = true;
            StaticManager.stunbar.bar.SetActive(true);
        }
        else
        {
            stunned = false;
            GetComponent<CharacterMovement>().MovementForbidden = false;
            StaticManager.stunbar.bar.SetActive(false);
        }
    }

    public bool checkMovable()
    {
        if (teleporting || dashing || stunned || gamePaused)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    public bool checkDetectable() //by AI Brain
    {
        if (teleporting) //or stealth
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private GameObject[] reticles;
    public void enableReticle(bool x)
    {
        reticles = GameObject.FindGameObjectsWithTag("Reticle");
        foreach (GameObject go in reticles)
        {
            go.GetComponentInChildren<SpriteRenderer>().enabled = x;
        }
    }

}
