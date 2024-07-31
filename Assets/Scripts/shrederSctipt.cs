using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class shrederSctipt : MonoBehaviour
{
    public Image indicator;
    public Image requireL;
    public TMP_Text requireLTxt;
    private float redNum;
    private float CookedResult;
    bool GameGoing;
    public bool AmIFilled = false;
    public GameObject shrederUI;
    public GameObject[] spawnPoints;
    public GameObject collision;
    // public BoilerScript boiler;
    public GameObject redMatter;
    public GameObject Impure;
    bool check = false;
    bool active = false;
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
            CookedResult = redNum;
            requireLTxt.text = "0";
            redNum = 0;
            GameGoing = false;
            requireL.color = new Color32(255, 255, 255, 150);
            AmIFilled = true;
            indicator.color = Color.red;
            shrederUI.SetActive(false);
            GetComponent<soundManager>().PlaySound(0, 0.9f, 1.1f);
            // tube.GetComponent<SpriteRenderer>().color = Color.red;

            Camera.main.transform.parent.GetComponent<playerFollow>().enabled = true;
            Camera.main.DOOrthoSize(2f, 0.3f);

            collision.gameObject.GetComponent<CookScript>().UnFreeze();
            active = false;
            requireL.color = new Color32(255, 255, 255, 255);
            check = false;
            ClearAllPoints();
        }
    }

    void Update()
    {
        if (collision != null && collision.gameObject.tag == "Cook")
        {
            CookScript cook = collision.gameObject.GetComponent<CookScript>();

            if (AmIFilled && Input.GetKeyDown(KeyCode.E))
            {
                for (int i = 0; i < CookedResult; i++)
                {
                    cook.GetItem("pureRed");
                }
                AmIFilled = false;
                indicator.color = Color.white;
                CookedResult = 0;
            }
            if (check) { Check(); }
        }
    }

    public void GetStarted()
    {
        CookScript cook = collision.gameObject.GetComponent<CookScript>();
        if (!AmIFilled && !GameGoing&&(redNum>0))
        {
            GameGoing = true;
            Camera.main.transform.parent.GetComponent<playerFollow>().enabled = false;
            Camera.main.transform.parent.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.3f);
            Camera.main.DOOrthoSize(0.5f, 0.3f);
            shrederUI.SetActive(true);
            cook.Freeze();
            ClearMePlease();
            check = true;
            active = true;

        }
    }

    public void GetLoot()
    {
        CookScript cook = collision.gameObject.GetComponent<CookScript>();
        if (checkIfSlotIsFull(cook.inventory, cook.ActiveSlot) && cook.inventory.ElementAt(cook.ActiveSlot).Key == "red")
        {
            if (!AmIFilled && !GameGoing)
            {
                redNum++;
                collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                requireL.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                requireLTxt.text = redNum.ToString();
            }
        }
    }

    bool checkIfSlotIsFull(Dictionary<string, int> inv, int slot)
    {
        return slot < inv.Count;
    }

    void ClearAllPoints()
    {
        foreach (var tr in shrederUI.transform.GetComponentsInChildren<Transform>())
        {
            if (tr != shrederUI.transform.GetComponentsInChildren<Transform>()[0])
                if (tr.gameObject.CompareTag("RedBTN") || tr.gameObject.CompareTag("RP"))
                {
                    Destroy(tr.gameObject);
                }

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
}
