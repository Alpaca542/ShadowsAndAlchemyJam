using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class giverScript : MonoBehaviour
{
    // Start is called before the first frame update

    public bool AmIFilled;

    float blue1;
    float blue2;
    float red;
    int CookedResult;

    bool check;


    public GameObject shrederUI;
    public GameObject collision;

    int blueNum = 0;
    int RedNum = 0;
    int greenNum = 0;
    int pureRedNum = 0;
    int redAndblueNum = 0;
    int greenAndblueNum = 0;
    int graphedNum = 0;
    int analyzedNum = 0;
    int whiteNum = 0;
    bool GameGoing = false;


    public TMP_Text redT;
    public TMP_Text greenT;
    public TMP_Text blueT;
    public TMP_Text pureRedT;
    public TMP_Text redAndblueT;
    public TMP_Text greenAndblueT;
    public TMP_Text graphedT;
    public TMP_Text analyzedT;
    public TMP_Text whiteT;



    public void GetLoot()
    {
        CookScript cook = collision.gameObject.GetComponent<CookScript>();
        if (checkIfSlotIsFull(cook.inventory, cook.ActiveSlot) && cook.inventory.ElementAt(cook.ActiveSlot).Key != "white")
        {
            GetComponent<soundManager>().PlaySound(0, 0.8f, 1.2f);
            if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "pureRed")
            {
                pureRedNum += 10;
                collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                // requireL.GetComponent<Image>().color = Color.white;
                pureRedT.text = pureRedNum.ToString();
            }
            else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "blue")
            {
                blueNum += 10;
                collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                // requirerLL.GetComponent<Image>().color = Color.white;
                blueT.text = blueNum.ToString();
            }
            else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "red")
            {
                RedNum += 10;
                collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                // requirerLL.GetComponent<Image>().color = Color.white;
                redT.text = RedNum.ToString();
            }
            else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "green")
            {
                greenNum += 10;
                collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                //requirerLL.GetComponent<Image>().color = Color.white;
                greenT.text = greenNum.ToString();
            }
            else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "redANDblue")
            {
                redAndblueNum += 10;
                collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                // requirerLL.GetComponent<Image>().color = Color.white;
                redAndblueT.text = redAndblueNum.ToString();
            }
            else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "greenANDblue")
            {
                greenAndblueNum += 10;
                collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                // requirerLL.GetComponent<Image>().color = Color.white;
                greenAndblueT.text = greenAndblueNum.ToString();
            }
            else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "Analyzed")
            {
                analyzedNum += 10;
                collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                //requirerLL.GetComponent<Image>().color = Color.white;
                analyzedT.text = analyzedNum.ToString();
            }
            else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "Graphed")
            {
                graphedNum += 10;
                collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                //requirerLL.GetComponent<Image>().color = Color.white;
                graphedT.text = graphedNum.ToString();
            }
            else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "white")
            {
                whiteNum += 10;
                collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                // requirerLL.GetComponent<Image>().color = Color.white;
                whiteT.text = whiteNum.ToString();
            }

        }
    }

    public void GiveLoot()
    {
        Debug.Log("IAmAlive");
        DefenderScript1 defender = collision.gameObject.GetComponent<DefenderScript1>();
        defender.bullets[0] += RedNum;
        defender.bullets[1] += greenNum;
        defender.bullets[2] += blueNum;
        defender.bullets[3] += pureRedNum;
        defender.bullets[4] += redAndblueNum;
        defender.bullets[5] += greenAndblueNum;
        defender.bullets[6] += graphedNum;
        defender.bullets[7] += analyzedNum;
        defender.bullets[8] += whiteNum;

        RedNum = 0;
        greenNum = 0;
        blueNum = 0;
        pureRedNum = 0;
        redAndblueNum = 0;
        greenAndblueNum = 0;
        graphedNum = 0;
        analyzedNum = 0;
        whiteNum = 0;
        for (int i = 0; i < 9; i++)
        {
            defender.BulletTextUpdate(i);
        }
    }

    private void Update()
    {
        redT.text = Convert.ToString(RedNum);
        greenT.text = Convert.ToString(greenNum);
        blueT.text = Convert.ToString(blueNum);
        pureRedT.text = Convert.ToString(pureRedNum);
        redAndblueT.text = Convert.ToString(redAndblueNum);
        greenAndblueT.text = Convert.ToString(greenAndblueNum);
        graphedT.text = Convert.ToString(graphedNum);
        analyzedT.text = Convert.ToString(analyzedNum);
        whiteT.text = Convert.ToString(whiteNum);
    }

    bool checkIfSlotIsFull(Dictionary<string, int> inv, int slot)
    {
        return slot < inv.Count;
    }
}
