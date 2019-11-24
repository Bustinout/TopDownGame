using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Library
{
    //just a library containing important references

    public static KeyCode[] pcInputDefaults = new KeyCode[] {
        KeyCode.Mouse0,
        KeyCode.Mouse1,
        KeyCode.LeftShift,
        KeyCode.E,
        KeyCode.R,
        KeyCode.F,
        KeyCode.Escape
    };

    //this includes the icons of every spell, corresponding with spellID
    public static string[] spellIcons = new string[] {

    };

    public static int[] weaponSkillGrowthRate = new int[] { 8, 10, 12, 14};
    public static int getWeaponSkillGain(int x)
    {
        return weaponSkillGrowthRate[x];
    }

    public static string[] heroNames = new string[] { "New Hero" };
    public static string randomizeHeroName()
    {
        return heroNames[Random.Range(0, heroNames.Length-1)];
    }

    public static void toggleFullScreen()
    {
        SaveLoad.current.fullscreen = !SaveLoad.current.fullscreen;
        setDisplaySettings();
    }


    public static void setResolution(Tuple x)
    {
        SaveLoad.current.resolution = x;
        setDisplaySettings();
    }

    public static void setDisplaySettings()
    {
        Screen.SetResolution(SaveLoad.current.resolution.int1, SaveLoad.current.resolution.int2, SaveLoad.current.fullscreen);
    }
}
