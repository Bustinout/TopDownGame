using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class PlayerSpawnAnimation : MonoBehaviour
{
    public Transform quadTransform;
    private Vector2 originalQuadScale;

    public Transform shadowTransform;
    private Vector2 originalShadowScale;

    public Transform playerTransform;

    public Transform[] weaponAttachments;

    private void Start()
    {
    }

    private void OnEnable()
    {
        GetComponent<Status>().teleporting = true;
        disableStuff(true);
        hideModel(true);
        startAnimation();
        
    }

    public void hideModel(bool x)
    {
        if (x)
        {
            shadowTransform.gameObject.SetActive(false);

            SpriteRenderer[] temp = playerTransform.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in temp)
            {
                sr.enabled = false;
            }
        }
        else
        {
            shadowTransform.gameObject.SetActive(true);

            SpriteRenderer[] temp = playerTransform.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in temp)
            {
                sr.enabled = true;
            }
        }
    }

    
    public bool checkToDisable(string x)
    {
        foreach (string y in componentsToDisable)
        {
            if (y == x)
            {
                return true;
            }
        }
        return false;
    }
    public string[] componentsToDisable = new string[] { "TopDownController2D", "Character", "CharacterOrientation2D", "CharacterMovement" };

    public void disableStuff(bool x)
    {
        if (x)
        {
            foreach (MonoBehaviour s in GetComponents<MonoBehaviour>())
            {
                if (checkToDisable(s.GetType().Name))
                {
                    //Debug.Log(s.GetType().Name);
                    s.enabled = false;
                }
                else
                {
                    
                }
            }
        }
        else
        {
            foreach (MonoBehaviour s in GetComponents<MonoBehaviour>())
            {
                if (s.enabled == false)
                {
                    s.enabled = true;
                }
            }
        }
    }

    private float fallHeight = 7f;

    private bool firstStart = true;
    public void startAnimation()
    {
        GetComponent<Health>().startInvulnerability();
        if (firstStart)
        {
            originalQuadScale = new Vector2(quadTransform.localScale.x, quadTransform.localScale.y);
            originalShadowScale = new Vector2(shadowTransform.localScale.x, shadowTransform.localScale.y);
            firstStart = false;
        }
        hideModel(false);
        quadTransform.localScale = new Vector3(0f, 0f, quadTransform.localScale.z);
        shadowTransform.localScale = new Vector3(0f, 1f, shadowTransform.localScale.z);

        playerTransform.localPosition = new Vector3(playerTransform.localPosition.x, fallHeight + 0.35f, playerTransform.localPosition.z);
        counter = 1;
        StartCoroutine("scalePortal");
    }

    private int counter = 1;
    private float speed = 20f; //higher the faster
    IEnumerator scalePortal()
    {
        while (counter <= speed)
        {
            quadTransform.localScale = new Vector3((originalQuadScale.x / speed) * counter, (originalQuadScale.y / speed) * counter, quadTransform.localScale.z);
            counter++;
            yield return new WaitForSeconds(0.01f);
        }
        counter = 1;
        StartCoroutine("playerFall");
    }

    private float fallSpeed = 10f;
    IEnumerator playerFall() //shadow grows here too
    {
        while (counter <= fallSpeed)
        {
            shadowTransform.localScale = new Vector3((originalShadowScale.x / fallSpeed) * counter, originalShadowScale.y, shadowTransform.localScale.z);
            playerTransform.localPosition = new Vector3(playerTransform.localPosition.x, playerTransform.localPosition.y - (fallHeight / fallSpeed), playerTransform.localPosition.z);

            counter++;
            yield return new WaitForSeconds(0.01f);
        }
        counter = 1;
        StartCoroutine("portalClose");
    }

    IEnumerator portalClose()
    {
        while (counter <= speed)
        {
            quadTransform.localScale = new Vector3(quadTransform.localScale.x - (originalQuadScale.x/speed), quadTransform.localScale.y - (originalQuadScale.y / speed), quadTransform.localScale.z);
            counter++;
            yield return new WaitForSeconds(0.01f);
        }

        counter = 1;
        GetComponent<Status>().teleporting = false;
        disableStuff(false);

        GetComponent<Health>().endInvulnerability(2f);
    }







}
