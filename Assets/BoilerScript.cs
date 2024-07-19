using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BoilerScript : MonoBehaviour
{
    public GameObject indicator;
    public GameObject requirerLL;
    public GameObject requireêL;

    public Scrollbar requirer;
    public Scrollbar requirer1;
    public Scrollbar requirer2;
    public Text loss;
    public Text loss1;
    public Text loss2;
    public Text totalLoss;

    float blue1;
    float blue2;
    float red;

    bool check;
    void Start()
    {
        indicator.GetComponent<Image>().color = Color.red;
    }

    public GameObject shrederUI;
    bool interact = false;
    public GameObject tube;
    Collision2D collision;
    public BoilerScript boiler;
    // bool check = false;

    int blueNum = 0;

    void Check()
    {
        loss.text = "loss: " + Convert.ToString((int)(requirer.value * 100 - blue1 * 100) );
        loss1.text = "loss: " + Convert.ToString((int)(requirer1.value * 100 - blue2 * 100));
        loss2.text = "loss: " + Convert.ToString((int)(requirer2.value * 100 - red * 100));
        
        totalLoss.text = "Average loss: "+Convert.ToString((((int)(requirer.value * 100 - blue1 * 100))+ ((int)(requirer1.value * 100 - blue2 * 100))+ ((int)(requirer2.value * 100 - red * 100)))/3);
        collision.gameObject.GetComponent<CookScript>().Freeze();


        if (Mathf.Abs(((((int)(requirer.value * 100 - blue1 * 100)) + ((int)(requirer1.value * 100 - blue2 * 100)) + ((int)(requirer2.value * 100 - red * 100)))) / 3) <10) ;
        {

            //shrederUI.SetActive(false);
            tube.GetComponent<SpriteRenderer>().color = Color.red;
            Invoke(nameof(turnTube), 1f);
            shrederUI.SetActive(false);
            collision.gameObject.GetComponent<CookScript>().UnFreeze();
            
            
            boiler.enabled = true;
            check = false;
            // check = false;
        }
    }

    void turnTube()
    {
        
        tube.GetComponent<SpriteRenderer>().color = Color.white;
        boiler.enabled = true;
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
            if (interact && checkIfSlotIsFull(cook.inventory, cook.ActiveSlot) && cook.inventory.ElementAt(cook.ActiveSlot).Key == "blue")
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //shrederUI.SetActive(true);
                    //collision.gameObject.GetComponent<CookScript>().Freeze();
                    if (checkIfSlotIsFull(cook.inventory, cook.ActiveSlot))
                    {
                        if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "blue")
                        {
                            blueNum += 1;
                            collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                        }
                    }
                    if (blueNum == 1)
                    {
                        requirerLL.gameObject.GetComponent<Image>().color = Color.blue;
                    }
                    if (blueNum == 2)
                    {
                        requireêL.gameObject.GetComponent<Image>().color = Color.blue;
                        blue1 = (UnityEngine.Random.Range(0, 100)) / 100.0f;
                        blue2 = (UnityEngine.Random.Range(0, 100)) / 100.0f;
                        red = (UnityEngine.Random.Range(0, 100))/100.0f;
                        Debug.Log(blue1); Debug.Log(blue2); Debug.Log(red);
                        check = true;
                    }
                    
                }
                
            }
        }
        if (check)
        {
            Check();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cook"))
        {
            interact = true;
            this.collision = collision;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cook"))
        {
            interact = false;
        }
    }
}
