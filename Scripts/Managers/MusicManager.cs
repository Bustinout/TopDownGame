using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource AS;
    public bool titleScreen;

    void Start()
    {
        StaticManager.musicmanager = this;
        
        initializeMusic();
    }

    public void matchSettings()
    {
        AS.volume = SaveLoad.current.getMusicVolume();
    }

    public void initializeMusic()
    {
        matchSettings();

        //choose song (already in for demo)
        //AS.time = AS.clip.length * .95f; //to check loop
        if (titleScreen)
        {
            //load music;
        }

        AS.Play();
    }

    public void pauseMusic()
    {
        AS.Pause();
    }

    public void unpauseMusic()
    {
        AS.Play();
    }

}
