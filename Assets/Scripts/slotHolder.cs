using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class slotHolder : MonoBehaviour
{
    public Image img;
    public TMP_Text txt;
    public MainShop myshop;
    private bool cookhere;
    public bool forCookOnly;

    private void Start()
    {
        if (myshop.isCookHere && forCookOnly)
        {
            img.GetComponent<Image>().color = Color.gray;
            GetComponent<Image>().enabled = false;
        }
        else if (!myshop.isCookHere && !forCookOnly)
        {
            img.GetComponent<Image>().color = Color.gray;
            GetComponent<Image>().enabled = false;
        }
        else
        {
            img.GetComponent<Image>().color = Color.white;
            GetComponent<Image>().enabled = true;
        }
    }

    public void BuyMe()
    {
        cookhere = myshop.isCookHere;
        if (cookhere)
        {

        }
    }
}
