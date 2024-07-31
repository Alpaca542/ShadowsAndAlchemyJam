using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        DOTween.To(() => audio.volume, x => audio.volume = x, 0.5f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
