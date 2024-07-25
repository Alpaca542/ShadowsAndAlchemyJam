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
    public bool forCookOnly;
    public int myPrice;
    public string myName;

    private void Start()
    {
        if (myshop.isCookHere && forCookOnly)
        {
            Deactivate();
        }
        else if (!myshop.isCookHere && !forCookOnly)
        {
            Deactivate();
        }
        else
        {
            img.GetComponent<Image>().color = Color.white;
            GetComponent<Image>().enabled = true;
            gameObject.GetComponent<Button>().interactable = true;
        }
    }

    public void BuyMe()
    {
        moneyManager MoneyManager = GameObject.FindGameObjectWithTag("MoneyManager").GetComponent<moneyManager>();
        if (MoneyManager.MyMoney >= myPrice)
        {
            if (myshop.isCookHere)
            {
                myshop.collision.GetComponent<CookScript>().GetItem(myName);
            }
            else if (!myshop.isCookHere)
            {
                myshop.collision.GetComponent<DefenderScript1>().GetItem(myName);
            }

            MoneyManager.LoseMoney(myPrice);
            Destroy(gameObject);
        }
    }

    public void Deactivate()
    {
        img.GetComponent<Image>().color = Color.gray;
        GetComponent<Image>().enabled = false;
        gameObject.GetComponent<Button>().interactable = false;
    }
}
