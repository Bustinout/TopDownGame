using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stunBar : MonoBehaviour
{
    public Slider stunSlider;
    public GameObject bar;


    private void Start()
    {
        StaticManager.stunbar = this;
    }

    public void refresh(float x, float y)
    {
        stunSlider.value = x / y;
    }


}
