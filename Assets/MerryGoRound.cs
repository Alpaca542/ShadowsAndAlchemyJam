using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MerryGoRound : MonoBehaviour
{
    public GameObject indicator;
    public GameObject requirerLL;
    public GameObject requireL;
   // public Image Indicator;
    public Slider requirer;
    public Slider requirer1;
    public Slider requirer2;
   // public Text loss;
    //public Text loss1;
   // public Text loss2;
   // public Text totalLoss;
    bool IHaveBlue = false;
   //float blue1;
   // float blue2;
   // float red;
    public Rigidbody2D[] points;
    bool check;
    float clickCount = 0;
   // GameObject CircleRed;
   // GameObject CircleGreen;
   // GameObject CircleBlue;
    bool clicked = false;
    
    public Text ClickRate;
    float clickrate = 0f;
    void Start()
    {
        
        indicator.GetComponent<Image>().color = Color.red;
        InvokeRepeating(nameof(StopClick), 1f,1f);
        InvokeRepeating(nameof(Winner), 5f, 5f);

        check = true;

    }

    public GameObject shrederUI;
    bool interact = false;
    public GameObject tube;
    Collision2D collision;
    public BoilerScript boiler;
    // bool check = false;

    int blueNum = 0;
    public void BtnClick()
    {
        clickCount += 1;
    }
    void StopClick()
    {
        ClickRate.text = "CPS: "+Convert.ToString(clickCount / 1f);
        clickrate = (clickCount / 1f);
        clickCount = 0;
    }
    private void Winner()
    {
        if(clickrate>=5f)
        {
            check = false;

            //shrederUI.SetActive(false);
            collision.gameObject.GetComponent<CookScript>().UnFreeze();

            // Indicator.color = Color.green;
            requirer.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.green;
            requirer1.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.green;
            requirer2.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.green;
            Invoke(nameof(turnTube), 2f);
            //tube.GetComponent<SpriteRenderer>().color = Color.red;
            // check = false;
            Invoke(nameof(DestroyUI), 2f);
            boiler.enabled = true;
        }
    }
    void Check()
    {
        
        foreach (var p in points)
        {
            p.velocity = p.gameObject.transform.up * 10f;
        }
        float speed = clickrate;

       // CircleBlue.transform.Rotate(0,0,0);
        foreach (var p in points)
        {
            int a = UnityEngine.Random.Range(0, 4);
            if (a == 0)
            {
                p.velocity = p.gameObject.transform.up * speed;
            }
            if (a == 1)
            {
                p.velocity = p.gameObject.transform.right * speed;
            }
            if (a == 2)
            {
                p.velocity = -p.gameObject.transform.right * speed;
            }
            if (a == 3)
            {
                p.velocity = -p.gameObject.transform.up * speed;
            }
        }

       // totalLoss.text = "Average loss: " + Convert.ToString((((int)(requirer.value * 100 - blue1 * 100)) + ((int)(requirer1.value * 100 - blue2 * 100)) + ((int)(requirer2.value * 100 - red * 100))) / 3);
        collision.gameObject.GetComponent<CookScript>().Freeze();
       // Indicator.color = new Color(Mathf.Abs((int)(requirer1.value * 100 - blue2 * 100)) / 255.0f, Mathf.Abs((int)(requirer.value * 100 - blue1 * 100)) / 255.0f, Mathf.Abs((int)(requirer2.value * 100 - red * 100)) / 255.0f, 1);
        Debug.Log("Checking");

        
            //Debug.Log(((requirer.value * 100 - blue1 * 100) + (requirer1.value * 100 - blue2 * 100) + (requirer2.value * 100 - red * 100) / 3));
            
           
        
    }
    void DestroyUI()
    {
        shrederUI.SetActive(false);
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

    public void turnBlue()
    {
        IHaveBlue = true;
        requireL.GetComponent<Image>().color = Color.blue;
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
                        if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "green")
                        {
                            blueNum += 1;
                            collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                            requirerLL.GetComponent<Image>().color = Color.blue;

                        }
                    }
                    if ((blueNum == 1)&&(IHaveBlue))
                    {

                        check = true;
                        shrederUI.SetActive(true);
                    }
                    

                }

            }
        }
        if (check && (collision != null))
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
            collision = null;
        }
    }
}
