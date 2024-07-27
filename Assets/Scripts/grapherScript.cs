using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class grapherScript : MonoBehaviour
{
    bool AmIFilled;
    private int CookedResult;
    public Image shower;
    public Slider sliderA;
    public Slider sliderB;
    public Slider sliderC;
    bool GameGoing;
    float rndA;
    float rndB;
    float rndC;
    float rndAUSER;
    float rndBUSER;
    float rndCUSER;
    public GameObject lp;
    public Transform[] randomPoint;
    public Image glass;
    public GameObject indicator;
    public GameObject requirerLL;
    public GameObject requireL;

    public TMP_Text requirerLLTxt;
    public TMP_Text requireLTxt;
    public LineRenderer lineRenderer;

    public int resolution = 10; // ���������� ����� �� �������
    public float xMin = -5f;     // ����������� �������� �� ��� X
    public float xMax = 5f;      // ������������ �������� �� ��� X
    public float yMin = -5f;     // ����������� �������� �� ��� Y
    public float yMax = 5f;      // ������������ �������� �� ��� Y
    bool check;
    public LineRenderer lineRenderer2;

    public int resolution2 = 10; // ���������� ����� �� �������
    public float xMin2 = -160f;     // ����������� �������� �� ��� X
    public float xMax2 = 160f;      // ������������ �������� �� ��� X
    public float yMin2 = -160f;     // ����������� �������� �� ��� Y
    public float yMax2 = 160f;      // ������������ �������� �� ��� Y

    float Function(float x)
    {
        return x * x;
    }

    float clickrate = 0f;


    public GameObject shrederUI;
    public GameObject tube;
    public GameObject collision;
    public BoilerScript boiler;
    // bool check = false;

    int blueNum = 0;
    int redNum = 0;
    private void Winner()
    {
        if (clickrate >= 5f)
        {
            check = false;
            collision.gameObject.GetComponent<CookScript>().UnFreeze();
            Invoke(nameof(DestroyUI), 2f);
            boiler.enabled = true;

        }
    }

    public void GetLoot()
    {
        CookScript cook = collision.gameObject.GetComponent<CookScript>();
        if (checkIfSlotIsFull(cook.inventory, cook.ActiveSlot) && ((cook.inventory.ElementAt(cook.ActiveSlot).Key == "green") || (cook.inventory.ElementAt(cook.ActiveSlot).Key == "greenANDblue")))
        {
            if (!AmIFilled && !GameGoing)
            {
                if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "greenANDblue")
                {
                    redNum++;
                    collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                    requireL.GetComponent<Image>().color = Color.white;
                    requireLTxt.text = redNum.ToString();
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

    void Update()
    {
        if (collision != null && collision.gameObject.tag == "Cook")
        {
            CookScript cook = collision.gameObject.GetComponent<CookScript>();
            Debug.Log(123);
            if (AmIFilled && Input.GetKeyDown(KeyCode.E))
            {
                for (int i = 0; i < CookedResult; i++)
                {
                    cook.GetItem("Graphed");
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
    void Check()
    {
        lineRenderer2.positionCount = resolution2 + 1; // +1 ��� ���������� �����

        for (int i = 0; i <= resolution2; i++)
        {
            float x = Mathf.Lerp(xMin2, xMax2, i / (float)resolution2);
            float y = fUSER(x); // ���� �������
                                // y = Mathf.Clamp(y, yMin, yMax);
            lineRenderer2.SetPosition(i, new Vector3(x / 100.0f, y / 100.0f, 0));
        }
        if (CalculateAverage() < 10f)
        {
            check = false;
            Invoke(nameof(DestroyUI), 1f);
            indicator.gameObject.GetComponent<Image>().color = Color.red;
            shower.color = Color.green;
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
                requirerLL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            }
            else
            {
                CookedResult = blueNum;
                redNum = 0;
                blueNum = 0;
                requirerLL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
                requireL.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            }
            requirerLLTxt.text = redNum.ToString();
            requireLTxt.text = blueNum.ToString();
            AmIFilled = true;
        }
    }
    void DestroyUI()
    {
        shrederUI.SetActive(false);
        GameGoing = false;
        Camera.main.GetComponent<playerFollow>().enabled = false;
        Camera.main.DOOrthoSize(2f, 0.3f);
        collision.gameObject.GetComponent<CookScript>().UnFreeze();
        shower.color = Color.white;
    }

    float CalculateAverage()
    {
        int k = 0;
        float sm = 0;
        for (int i = 0; i <= 10; i++)
        {
            sm += (float)Math.Pow((lineRenderer.GetPosition(i) - lineRenderer2.GetPosition(i)).magnitude, 2);
            k += 1;
        }
        return sm / (float)k;
    }
    bool checkIfSlotIsFull(Dictionary<string, int> inv, int slot)
    {
        return slot < inv.Count;
    }

    public void turnBlue()
    {
        requireL.GetComponent<Image>().color = Color.blue;
    }

    public float f(float x)
    {
        return (x * x * rndA + x * rndB + rndC);
    }
    public float fUSER(float x)
    {
        return (x * x * sliderA.value + x * sliderB.value + sliderC.value);
    }
    public void GetStarted()
    {
        CookScript cook = collision.gameObject.GetComponent<CookScript>();
        if (blueNum > 0 && redNum > 0 && !GameGoing)
        {
            GameGoing = true;
            rndA = UnityEngine.Random.Range(10, 100) / 100.0f;
            rndB = UnityEngine.Random.Range(-6, 6);
            rndC = UnityEngine.Random.Range(-100, -10);
            collision.gameObject.GetComponent<CookScript>().Freeze();
            check = true;
            lineRenderer.positionCount = resolution + 1; // +1 ��� ���������� �����

            for (int i = 0; i <= resolution; i++)
            {
                float x = Mathf.Lerp(xMin, xMax, i / (float)resolution);
                float y = f(x); // ���� �������
                                // y = Mathf.Clamp(y, yMin, yMax);
                lineRenderer.SetPosition(i, new Vector3(x / 100.0f, y / 100.0f, 0));
            }
            shrederUI.SetActive(true);
            Camera.main.GetComponent<playerFollow>().enabled = false;
            Camera.main.transform.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.3f);
            Camera.main.DOOrthoSize(0.5f, 0.3f);
        }
    }
}