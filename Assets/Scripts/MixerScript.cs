using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MixerScript : MonoBehaviour
{
    public bool AmIFilled;
    public GameObject indicator;
    public GameObject requirerLL;
    public GameObject requireL;
    public GameObject requirerLLL;
    public GameObject requirerLLLL;
    public GameObject requirerLLLLL;
    public Image Indicator;
    float blue1;
    float blue2;
    float red;

    public bool check;


    public GameObject shrederUI;
    public GameObject collision;
    Canvas canvas;
    bool redANDblue = false;
    bool pureRed = false;
    bool greenANDblue = false;
    bool Analyzed = false;
    bool Graphed = false;

    public void GetStarted()
    {
        CookScript cook = collision.gameObject.GetComponent<CookScript>();
        if (checkIfSlotIsFull(cook.inventory, cook.ActiveSlot) && ((cook.inventory.ElementAt(cook.ActiveSlot).Key == "pureRed" && !pureRed) || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "redANDblue" && !redANDblue) || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "greenANDblue" && !greenANDblue) || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "Analyzed" && !Analyzed) || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "Graphed" && !Graphed)))
        {
            if (!AmIFilled)
            {
                if (checkIfSlotIsFull(cook.inventory, cook.ActiveSlot))
                {
                    if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "redANDblue")
                    {
                        redANDblue = true;
                        requirerLL.gameObject.GetComponent<Image>().color = new Color32(0, 0, 255, 255);
                        collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);

                    }
                    if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "pureRed")
                    {
                        pureRed = true;
                        requireL.gameObject.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                        collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                    }
                    if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "greenANDblue")
                    {
                        greenANDblue = true;
                        requirerLLL.gameObject.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                        collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                    }
                    if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "Analyzed")
                    {
                        Analyzed = true;
                        requirerLLLL.gameObject.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                        collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                    }
                    if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "Graphed")
                    {
                        Graphed = true;
                        requirerLLLLL.gameObject.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                        collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                    }
                }

                if (redANDblue&&pureRed&&greenANDblue&&Analyzed&&Graphed)
                {
                    shrederUI.SetActive(true);
                    check = true;


                    blue1 = (UnityEngine.Random.Range(90, 100)) / 100.0f;
                    blue2 = (UnityEngine.Random.Range(50, 100)) / 100.0f;
                    red = (UnityEngine.Random.Range(30, 100)) / 100.0f;
                    Debug.Log(blue1); Debug.Log(blue2); Debug.Log(red);
                }
            }
        }
    }

    void Check()
    {
        
    }
    public void FinishGame()
    {
        Invoke(nameof(DestroyUI), 2f);
        AmIFilled = true;
        indicator.GetComponent<Image>().color = Color.red;
        requirerLL.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
        requireL.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
        requirerLLL.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
        requirerLLLL.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
        requirerLLLLL.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
        pureRed = false;
        Analyzed = false;
        Graphed = false;
        greenANDblue = false;
        redANDblue = false;
    }
    void DestroyUI()
    {
        shrederUI.SetActive(false);
        collision.gameObject.GetComponent<CookScript>().UnFreeze();
    }

    bool checkIfSlotIsFull(Dictionary<string, int> inv, int slot)
    {
        return slot < inv.Count;
    }
    void Update()
    {
        if (collision != null && collision.gameObject.tag == "Cook")
        {
            CookScript cook = collision.gameObject.GetComponent<CookScript>();

            if (AmIFilled && (Input.GetKeyDown(KeyCode.E)))
            {

                AmIFilled = false;
                indicator.GetComponent<Image>().color = Color.white;
                cook.GetItem("whiteMatter");
                Debug.Log("AfterGive");

            }
        }
        if (check && (collision != null))
        {
            Check();
        }
        if (collision != null)
        {
            // Check();
           
        }
    }
}
