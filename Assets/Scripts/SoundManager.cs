using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    private AudioSource sound;
    public AudioClip[] soundslist;
    public bool shouldILoop = false;
    public bool amIPlaying = false;
    public bool shouldIPlayOnStart = false;
    public GameObject audioHandler;

    public void PlaySound(int whichsound, float pitchrangeDOWN, float pitchrangeUP)
    {
        GameObject gmb = Instantiate(audioHandler, transform.position, Quaternion.identity);
        gmb.GetComponent<AudioSource>().clip = soundslist[whichsound];
        gmb.GetComponent<AudioSource>().pitch = Random.Range(pitchrangeDOWN, pitchrangeUP);
        gmb.GetComponent<AudioSource>().Play();
    }

    public void StopPlaying()
    {
        sound.loop = false;
    }
}