using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class moneyManager : MonoBehaviour
{
    public int MyMoney;
    public TMP_Text txt1;
    private float totalDebt;
    private bool counting;

    public void GetMoney(int howMuch)
    {
        MyMoney += howMuch;
        totalDebt -= howMuch;
        if (!counting)
        {
            StartCoroutine(moneyCounter());
        }
    }
    private void Start()
    {
        totalDebt = MyMoney;
    }
    public void LoseMoney(int howMuch)
    {
        MyMoney -= howMuch;
        totalDebt += howMuch;
        if (!counting)
        {
            StartCoroutine(moneyCounter());
        }
    }

    IEnumerator moneyCounter()
    {
        counting = true;
        int MyMoney1 = Convert.ToInt32(txt1.text);
        while (totalDebt != 0 && MyMoney1 >= 0 && MyMoney1 < 1000000)
        {
            if (totalDebt > 0)
            {
                txt1.text = (Convert.ToInt32(txt1.text) - 1).ToString();
                totalDebt--;
                yield return new WaitForSeconds(0.05f);
            }
            else if (totalDebt < 0)
            {
                txt1.text = (Convert.ToInt32(txt1.text) + 1).ToString();
                totalDebt++;
                yield return new WaitForSeconds(0.05f);
            }
        }
        counting = false;
    }

    // private void Update()
    // {
    //     txt1.text = MyMoney.ToString();
    // }
}
