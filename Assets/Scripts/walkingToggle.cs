using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class walkingToggle : MonoBehaviour
{
    public bool Walking;
    public bool StartTheCar1;
    public AudioSource car;
    private bool started;
    private void Update()
    {
        if (Walking)
        {
            GetComponent<Animator>().SetBool("Walking", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Walking", false);
        }

        if (StartTheCar1 && !started)
        {
            started = true;
            StartTheCar();
        }
    }
    public void StartTheCar()
    {
        car.enabled = true;
        DOTween.To(() => car.volume, x => car.volume = x, 1, 0.5f);
    }
}