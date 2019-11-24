using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

public class Dash : MonoBehaviour
{
    //https://www.youtube.com/watch?v=w4YV8s9Wi3w

    private float dashDistance;
    private float dashTime;
    private float dashTimeCounter = 0;
    public int inputButton;

    private float cooldown;
    private float cooldownTimer;

    private Vector3 dashStartLocation;
    private Vector3 dashDestination;
    private Vector3 dashDirection;
    private Vector3 newPosition;
    private TopDownController _controller;

    public GameObject startEffect;
    public GameObject endEffect;


    private void Start()
    {
        _controller = GetComponent<TopDownController>();
        setWeapon();
    }

    public void setWeapon()
    {
        if (SaveLoad.current.currentHero.equippedWeapons[2] == 0) //default dash move
        {
            dashDistance = 5f;
            dashTime = 0.2f;
            cooldown = 1f;

            //set start and ned effect
        }
    }

    public void startDash()
    {
        GetComponent<Status>().dashing = true;
        cooldownTimer = cooldown;
        dashTimeCounter = 0;
        _controller.FreeMovement = false;
        dashStartLocation = this.transform.position;
        dashDestination = this.transform.position + _controller.CurrentDirection.normalized * dashDistance;
        if (startEffect != null)
        {
            Instantiate(startEffect, this.transform.position, Quaternion.identity, StaticManager.SpecialEffectsParent.transform); //spawn particle system
        }
        StartCoroutine("dash");
        StartCoroutine("cooldownCountdown");
    }

    private void Update()
    {
        if (Input.GetKeyDown(SaveLoad.current.pcInput[2]))
        {
            if (cooldownTimer == 0)
            {
                if (GetComponent<Status>().checkMovable())
                {
                    startDash();
                }
            }
            else
            {
                //on cd
            }
        }
    }

    IEnumerator cooldownCountdown()
    {
        while (cooldownTimer >= 0)
        {
            cooldownTimer -= Time.deltaTime;
            StaticManager.spellsPanel.abilities[inputButton].updateRadial(cooldownTimer, cooldown);
            yield return new WaitForEndOfFrame();
        }
        cooldownTimer = 0;
    }

    IEnumerator dash()
    {
        while (dashTimeCounter < dashTime)
        {
            dashTimeCounter += Time.deltaTime;
            newPosition = Vector3.Lerp(dashStartLocation, dashDestination, (dashTimeCounter / dashTime));
            _controller.MovePosition(newPosition);
            yield return new WaitForEndOfFrame();
        }
        _controller.FreeMovement = true;
        if (endEffect != null)
        {
            Instantiate(endEffect, this.transform.position, Quaternion.identity, StaticManager.SpecialEffectsParent.transform); //spawn particle system
        }
        GetComponent<Status>().dashing = false;
    }

    public void dashIntoPortal()
    {
        StopCoroutine("dash");
        _controller.FreeMovement = true;
        GetComponent<Status>().dashing = false;
    }

}
