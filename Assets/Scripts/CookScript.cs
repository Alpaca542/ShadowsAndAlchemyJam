using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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
    public bool InCar;
    public Animator myBody;


    public SpriteRenderer handCarry;
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
            if (inventory[whatItem] > 1)
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
        if (Input.GetKeyDown(KeyCode.H) || Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            moveActiveSlot(true);
        }
        else if (Input.GetKeyDown(KeyCode.K) || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            moveActiveSlot(false);
        }
        
       
        handCarry.sprite = itemSprites[Array.IndexOf(itemFilter, inventory.ElementAt(ActiveSlot).Key)];
    }

    void moveActiveSlot(bool up)
    {
        inventoryGrid.transform.GetChild(ActiveSlot).GetComponent<ImageKeeper>().myImg.gameObject.SetActive(false);
        if (ActiveSlot == 0 && !up)
        {
            ActiveSlot = max_slots - 1;
        }
        else if (ActiveSlot == max_slots - 1 && up)
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
        inventoryGrid.transform.GetChild(ActiveSlot).GetComponent<ImageKeeper>().myImg.gameObject.SetActive(true);
    }

    private void Start()
    {
        inventory = new Dictionary<string, int>();
    }

    
}
