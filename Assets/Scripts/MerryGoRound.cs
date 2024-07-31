using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MerryGoRound : MonoBehaviour
{

    public GameObject particle;
    public GameObject lp;
    public Transform[] randomPoint;
    public Image glass;
    public GameObject indicator;
    public GameObject requirerLL;
    public GameObject requireL;
    public TMP_Text requirerLLTxt;
    public TMP_Text requireLTxt;
    public Slider requirer;
    public Slider requirer1;
    public Slider requirer2;
    bool check;
    float clickCount = 0;
    public Text ClickRate;
    float clickrate = 0f;
    float CookedResult;

    bool AmIFilled;

    public GameObject shrederUI;
    public GameObject collision;

    int blueNum = 0;
    int RedNum = 0;
    bool GameGoing;

    public void BtnClick()
    {
        GetComponent<soundManager>().PlaySound(0, 0.8f, 1.2f);
        clickCount += 1;
    }
    void StopClick()
    {
        ClickRate.text = "CPS: " + Convert.ToString(clickCount / 1f);
        clickrate = (clickCount / 1f);
        clickCount = 0;

    }
    void BRRRR()
    {
        lp.gameObject.transform.position = glass.transform.position;
        
    }

    public void GetLoot()
    {
        CookScript cook = collision.gameObject.GetComponent<CookScript>();
        if (checkIfSlotIsFull(cook.inventory, cook.ActiveSlot) && ((cook.inventory.ElementAt(cook.ActiveSlot).Key == "green") || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "redANDblue")))
        {
            if (!AmIFilled && !GameGoing)
            {
                if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "redANDblue")
                {
                    RedNum++;
                    collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                    requireL.GetComponent<Image>().color = Color.white;
                    requireLTxt.text = RedNum.ToString();
                }
                else if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "green")
                {
                    blueNum++;
                    collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                    requirerLL.GetComponent<Image>().color = Color.white;
                    requirerLLTxt.text = blueNum.ToString();
                }
            }
        }
    }

    private void Winner()
    {
        if (clickrate >= 5f)
        {
            check = false;
            GameGoing = false;
            GetComponent<soundManager>().PlaySound(1, 0.9f, 1.1f);
            requirerLL.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            requireL.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            indicator.GetComponent<Image>().color = Color.red;
            shrederUI.SetActive(false);
            Camera.main.transform.parent.GetComponent<playerFollow>().enabled = true;
            Camera.main.DOOrthoSize(2f, 0.3f);
            collision.gameObject.GetComponent<CookScript>().UnFreeze();
            //indicator.GetComponent<Image>().color = Color.red;
            AmIFilled = true;
            if (RedNum > blueNum)
            {
                RedNum -= blueNum;
                CookedResult = blueNum;
                blueNum = 0;
                requireL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            }
            else if (RedNum < blueNum)
            {
                blueNum -= RedNum;
                CookedResult = RedNum;
                RedNum = 0;
                requirerLL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            }
            else
            {
                CookedResult = blueNum;
                RedNum = 0;
                blueNum = 0;
                requirerLL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
                requireL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            }
            requirerLLTxt.text = RedNum.ToString();
            requireLTxt.text = blueNum.ToString();
            CancelInvoke(nameof(BRRRR));
            CancelInvoke(nameof(Winner));
            CancelInvoke(nameof(StopClick));
        }
    }
    void Check()
    {
        float speed = clickrate / 3;
        glass.color = new Color(0, 1 * clickrate / 5 + 1, 0, 1 * clickrate / 5 + 1);
        // CircleBlue.transform.Rotate(0,0,0);
        particle.GetComponent<ParticleSystem>().startSpeed = clickrate * 10f;
        collision.gameObject.GetComponent<CookScript>().Freeze();
    }
    void DestroyUI()
    {
        requirerLL.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
        requireL.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
        indicator.GetComponent<Image>().color = Color.red;
        shrederUI.SetActive(false);
        collision.gameObject.GetComponent<CookScript>().UnFreeze();
    }
    bool checkIfSlotIsFull(Dictionary<string, int> inv, int slot)
    {
        return slot < inv.Count;
    }

    public void turnBlue()
    {
        requireL.GetComponent<Image>().color = Color.blue;
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
                    cook.GetItem("greenANDblue");
                }
                CookedResult = 0;
                AmIFilled = false;
                indicator.GetComponent<Image>().color = Color.white;

            }
        }
        if (check && (collision != null))
        {
            Check();
        }
    }

    public void GetStarted()
    {
        CookScript cook = collision.gameObject.GetComponent<CookScript>();
        if (!AmIFilled)
        {
            if (blueNum > 0 && RedNum > 0 && !GameGoing)
            {
                GameGoing = true;
                check = true;
                shrederUI.SetActive(true);
                Camera.main.transform.parent.GetComponent<playerFollow>().enabled = false;
                Camera.main.transform.parent.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.3f);
                Camera.main.DOOrthoSize(0.5f, 0.3f);
                collision.gameObject.GetComponent<CookScript>().Freeze();

                InvokeRepeating(nameof(StopClick), 1f, 1f);
                InvokeRepeating(nameof(Winner), 5f, 5f);
                InvokeRepeating(nameof(BRRRR), 0.3f, 0.3f);

            }
        }
    }
}
