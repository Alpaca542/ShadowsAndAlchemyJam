using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicscript : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        DOTween.To(() => GetComponent<AudioSource>().volume, x => GetComponent<AudioSource>().volume = x, 0.8f, 1f).SetUpdate(true);
    }
}
