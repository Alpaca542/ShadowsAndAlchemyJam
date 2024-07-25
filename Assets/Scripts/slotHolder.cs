using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class slotHolder : MonoBehaviour
{
    public Image img;
    public TMP_Text txt;
    public TMP_Text txtAmount;
    public MainShop myshop;
    public bool forCookOnly;
    public int myPrice;
    public string myName;

    public int myAmount;

    public void Start()
    {
        if (!myshop.isCookHere && forCookOnly)
        {
            Deactivate();
        }
        else if (myshop.isCookHere && !forCookOnly)
        {
            Deactivate();
        }
        else
        {
            img.GetComponent<Image>().color = Color.white;
            GetComponent<Image>().enabled = true;
            gameObject.GetComponent<Button>().interactable = true;
        }

        txtAmount.text = myAmount.ToString();
    }
    bool checkIfSlotIsFull(Dictionary<string, int> inv, int slot)
    {
        return slot < inv.Count;
    }
    public void BuyMe()
    {
        moneyManager MoneyManager = GameObject.FindGameObjectWithTag("MoneyManager").GetComponent<moneyManager>();
        if (myPrice < 0)
        {
            CookScript cook = myshop.collision.GetComponent<CookScript>();
            if (checkIfSlotIsFull(cook.inventory, cook.ActiveSlot) && (cook.inventory.ElementAt(cook.ActiveSlot).Key == myName))
            {
                Debug.Log(123);
                myshop.collision.GetComponent<CookScript>().RemoveItem(myName);
                myAmount--;
                txtAmount.text = myAmount.ToString();
                if (myAmount <= 0)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                return;
            }
        }
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
            myAmount--;
            txtAmount.text = myAmount.ToString();
            if (myAmount <= 0)
            {
                Destroy(gameObject);
            }
            MoneyManager.LoseMoney(myPrice);
        }
    }

    public void Deactivate()
    {
        img.GetComponent<Image>().color = Color.gray;
        GetComponent<Image>().enabled = false;
        gameObject.GetComponent<Button>().interactable = false;
    }
}
