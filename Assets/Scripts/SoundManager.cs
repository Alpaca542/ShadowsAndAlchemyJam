using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class soundManager : MonoBehaviour
{
    private AudioSource sound;
    public AudioClip[] soundslist;
    public bool shouldILoop = false;
    public bool amIPlaying = false;
    public bool shouldIPlayOnStart = false;

    void Awake()
    {
        sound = GetComponent<AudioSource>();
        if (shouldIPlayOnStart)
        { PlaySound(0, shouldILoop); }
    }

    public void PlaySound(int whichsound, bool Loop)
    {
        if (!sound.isPlaying && PlayerPrefs.GetString("sfx") == "True")
        {
            sound.clip = soundslist[whichsound];
            sound.volume = PlayerPrefs.GetFloat("volume");
            sound.Play();
            sound.loop = Loop;
        }

    }

    public void StopPlaying()
    {
        sound.loop = false;
    }

    public void letmeplay()
    {
        amIPlaying = false;
    }
}