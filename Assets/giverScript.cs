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
    public GameObject indicator;
    public GameObject requirerLL;
    public GameObject requireL;
    public TMP_Text requirerLLTxt;
    public TMP_Text requireLTxt;
    public Image Indicator;
    public Slider requirer;
    public Slider requirer1;
    public Slider requirer2;
    public Text loss;
    public Text loss1;
    public Text loss2;
    public Text totalLoss;
    public Transform rot1;
    public Transform rot2;
    public Transform rot3;
    public Transform rot4;
    public Transform rot5;
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
    public void GetStarted()
    {
        CookScript cook = collision.gameObject.GetComponent<CookScript>();
        if (!AmIFilled)
        {
            if (blueNum > 0 && RedNum > 0 && !GameGoing)
            {
                Camera.main.GetComponent<playerFollow>().enabled = false;
                Camera.main.transform.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.3f);
                Camera.main.DOOrthoSize(0.5f, 0.3f);

                shrederUI.SetActive(true);
                check = true;
                GameGoing = true;

                blue1 = (UnityEngine.Random.Range(90, 100)) / 100.0f;
                blue2 = (UnityEngine.Random.Range(50, 100)) / 100.0f;
                red = (UnityEngine.Random.Range(30, 100)) / 100.0f;
                // Debug.Log(blue1); Debug.Log(blue2); Debug.Log(red);
            }
        }
    }


    public void GetLoot()
    {
        CookScript cook = collision.gameObject.GetComponent<CookScript>();
        if (checkIfSlotIsFull(cook.inventory, cook.ActiveSlot) && ((cook.inventory.ElementAt(cook.ActiveSlot).Key == "pureRed") || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "blue") || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "red") || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "green") || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "renANDblue") || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "greenANDblue") || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "Analyzed") || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "Graphed") || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "white")))
        {
            if (!AmIFilled && !GameGoing)
            {
                if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "pureRed")
                {
                    RedNum++;
                    collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                   // requireL.GetComponent<Image>().color = Color.white;
                    pureRedT.text = pureRedNum.ToString();
                }
                else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "blue")
                {
                    blueNum++;
                    collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                   // requirerLL.GetComponent<Image>().color = Color.white;
                    blueT.text = blueNum.ToString();
                }
                else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "red")
                {
                    blueNum++;
                    collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                   // requirerLL.GetComponent<Image>().color = Color.white;
                    redT.text = RedNum.ToString();
                }
                else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "green")
                {
                    blueNum++;
                    collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                    //requirerLL.GetComponent<Image>().color = Color.white;
                    greenT.text =greenNum.ToString();
                }
                else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "redANDblue")
                {
                    blueNum++;
                    collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                   // requirerLL.GetComponent<Image>().color = Color.white;
                    redAndblueT.text = redAndblueNum.ToString();
                }
                else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "greenANDblue")
                {
                    blueNum++;
                    collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                   // requirerLL.GetComponent<Image>().color = Color.white;
                    greenAndblueT.text = greenAndblueNum.ToString();
                }
                else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "Analyzed")
                {
                    blueNum++;
                    collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                    //requirerLL.GetComponent<Image>().color = Color.white;
                    analyzedT.text = analyzedNum.ToString();
                }
                else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "Graphed")
                {
                    blueNum++;
                    collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                    //requirerLL.GetComponent<Image>().color = Color.white;
                    graphedT.text = graphedNum.ToString();
                }
                else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "white")
                {
                    blueNum++;
                    collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                   // requirerLL.GetComponent<Image>().color = Color.white;
                    whiteT.text = whiteNum.ToString();
                }
            }
        }
    }

    
    void DestroyUI()
    {
        shrederUI.SetActive(false);
        GameGoing = false;
        Camera.main.GetComponent<playerFollow>().enabled = false;
        Camera.main.DOOrthoSize(2f, 0.3f);

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

            if (AmIFilled && Input.GetKeyDown(KeyCode.E))
            {
                for (int i = 0; i < CookedResult; i++)
                {
                    cook.GetItem("redANDblue");
                }
                CookedResult = 0;
                AmIFilled = false;
                indicator.GetComponent<Image>().color = Color.white;
            }
        }

    }
}
