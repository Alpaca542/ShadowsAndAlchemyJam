using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

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
    public int blueNum;
    public int redNum;
    bool GameGoing;

    float CookedResult;

    public GameObject requireL;
    public GameObject requireLL;

    public TMP_Text requireLTxt;
    public TMP_Text requireLLTxt;

    public GameObject indicator;

    bool checkIfSlotIsFull(Dictionary<string, int> inv, int slot)
    {
        return slot < inv.Count;
    }
    private void Update()
    {
        if (collision != null && collision.gameObject.tag == "Cook")
        {
            CookScript cook = collision.gameObject.GetComponent<CookScript>();

            if (AmIFilled && Input.GetKeyDown(KeyCode.E))
            {
                for (int i = 0; i < CookedResult; i++)
                {
                    cook.GetItem("Analyzed");
                }
                CookedResult = 0;
                AmIFilled = false;
                indicator.GetComponent<Image>().color = Color.white;

            }
        }
    }
    public void GetStarted()
    {
        CookScript cook = collision.gameObject.GetComponent<CookScript>();
        if (!AmIFilled && !GameGoing)
        {
            if (blueNum > 0 && redNum > 0 && !GameGoing)
            {
                GameGoing = true;
                myUI.SetActive(true);
                Camera.main.transform.parent.GetComponent<playerFollow>().enabled = false;
                Camera.main.transform.parent.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.3f);
                Camera.main.DOOrthoSize(0.5f, 0.3f);
                cook.Freeze();
                StartTask();
            }
        }
    }

    public void GetLoot()
    {
        CookScript cook = collision.gameObject.GetComponent<CookScript>();
        if (checkIfSlotIsFull(cook.inventory, cook.ActiveSlot) && ((cook.inventory.ElementAt(cook.ActiveSlot).Key == "Graphed") || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "blue")))
        {
            if (!AmIFilled && !GameGoing)
            {
                if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "Graphed")
                {
                    redNum++;
                    collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                    requireL.GetComponent<Image>().color = Color.white;
                    requireLTxt.text = redNum.ToString();
                }
                else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "blue")
                {
                    blueNum++;
                    collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                    requireLL.GetComponent<Image>().color = Color.white;
                    requireLLTxt.text = blueNum.ToString();
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
        if (totalAmount <= 0)
        {
            requireLL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            requireL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            if (redNum > blueNum)
            {
                redNum -= blueNum;
                CookedResult = blueNum;
                blueNum = 0;
                requireL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            }
            else if (redNum < blueNum)
            {
                blueNum -= redNum;
                CookedResult = redNum;
                redNum = 0;
                requireLL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            }
            else
            {
                CookedResult = blueNum;
                redNum = 0;
                blueNum = 0;
                requireLL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
                requireL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            }
            AmIFilled = true;

            requireLLTxt.text = redNum.ToString();
            requireLTxt.text = blueNum.ToString();

            indicator.GetComponent<Image>().color = Color.red;
            myUI.SetActive(false);
            GetComponent<soundManager>().PlaySound(0, 0.7f, 1.3f);
            Camera.main.transform.parent.GetComponent<playerFollow>().enabled = true;
            Camera.main.DOOrthoSize(2f, 0.3f);

            collision.gameObject.GetComponent<CookScript>().UnFreeze();
            GameGoing = false;
        }
    }
}
