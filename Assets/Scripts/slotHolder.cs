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
    public int myPrice;
    public string myName;

    public int myAmount;

    public void Start()
    {
        if (!(myName == "heal" && myshop.isCookHere) && !(myPrice < 0 && !myshop.isCookHere))
        {
            img.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            GetComponent<Image>().enabled = true;
            gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            Deactivate();
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
                transform.parent.GetComponent<soundManager>().PlaySound(0, 0.7f, 1.3f);
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
            transform.root.GetComponent<soundManager>().PlaySound(1, 0.7f, 1.3f);
            if (myshop.isCookHere && myName != "Heal")
            {
                myshop.collision.GetComponent<CookScript>().GetItem(myName);
            }
            else if (!myshop.isCookHere)
            {
                myshop.collision.GetComponent<DefenderScript1>().GetItem(myName);
            }
            txtAmount.text = myAmount.ToString();
            MoneyManager.LoseMoney(myPrice);

            myAmount--;
            if (myAmount <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Deactivate()
    {
        img.GetComponent<Image>().color = new Color32(255, 255, 255, 50);
        GetComponent<Image>().enabled = false;
        gameObject.GetComponent<Button>().interactable = false;
    }
}
