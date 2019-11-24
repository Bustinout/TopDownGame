using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textFader : MonoBehaviour
{
    private Text text;

    private void OnEnable()
    {
        text = GetComponent<Text>();
        radian = 0;
        StartCoroutine("startFader");
    }

    private void OnDisable()
    {
        StopCoroutine("startFader");
    }

    private int poop = 0;
    private float radian = 0;
    IEnumerator startFader()
    {
        while (poop == 0)
        {
            Color tmp = text.color;
            tmp.a = Mathf.Sin(radian) / 2 + 0.5f;
            text.color = tmp;
            radian += 0.15f;

            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
}
