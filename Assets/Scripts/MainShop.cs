using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainShop : MonoBehaviour
{
    public Sprite[] EverythingISell;
    public int[] EverythingISellPrices;
    public bool[] EverythingISellForCookBools;

    public Sprite[] EverythingIBuy;
    public int[] EverythingIBuyPrices;

    public int howManySlotsMin;
    public int howManySlotsMax;
    private int howManySlots;

    public int maxAmount;

    public List<GameObject> slots = new List<GameObject>();
    public GameObject[] slotForSale;

    public int priceSpread;
    public GameObject myUI;
    public bool isCookHere;
    public GameObject collision;

    public GameObject grid;

    private void Start()
    {
        howManySlots = Random.Range(howManySlotsMin, howManySlotsMax + 1);
        GenerteAssortiment();
    }

    private void GenerteAssortiment()
    {
        for (int i = 0; i < howManySlots; i++)
        {
            int chosenObject = Random.Range(0, EverythingISell.Length);
            int chosenSlot = Random.Range(0, slots.Count);
            int chosenAmount = Random.Range(1, maxAmount);
            slots[chosenSlot].SetActive(true);
            slots[chosenSlot].GetComponent<slotHolder>().img.sprite = EverythingISell[chosenObject];
            int setPrice = EverythingISellPrices[chosenObject] + Random.Range(-priceSpread, priceSpread);
            slots[chosenSlot].GetComponentInChildren<TMP_Text>().text = setPrice.ToString();
            slots[chosenSlot].GetComponent<slotHolder>().myPrice = setPrice;
            slots[chosenSlot].GetComponent<slotHolder>().myAmount = chosenAmount;
            slots[chosenSlot].GetComponent<slotHolder>().myName = EverythingISell[chosenObject].name;
            slots[chosenSlot].GetComponent<slotHolder>().forCookOnly = EverythingISellForCookBools[chosenObject];
            slots.RemoveAt(chosenSlot);
        }

        for (int i = 0; i < slotForSale.Length; i++)
        {
            int chosenAmount = Random.Range(1, maxAmount);
            int chosenObject = Random.Range(0, EverythingIBuy.Length);
            slotForSale[i].GetComponent<slotHolder>().img.sprite = EverythingIBuy[chosenObject];
            int setPrice = EverythingIBuyPrices[chosenObject] + Random.Range(-priceSpread, priceSpread);
            slotForSale[i].GetComponentInChildren<TMP_Text>().text = setPrice.ToString();
            slotForSale[i].GetComponent<slotHolder>().myPrice = -setPrice;
            slotForSale[i].GetComponent<slotHolder>().myAmount = chosenAmount;
            slotForSale[i].GetComponent<slotHolder>().myName = EverythingIBuy[chosenObject].name;
            slotForSale[i].GetComponent<slotHolder>().forCookOnly = true;
        }

    }

    public void Open()
    {
        foreach (Transform slot in grid.transform)
        {
            if (slot.GetComponent<slotHolder>() != null)
            {
                slot.GetComponent<slotHolder>().Start();
            }
        }
        myUI.SetActive(true);
    }

    public void Close()
    {
        myUI.SetActive(false);
    }

}
