using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoilerScript : MonoBehaviour
{
    public GameObject indicator;
    public GameObject requirer;
    public GameObject requirer1;
    // Start is called before the first frame update
    void Start()
    {
        indicator.GetComponent<Image>().color = Color.red;
    }

    // Update is called once per frame
    public GameObject shrederUI;
    bool interact = false;
    public GameObject tube;
    Collision2D collision;
    public BoilerScript boiler;
    bool check = false;

    int blueNum = 0;

    void Check()
    {
        int k = 0;
        foreach (Transform lol in gameObject.GetComponentsInChildren<Transform>())
        {
            if (lol.gameObject.CompareTag("RedBTN"))
            {
                k++;
            }
        }

        if ((k == 0))
        {

            //shrederUI.SetActive(false);
            tube.GetComponent<SpriteRenderer>().color = Color.red;
            Invoke(nameof(turnTube), 1f);
            collision.gameObject.GetComponent<CookScript>().UnFreeze();
            check = false;
        }
    }

    void turnTube()
    {
        tube.GetComponent<SpriteRenderer>().color = Color.white;
        boiler.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (interact)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {

                //shrederUI.SetActive(true);
                //collision.gameObject.GetComponent<CookScript>().Freeze();
                if ((collision.gameObject.GetComponent<CookScript>().WhatIHave == "blue"))
                {
                    blueNum += 1;
                    collision.gameObject.GetComponent<CookScript>().WhatIHave = "0";
                }
                if(blueNum ==1)
                {
                    requirer1.gameObject.GetComponent<Image>().color = Color.blue;
                }
                if (blueNum == 2)
                {
                    requirer.gameObject.GetComponent<Image>().color = Color.blue;
                }
                if(blueNum > 2)
                {
                    turnTube();
                }
            }

        }
       

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cook"))
        {

            if ((collision.gameObject.GetComponent<CookScript>().WhatIHave == "blue"))
            {
                interact = true;
                this.collision = collision;
            }
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
