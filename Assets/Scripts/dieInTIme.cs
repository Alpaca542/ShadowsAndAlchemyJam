using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieInTIme : MonoBehaviour
{
    public float time_;

    public void Start()
    {
        CancelInvoke(nameof(DieInTime));
        Invoke(nameof(DieInTime), time_);
    }

    void DieInTime()
    {
        Destroy(gameObject);
    }
}
