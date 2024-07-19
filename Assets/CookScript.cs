using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CookScript : MonoBehaviour
{
    public int ActiveSlot;
    public LayerMask brewerLayer;
    public string[] itemFilter;
    public Sprite[] itemSprites;
    public Sprite defaultSprite;
    public Dictionary<string, int> inventory;
    public int max_slots = 6;
    public GameObject inventoryGrid;

    public void Freeze()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public void UnFreeze()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void GetItem(string whatItem)
    {
        if (itemFilter.Contains(whatItem))
        {
            if (inventory.ContainsKey(whatItem))
            {
                inventory[whatItem]++;
                UpdateInventory();
            }
            else
            {
                if (inventory.Count < max_slots)
                {
                    inventory[whatItem] = 1;
                    UpdateInventory();
                }
            }

        }
    }

    public void RemoveItem(string whatItem)
    {
        if (inventory.ContainsKey(whatItem))
        {
            if (inventory[whatItem] > 0)
            {
                inventory[whatItem]--;
                UpdateInventory();
            }
            else
            {
                inventory.Remove(whatItem);
                UpdateInventory();
            }
        }
    }

    void UpdateInventory()
    {
        for (int i = 0; i < max_slots; i++)
        {
            GameObject currentSlot = inventoryGrid.transform.GetChild(i).gameObject;

            if (i < inventory.Count)
            {
                string currentItem = inventory.ElementAt(i).Key;
                int currentAmount = inventory.ElementAt(i).Value;

                currentSlot.GetComponentInChildren<TMP_Text>().text = currentAmount.ToString();
                currentSlot.GetComponent<Image>().sprite = itemSprites[Array.IndexOf(itemFilter, currentItem)];
            }
            else
            {
                currentSlot.GetComponentInChildren<TMP_Text>().text = "";
                currentSlot.GetComponent<Image>().sprite = defaultSprite;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveActiveSlot(true);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveActiveSlot(false);
        }
    }

    void moveActiveSlot(bool up)
    {
        if (ActiveSlot == 0 && !up)
        {
            ActiveSlot = max_slots;
        }
        else if (ActiveSlot == max_slots && up)
        {
            ActiveSlot = 0;
        }
        else
        {
            if (up)
            {
                ActiveSlot++;
            }
            else
            {
                ActiveSlot--;
            }
        }
    }

    private void Start()
    {
        inventory = new Dictionary<string, int>();
    }
}
