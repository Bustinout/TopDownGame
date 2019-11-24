using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    void Start()
    {
        StaticManager.sceneFader = this;
        setOpacity(0f);
    }

    public void setOpacity(float x)
    {
        Color temp = GetComponent<Image>().color;
        temp.a = x;
        GetComponent<Image>().color = temp;
    }

    IEnumerator startGame()
    {
        while (GetComponent<Image>().color.a < 1)
        {
            setOpacity(GetComponent<Image>().color.a + .05f);
            yield return new WaitForSecondsRealtime(.05f);
        }
        //find way to make it do the command that fclaled it 
    }

}
