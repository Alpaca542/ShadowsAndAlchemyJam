using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainShop : MonoBehaviour
{
    public GameObject[] EverythingISell;
    public int[] EverythingISellPrices;
    public int howManySlotsMin;
    public int howManySlotsMax;
    private int howManySlots;
    public List<GameObject> slots;
    public int priceSpread;
    public GameObject myUI;
    public bool isCookHere;

    private void Start()
    {
        howManySlots = Random.Range(howManySlotsMin, howManySlotsMax + 1);
        GenerteAssortiment();
    }

    private void GenerteAssortiment()
    {
        slots = new List<GameObject>();
        for (int i = 0; i < howManySlots; i++)
        {
            int chosenObject = Random.Range(0, EverythingISell.Length);
            int chosenSlot = Random.Range(0, slots.Count);
            slots[chosenSlot].SetActive(true);
            slots[chosenSlot].GetComponentInChildren<Image>().sprite = EverythingISell[chosenObject].GetComponent<SpriteRenderer>().sprite;
            slots[chosenSlot].GetComponentInChildren<TMP_Text>().text = EverythingISellPrices[chosenObject] + Random.Range(-priceSpread, priceSpread).ToString();
            slots.RemoveAt(chosenSlot);
        }
    }

    public void Open()
    {
        myUI.SetActive(true);
    }
}
