using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BoilerScript : MonoBehaviour
{
    public bool AmIFilled;
    public GameObject indicator;
    public GameObject requirerLL;
    public GameObject requireL;
    public Image Indicator;
    public Slider requirer;
    public Slider requirer1;
    public Slider requirer2;
    public Text loss;
    public Text loss1;
    public Text loss2;
    public Text totalLoss;

    float blue1;
    float blue2;
    float red;

    bool check;


    public GameObject shrederUI;
    public GameObject collision;

    bool blueNum = false;
    bool RedNum = false;

    public void GetStarted()
    {
        CookScript cook = collision.gameObject.GetComponent<CookScript>();
        if (checkIfSlotIsFull(cook.inventory, cook.ActiveSlot) && ((cook.inventory.ElementAt(cook.ActiveSlot).Key == "blue" && !blueNum) || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "pureRed" && !RedNum)))
        {
            if (!AmIFilled)
            {
                if (checkIfSlotIsFull(cook.inventory, cook.ActiveSlot))
                {
                    if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "blue")
                    {
                        blueNum = true;
                        requirerLL.gameObject.GetComponent<Image>().color = Color.blue;
                        collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);

                    }
                    else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "pureRed")
                    {
                        RedNum = true;
                        requireL.gameObject.GetComponent<Image>().color = Color.red;
                        collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                    }
                }

                if (blueNum && RedNum)
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
        loss.text = "loss: " + Convert.ToString((int)(requirer.value * 100 - blue1 * 100));
        loss1.text = "loss: " + Convert.ToString((int)(requirer1.value * 100 - blue2 * 100));
        loss2.text = "loss: " + Convert.ToString((int)(requirer2.value * 100 - red * 100));

        totalLoss.text = "Average loss: " + Convert.ToString((((int)(requirer.value * 100 - blue1 * 100)) + ((int)(requirer1.value * 100 - blue2 * 100)) + ((int)(requirer2.value * 100 - red * 100))) / 3);
        collision.gameObject.GetComponent<CookScript>().Freeze();
        Indicator.color = new Color(Mathf.Abs((int)(requirer1.value * 100 - blue2 * 100)) / 255.0f, Mathf.Abs((int)(requirer.value * 100 - blue1 * 100)) / 255.0f, Mathf.Abs((int)(requirer2.value * 100 - red * 100)) / 255.0f, 1);
        Debug.Log("Checking");

        if (Mathf.Abs((((requirer.value * 100 - blue1 * 100) + (requirer1.value * 100 - blue2 * 100) + (requirer2.value * 100 - red * 100))) / 3) <= 10)
        {
            Debug.Log(((requirer.value * 100 - blue1 * 100) + (requirer1.value * 100 - blue2 * 100) + (requirer2.value * 100 - red * 100) / 3));

            check = false;

            //shrederUI.SetActive(false);


            Indicator.color = Color.green;
            requirer.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.green;
            requirer1.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.green;
            requirer2.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.green;

            // check = false;
            Invoke(nameof(DestroyUI), 2f);
            AmIFilled = true;
            indicator.GetComponent<Image>().color = Color.red;
            requirerLL.gameObject.GetComponent<Image>().color = Color.white;
            requireL.gameObject.GetComponent<Image>().color = Color.white;
            RedNum = false;
            blueNum = false;

        }
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
                cook.GetItem("redANDblue");
                AmIFilled = false;
                indicator.GetComponent<Image>().color = Color.white;

            }
        }
        if (check && (collision != null))
        {
            Check();
        }
    }
}
