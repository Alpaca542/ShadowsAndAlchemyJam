using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class shrederSctipt : MonoBehaviour
{
    public Image AmIFULLIMAGE;
    public bool AmIFilled = false;
    public GameObject shrederUI;
    public GameObject[] spawnPoints;
    bool interact = false;
    Collision2D collision;
   // public BoilerScript boiler;
    public GameObject redMatter;
    public GameObject Impure;
    bool check = false;

    public void Check()
    {
        Debug.Log("Checking");
        int k = 0;
        foreach (Transform lol in gameObject.GetComponentsInChildren<Transform>())
        {
            if (lol.gameObject.CompareTag("RedBTN"))
            {
                k++;
            }
        }

        if (k == 0)
        {
            check = false;
            AmIFilled = true;
            shrederUI.SetActive(false);
           // tube.GetComponent<SpriteRenderer>().color = Color.red;
            Invoke(nameof(turnTube), 1f);
           collision.gameObject.GetComponent<CookScript>().UnFreeze();
            
            AmIFULLIMAGE.color = Color.red;
        }
    }

    void turnTube()
    {
        //tube.GetComponent<SpriteRenderer>().color = Color.white;
       // boiler.enabled = true;
    }

    void Update()
    {
        if (collision != null && collision.gameObject.tag == "Cook")
        {
            CookScript cook = collision.gameObject.GetComponent<CookScript>();
            if (interact && cook.ActiveSlot < cook.inventory.Count && cook.inventory.ElementAt(cook.ActiveSlot).Key == "red")
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (!AmIFilled)
                    {
                        shrederUI.SetActive(true);
                        cook.Freeze();
                        ClearMePlease();
                        cook.RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                        interact = false;
                        check = true;
                        
                    }

                }
                

            }
            if (AmIFilled&& (Input.GetKeyDown(KeyCode.Space)))
            {
                cook.GetItem("pureRed");
                AmIFilled = false;
                AmIFULLIMAGE.color = Color.white;

            }
            if (check) { Check(); }
        }
    }
    public void ClearMePlease()
    {
        foreach (GameObject go in spawnPoints)
        {
            if ((Random.Range(0, 3) == 0) || (Random.Range(0, 3) == 1))
            {
                if (Random.Range(0, 2) == 0)
                {
                    Instantiate(Impure, go.transform.position, Quaternion.identity, shrederUI.transform);
                }
                else
                {
                    Instantiate(redMatter, go.transform.position, Quaternion.identity, shrederUI.transform);
                }
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CookScript cck = collision.gameObject.GetComponent<CookScript>();
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
            //collision.gameObject.GetComponent<CookScript>().UnFreeze();
            interact = false;
            collision = null;
        }
    }
}
