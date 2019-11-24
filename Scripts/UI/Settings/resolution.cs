using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resolution : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject clickedSFX;

    private Tuple[] resolutions = new Tuple[] {
        new Tuple(1920, 1080),
        new Tuple(1680, 1050),
        new Tuple(1600, 1200),
        new Tuple(1600, 1024),
        new Tuple(1600, 900),
        new Tuple(1440, 900),
        new Tuple(1360, 768),
        new Tuple(1280, 960),
        new Tuple(1200, 800),
        new Tuple(1152, 864),
        new Tuple(1024, 768),
    };

    public int findValue(Tuple r)
    {
        for(int x = 0; x < resolutions.Length; x++)
        {
            if (resolutions[x].int1 == r.int1 && resolutions[x].int2 == r.int2)
            {
                return x;
            }
        }
        Debug.Log("Resolution Not Found");
        return -1;
    }

    private bool settingInitial;
    public void setInitial()
    {
        settingInitial = true;
        dropdown.value = findValue(SaveLoad.current.resolution);
    }

    public void valueChanged()
    {
        if (settingInitial)
        {
            settingInitial = false;
        }
        else
        {
            Instantiate(clickedSFX);
            Library.setResolution(resolutions[dropdown.value]);
        }
    }

    private void Start()
    {
        setInitial();
    }
}
