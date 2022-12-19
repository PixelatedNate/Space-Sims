using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;
    
    public enum Sound
    {
        UIclick,
        PanalOpen,
        Error,
        RoomPlaced,
        QuestCompleted,
        MetalFootSteps,
    }


    public enum VoiceSounds
    {
        PickVoiceLines,
        PutDownVoiceLines,
    }


    [Serializable]
    public class SoundAudioClip
    {
        public Sound sound;
        public AudioClip audioClip;
    }

    [Serializable]
    public class VoiceSoundAudioClip
    {
        public VoiceSounds sound;
        public AudioClip[] audioClips;
    }

    [SerializeField]
    private SoundAudioClip[] _soundAudioClips;

    [SerializeField]
    private VoiceSoundAudioClip[] _voiceSoundAudioClips;

    [SerializeField]
    private AudioSource _effectsSource;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void PlaySound(Sound sound)
    {
        _effectsSource.PlayOneShot(GetAudioClip(sound));
    }

    public void PlaySoundIfVisableAndZoomedIn(Sound sound, Vector3 position, float MinzoomValue, float voloume = 1)
    {
        Vector3 viewPortPoint = Camera.main.WorldToViewportPoint(position);
        if (Camera.main.orthographicSize <= MinzoomValue)
        {
            if (Mathf.Abs(viewPortPoint.x) < 1 && Mathf.Abs(viewPortPoint.y) < 1)
            {
                _effectsSource.PlayOneShot(GetAudioClip(sound),voloume);
            }
        }
    }

    public void PlaySound(VoiceSounds voiceSound, float pitch)
    {

        AudioClip[] soundClips = GetVoiceAudioClips(voiceSound);

        int index = UnityEngine.Random.Range(0, soundClips.Length);

        AudioSource customPitchAudioSoruce = gameObject.AddComponent<AudioSource>();

        customPitchAudioSoruce.volume = _effectsSource.volume;
        customPitchAudioSoruce.pitch = pitch;
        customPitchAudioSoruce.PlayOneShot(soundClips[index]);
        StartCoroutine("SoundCleanUp", customPitchAudioSoruce);
    }

    private AudioClip[] GetVoiceAudioClips(VoiceSounds voiceSound)
    {
        foreach (VoiceSoundAudioClip voiceSoundAudioClip in _voiceSoundAudioClips)
        {
            if (voiceSoundAudioClip.sound == voiceSound)
            {
                return voiceSoundAudioClip.audioClips;
            }
        }
        return null;
    }


   IEnumerator SoundCleanUp(AudioSource audioSource)
    {
        yield return new WaitUntil(() => audioSource.isPlaying == false);
        Destroy(audioSource);
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        foreach(SoundAudioClip soundAudioClip in _soundAudioClips)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        return null;
    }

}
