using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class moneyManager : MonoBehaviour
{
    public int MyMoney;
    public TMP_Text txt1;
    public TMP_Text txt2;

    public void GetMoney(int howMuch)
    {
        MyMoney += howMuch;
    }

    public void LoseMoney(int howMuch)
    {
        MyMoney -= howMuch;
    }

    private void Update()
    {
        txt1.text = MyMoney.ToString();
        txt2.text = MyMoney.ToString();
    }
}
