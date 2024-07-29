using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingToggle : MonoBehaviour
{
    public bool Walking;
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
    }
}