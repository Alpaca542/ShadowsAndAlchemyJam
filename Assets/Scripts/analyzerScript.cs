using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class analyzercript : MonoBehaviour
{
    public GameObject myUI;
    public GameObject[] spawnPoints;
    public GameObject[] bacterias;
    bool interact = false;
    public GameObject tube;
    Collision2D collision;
    public BoilerScript boiler;
    //public GameObject require1;
    private int totalAmount;

    void turnTube()
    {
        tube.GetComponent<SpriteRenderer>().color = Color.white;
    }

    void Update()
    {
        if (collision != null && collision.gameObject.tag == "Cook")
        {
            CookScript cook = collision.gameObject.GetComponent<CookScript>();
            if (interact && cook.ActiveSlot < cook.inventory.Count && cook.inventory.ElementAt(cook.ActiveSlot).Key == "green")
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //require1.gameObject.GetComponent<Image>().color = Color.blue;
                    myUI.SetActive(true);
                    cook.Freeze();
                    StartTask();
                    cook.RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                    interact = false;
                }

            }
        }
    }

    public void StartTask()
    {
        totalAmount = 5;
        for (int i = 0; i < totalAmount; i++)
        {
            GameObject go = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
            if (Random.Range(0, 2) == 0)
            {
                Instantiate(bacterias[0], go.transform.position, Quaternion.identity, myUI.transform);
            }
            else
            {
                Instantiate(bacterias[1], go.transform.position, Quaternion.identity, myUI.transform);
            }
        }
    }

    public void FoundOne(GameObject found)
    {
        totalAmount--;
        found.tag = "Untagged";
        found.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        if (totalAmount == 0)
        {
            myUI.SetActive(false);
            turnTube();
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
            collision.gameObject.GetComponent<CookScript>().UnFreeze();
        }
    }
}
