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
    public Transform rot1;
    public Transform rot2;
    public Transform rot3;
    public Transform rot4;
    public Transform rot5;
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
                        requirerLL.gameObject.GetComponent<Image>().color = new Color32(0, 0, 255, 255);
                        collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);

                    }
                    else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "pureRed")
                    {
                        RedNum = true;
                        requireL.gameObject.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
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

        //Indicator.color = new Color(Mathf.Abs((int)(requirer1.value * 100 - blue2 * 100)) / 255.0f, Mathf.Abs((int)(requirer.value * 100 - blue1 * 100)) / 255.0f, Mathf.Abs((int)(requirer2.value * 100 - red * 100)) / 255.0f, 1);
        Debug.Log("Checking");

        if ((Mathf.Abs((int)(requirer.value * 100 - blue1 * 100))<=10.0f)&& (Mathf.Abs(((int)(requirer1.value * 100 - blue2 * 100))) <= 10.0f)&& (Mathf.Abs(((int)(requirer2.value * 100 - red * 100))) <= 10.0f))
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
                
                AmIFilled = false;
                indicator.GetComponent<Image>().color = Color.white;
                cook.GetItem("redANDblue");
                Debug.Log("AfterGive");

            }
        }
        if (check && (collision != null))
        {
            Check();
        }
        if(collision!=null)
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
