using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUIMenu : MonoBehaviour
{

    [SerializeField]
    AudioSource SfxAudioSorce;
    
    [SerializeField]
    AudioSource MusicAudioSorce;


    [SerializeReference]
    Slider SfxSlider;
 
    [SerializeReference]
    Slider MusicSlider;

    public void SetSfxVoloum()
    {
        SfxAudioSorce.volume = SfxSlider.value;
    }

    public void SetMusicVoloum()
    {
        
        MusicAudioSorce.volume = MusicSlider.value;
    }

    public void CloseSettings()
    {
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        TouchControls.EnableCameramovemntAndSelection(true);
    }

    private void OnEnable()
    {
        TouchControls.EnableCameramovemntAndSelection(false);
    }
}
