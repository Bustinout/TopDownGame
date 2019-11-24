using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeSlider : MonoBehaviour
{
    public Slider slider;

    public enum sliderType { Master, Music, SFX };
    public sliderType type;

    private void Start()
    {
        setInitialValue();
    }

    public void setInitialValue()
    {
        switch (type)
        {
            case sliderType.Master:
                slider.value = SaveLoad.current.masterVolume;
                break;
            case sliderType.Music:
                slider.value = SaveLoad.current.musicVolume;
                break;
            case sliderType.SFX:
                slider.value = SaveLoad.current.sfxVolume;
                break;
            default:
                Debug.Log("Invalid SliderType Case");
                break;
        }
    }

    public void setNewValue()
    {
        switch (type)
        {
            case sliderType.Master:
                SaveLoad.current.masterVolume = slider.value;
                break;
            case sliderType.Music:
                SaveLoad.current.musicVolume = slider.value;
                break;
            case sliderType.SFX:
                SaveLoad.current.sfxVolume = slider.value;
                break;
            default:
                Debug.Log("Invalid SliderType Case");
                break;
        }
        StaticManager.matchVolumeSettings();
    }
}
