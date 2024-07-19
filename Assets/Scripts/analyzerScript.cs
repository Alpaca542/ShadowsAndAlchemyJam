using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class analyzercript : MonoBehaviour
{
    public GameObject myUI;
    public GameObject[] spawnPoints;
    bool interact = false;
    public GameObject tube;
    Collision2D collision;
    public BoilerScript boiler;
    public GameObject redMatter;
    public GameObject Impure;
    public GameObject require1;

    void turnTube()
    {
        tube.GetComponent<SpriteRenderer>().color = Color.white;
        boiler.enabled = true;
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
                    require1.gameObject.GetComponent<Image>().color = Color.blue;
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
        // foreach(GameObject gmb in spawnPoints)
    }

    public void FoundOne()
    {
        //
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
            collision.gameObject.GetComponent<CookScript>().UnFreeze();
        }
    }
}
