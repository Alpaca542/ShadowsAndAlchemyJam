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
    public GameObject tube;
    public GameObject collision;
    public BoilerScript boiler;
    //public GameObject require1;
    private int totalAmount;

    void turnTube()
    {
        Debug.Log("analyzerWin");
    }

    public void GetStarted()
    {
        if (collision != null && collision.gameObject.tag == "Cook")
        {
            CookScript cook = collision.gameObject.GetComponent<CookScript>();
            if (cook.ActiveSlot < cook.inventory.Count && cook.inventory.ElementAt(cook.ActiveSlot).Key == "green")
            {
                //require1.gameObject.GetComponent<Image>().color = Color.blue;
                myUI.SetActive(true);
                cook.Freeze();
                StartTask();
                cook.RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
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
}
