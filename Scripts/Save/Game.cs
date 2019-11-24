using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game
{
    public bool newGame = true;
    //set to false after character name

    //Save Game for ALL playthroughs until manually reset

    //Sound Settings
    public float masterVolume = 0.2f;
    public float musicVolume = 0.2f;
    public float sfxVolume = 0.2f;

    //Graphics Settings
    public Tuple resolution = new Tuple(1600, 900);
    public bool fullscreen = false;

    public float getMusicVolume()
    {
        return (masterVolume * musicVolume);
    }

    public float getSFXVolume()
    {
        return (masterVolume * sfxVolume);
    }

    //Controls Settings
    // 0 - primary
    // 1 - secondary
    // 2 - dash/defensive
    // 3 - s1
    // 4 - s2
    // 5 - interact
    // 6 - pause
    // 7 - show inventory

    public KeyCode[] pcInput = new KeyCode[] {
        KeyCode.Mouse0,
        KeyCode.Mouse1,
        KeyCode.LeftShift,
        KeyCode.E,
        KeyCode.R,
        KeyCode.F,
        KeyCode.Escape,
        KeyCode.Tab
    };
    
    public void resetInputs()
    {
        pcInput = Library.pcInputDefaults;
    }



    
    //Next Level INfo
    public string zoneName = "Magedog Woods"; //name of zone headed into stored here, used in roommanager to do zonetext



    public int currentYear = 0;
    //Heroes
    public Hero currentHero;
    public List<Hero> pastHeroes;

    public void retireHero()
    {
        currentHero.deathYear = currentYear;
        pastHeroes.Add(currentHero);
        currentHero = new Hero();
        //go to intro/naming scene
    }

    public Game()
    {
        currentHero = new Hero();

    }


}
