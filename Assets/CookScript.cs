using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookScript : MonoBehaviour
{
    // Start is called before the first frame update
    public string WhatIHave = "0";
    public bool Connected = false;
    void Start()
    {
        
    }
    public void Freeze()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    }
    public void UnFreeze()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
