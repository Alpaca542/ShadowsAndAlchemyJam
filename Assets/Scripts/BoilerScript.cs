using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class BoilerScript : MonoBehaviour
{
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

    bool GameGoing = false;

    public void GetStarted()
    {
        CookScript cook = collision.gameObject.GetComponent<CookScript>();
        if (!AmIFilled)
        {
            if (blueNum > 0 && RedNum > 0 && !GameGoing)
            {
                Camera.main.transform.parent.GetComponent<playerFollow>().enabled = false;
                Camera.main.transform.parent.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.3f);
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
        if (checkIfSlotIsFull(cook.inventory, cook.ActiveSlot) && ((cook.inventory.ElementAt(cook.ActiveSlot).Key == "pureRed") || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "blue")))
        {
            if (!AmIFilled && !GameGoing)
            {
                if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "pureRed")
                {
                    RedNum++;
                    collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                    requireL.GetComponent<Image>().color = Color.white;
                    requireLTxt.text = RedNum.ToString();
                }
                else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "blue")
                {
                    blueNum++;
                    collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                    requirerLL.GetComponent<Image>().color = Color.white;
                    requirerLLTxt.text = blueNum.ToString();
                }
            }
        }
    }

    void Check()
    {
        loss.text = "loss: " + Convert.ToString((int)(requirer.value * 100 - blue1 * 100));
        loss1.text = "loss: " + Convert.ToString((int)(requirer1.value * 100 - blue2 * 100));
        loss2.text = "loss: " + Convert.ToString((int)(requirer2.value * 100 - red * 100));


        totalLoss.text = "Average loss: " + Convert.ToString((((int)(requirer.value * 100 - blue1 * 100)) + ((int)(requirer1.value * 100 - blue2 * 100)) + ((int)(requirer2.value * 100 - red * 100))) / 3);
        collision.gameObject.GetComponent<CookScript>().Freeze();

        //Indicator.color = new Color(Mathf.Abs((int)(requirer1.value * 100 - blue2 * 100)) / 255.0f, Mathf.Abs((int)(requirer.value * 100 - blue1 * 100)) / 255.0f, Mathf.Abs((int)(requirer2.value * 100 - red * 100)) / 255.0f, 1);
        Debug.Log("Checking");

        if ((Mathf.Abs((int)(requirer.value * 100 - blue1 * 100)) <= 10.0f) && (Mathf.Abs(((int)(requirer1.value * 100 - blue2 * 100))) <= 10.0f) && (Mathf.Abs(((int)(requirer2.value * 100 - red * 100))) <= 10.0f))
        {
            Debug.Log(((requirer.value * 100 - blue1 * 100) + (requirer1.value * 100 - blue2 * 100) + (requirer2.value * 100 - red * 100) / 3));

            check = false;

            //shrederUI.SetActive(false);


            Indicator.color = Color.green;


            // check = false;
            Invoke(nameof(DestroyUI), 2f);
            AmIFilled = true;
            indicator.GetComponent<Image>().color = Color.red;
            requirerLL.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            requireL.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            if (RedNum > blueNum)
            {
                RedNum -= blueNum;
                CookedResult = blueNum;
                blueNum = 0;
                requireL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            }
            else if (RedNum < blueNum)
            {
                blueNum -= RedNum;
                CookedResult = RedNum;
                RedNum = 0;
                requirerLL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            }
            else
            {
                CookedResult = blueNum;
                RedNum = 0;
                blueNum = 0;
                requirerLL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
                requireL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            }
            requirerLLTxt.text = RedNum.ToString();
            requireLTxt.text = blueNum.ToString();
        }
    }
    void DestroyUI()
    {
        shrederUI.SetActive(false);
        GameGoing = false;
        Camera.main.transform.parent.GetComponent<playerFollow>().enabled = true;
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
        if (check && (collision != null))
        {
            Check();
        }
        if (collision != null)
        {
            // Check();
            rot1.Rotate(0, 0, Mathf.Abs(requirer.value * 100 - blue1 * 100 * Time.deltaTime) / 100f);
            rot2.Rotate(0, 0, Mathf.Abs(requirer1.value * 100 - blue2 * 100 * Time.deltaTime) / 100f);
            rot3.Rotate(0, 0, Mathf.Abs(requirer2.value * 100 - red * 100 * Time.deltaTime) / 100f);
            rot4.Rotate(0, 0, Mathf.Abs(requirer.value * 100 - blue1 * 100 * Time.deltaTime) / 40f);
            rot5.Rotate(0, 0, Mathf.Abs(requirer2.value * 100 - blue1 * 100 * Time.deltaTime) / 70f);
        }
    }
}
