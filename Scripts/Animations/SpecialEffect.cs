using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffect : MonoBehaviour
{

    private ParticleSystem PS;
    private float soundLength;
    private float particleLength;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<ParticleSystem>() != null)
        {
            PS = GetComponent<ParticleSystem>();
            particleLength = PS.main.duration;
        }
        else
        {
            particleLength = 0;
        }
        if (GetComponent<AudioSource>().clip != null)
        {
            GetComponent<AudioSource>().volume = SaveLoad.current.getSFXVolume();
            soundLength = GetComponent<AudioSource>().clip.length;
        }
        else
        {
            soundLength = 0;
        }
        StartCoroutine("kill");
    }

    IEnumerator kill()
    {
        yield return new WaitForSeconds(Mathf.Max(soundLength, particleLength));
        Destroy(gameObject);
    }
}
