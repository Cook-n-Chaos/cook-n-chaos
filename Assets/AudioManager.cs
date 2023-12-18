using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceSfx;
    [SerializeField] private Sound[] clipsMusic;
    [SerializeField] private Sound[] clipsSfx;



    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume");
            ChangeVolume(savedVolume);
        }
        else
        {
            ChangeVolume(0.3f);
        }

        if (PlayerPrefs.HasKey("VolumeSfx"))
        {
            float savedVolumeSfx = PlayerPrefs.GetFloat("VolumeSfx");
            ChangeVolumeSfx(savedVolumeSfx);
        }
        else
        {
            ChangeVolumeSfx(0.3f);
        }

        PlayMusic("music");
    }
    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void ChangeVolumeSfx(float volume)
    {
        audioSourceSfx.volume = volume;
        PlayerPrefs.SetFloat("VolumeSfx", volume);
    }
    public void PlayMusic(string sound)
    {
        Sound s = Array.Find(clipsMusic, item => item.name == sound);
        if (s == null)
        {
            //Debug.LogWarning("Music: " + name + " not found!");
            return;
        }
        audioSource.clip = s.clip;
        audioSource.loop = s.loop; // Set the loop property based on the sound's shouldLoop flag
        audioSource.PlayOneShot(s.clip);
    }

    public void Play(string sound)
    {
        Sound s = Array.Find(clipsSfx, item => item.name == sound);
        if (s == null)
        {
            //Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        audioSourceSfx.clip = s.clip;
        audioSourceSfx.PlayOneShot(s.clip);
    }
    public void StopAudio()
    {
        audioSource.Stop();
        audioSourceSfx.Stop();
    }
    public float GetSfxVolume()
    {
        return audioSourceSfx.volume;
    }
}