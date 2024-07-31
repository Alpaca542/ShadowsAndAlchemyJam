using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class vignetteTweener : MonoBehaviour
{
    public GameObject animm;
    public GameObject place1;
    public GameObject place2;
    public int type;
    private void Start()
    {
        if (type == 1)
        {
            animm.transform.DOMove(place2.transform.position, 5f).SetUpdate(true); ;
        }
        else if (type == 2)
        {
            animm.transform.DOMove(place1.transform.position, 5f).SetUpdate(true); ;
        }
    }
}
