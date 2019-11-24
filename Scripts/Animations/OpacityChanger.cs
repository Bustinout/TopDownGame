using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityChanger : MonoBehaviour
{
    //changes the opacity over time
    private float speed = 12f;

    public void startOpacityChanger()
    {
        StartCoroutine("opacityChanger");
    }

    public void stopOpacityChanger()
    {
        StopCoroutine("opacityChanger");
        foreach (SpriteRenderer x in GetComponentsInChildren<SpriteRenderer>())
        {
            Color temp = x.color;
            temp.a = 1f;
            x.color = temp;
        }
    }

    //private bool go = true;
    IEnumerator opacityChanger()
    {
        while (true)
        {
            float alpha = (Mathf.Sin(Time.time * speed)*0.3f)+.7f;
            foreach (SpriteRenderer x in GetComponentsInChildren<SpriteRenderer>())
            {
                Color temp = x.color;
                temp.a = alpha;
                x.color = temp;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
