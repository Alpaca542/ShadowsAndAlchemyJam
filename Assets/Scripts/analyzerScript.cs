using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class analyzercript : MonoBehaviour
{
    public GameObject myUI;
    public GameObject[] spawnPoints;
    public GameObject[] bacterias;
    public GameObject tube;
    public GameObject collision;
    public BoilerScript boiler;
    private int totalAmount;
    private bool AmIFilled;
    public bool blueNum;
    public bool redNum;

    public GameObject requireL;
    public GameObject requireLL;

    public GameObject indicator;


    void turnTube()
    {
        Debug.Log("analyzerWin");
    }

    bool checkIfSlotIsFull(Dictionary<string, int> inv, int slot)
    {
        return slot < inv.Count;
    }
    private void Update()
    {
        if (collision != null && collision.gameObject.tag == "Cook")
        {
            CookScript cook = collision.gameObject.GetComponent<CookScript>();

            if (AmIFilled && (Input.GetKeyDown(KeyCode.E)))
            {
                cook.GetItem("Analyzed");
                AmIFilled = false;
                indicator.GetComponent<Image>().color = Color.white;

            }
        }
    }
    public void GetStarted()
    {
        CookScript cook = collision.gameObject.GetComponent<CookScript>();
        if (checkIfSlotIsFull(cook.inventory, cook.ActiveSlot) && ((cook.inventory.ElementAt(cook.ActiveSlot).Key == "blue" && !blueNum) || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "Graphed" && !redNum)))
        {
            if (!AmIFilled)
            {
                if (checkIfSlotIsFull(cook.inventory, cook.ActiveSlot))
                {
                    if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "blue")
                    {
                        blueNum = true;
                        requireLL.gameObject.GetComponent<Image>().color = Color.white;
                        collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);

                    }
                    else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "Graphed")
                    {
                        redNum = true;
                        requireL.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                        collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                    }
                }

                if (blueNum && redNum)
                {
                    myUI.SetActive(true);
                    Camera.main.GetComponent<playerFollow>().enabled = false;
                    Camera.main.transform.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.3f);
                    Camera.main.DOOrthoSize(0.5f, 0.3f);
                    cook.Freeze();
                    StartTask();
                    cook.RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);

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
            requireLL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            requireL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            indicator.GetComponent<Image>().color = Color.red;
            myUI.SetActive(false);
            Camera.main.GetComponent<playerFollow>().enabled = false;
            Camera.main.DOOrthoSize(2f, 0.3f);
            collision.gameObject.GetComponent<CookScript>().UnFreeze();
            turnTube();
        }
    }
}
