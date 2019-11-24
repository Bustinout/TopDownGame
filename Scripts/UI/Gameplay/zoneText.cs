using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zoneText : MonoBehaviour
{
    private Text text;
    

    void Start()
    {
        text = GetComponent<Text>();
        StaticManager.zonetext = this;
    }

    public void startZoneText(string x)
    {
        text.text = x;

        Color temp = text.color;
        temp.a = 1f;
        text.color = temp;

        StartCoroutine("FadeAnimation1");
    }


    IEnumerator FadeAnimation1()
    {

        yield return new WaitForSecondsRealtime(1.5f);
        StartCoroutine("FadeAnimation2");
    }

    IEnumerator FadeAnimation2()
    {
        while (text.color.a > 0)
        {
            Color temp = text.color;
            temp.a -= 0.02f;
            text.color = temp;

            yield return new WaitForSecondsRealtime(.02f);
        }
    }
}
