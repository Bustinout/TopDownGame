using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class delayedAoE : MonoBehaviour
{
    public SpriteRenderer model;
    public float animationDelay; //time until projectile spawns
    public float timeUntilActive;
    public float timeActive; //time the aoe hitbox remains
    public float timeUntilInactive; //time until gameobject is set inactive

    public MMFeedbacks HitDamageableFeedback;

    public GameObject particleOnActive;

    public void setModelInvisible()
    {
        Color temp = model.color;
        temp.a = 0f;
        model.color = temp;
    }

    void Start()
    {
        setModelInvisible();
        StartCoroutine("delay");
    }

    public void activate()
    {
        model.gameObject.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = true;
        HitDamageableFeedback.PlayFeedbacks(this.transform.position);
        StartCoroutine("projectileFadeIn");
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(animationDelay);
        StartCoroutine("projectileFadeIn");
    }

    IEnumerator projectileFadeIn()
    {
        while (model.color.a < 1f)
        {
            Color temp = model.color;
            temp.a += 1 / (timeUntilActive / 0.1f);
            model.color = temp;
            yield return new WaitForSeconds(0.1f);
        }
        setModelInvisible();
        GetComponent<BoxCollider2D>().enabled = true;
        if (HitDamageableFeedback != null)
        {
            HitDamageableFeedback.PlayFeedbacks(this.transform.position);

        }
        if (particleOnActive != null)
        {
            Instantiate(particleOnActive, transform.position, Quaternion.identity, StaticManager.ObjectPool.transform);
        }
        StartCoroutine("hitBoxActive");
    }

    IEnumerator hitBoxActive()
    {
        yield return new WaitForSeconds(timeActive);
        GetComponent<BoxCollider2D>().enabled = false;


        StartCoroutine("delayBeforeDestroy");
    }

    IEnumerator delayBeforeDestroy()
    {
        yield return new WaitForSeconds(timeUntilInactive);
        gameObject.SetActive(false);
    }

    //coroutine, add alpha, play effect + deactivate model, then set inactive
}
