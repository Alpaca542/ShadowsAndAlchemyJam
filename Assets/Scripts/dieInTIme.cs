using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieInTIme : MonoBehaviour
{
    public float time_;
    private void Start()
    {
        Invoke(nameof(DieInTime), time_);
    }

    void DieInTime()
    {
        Destroy(gameObject);
    }
}
