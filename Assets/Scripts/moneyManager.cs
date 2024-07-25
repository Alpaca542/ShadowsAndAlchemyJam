using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneyManager : MonoBehaviour
{
    public int MyMoney;

    public void GetMoney(int howMuch)
    {
        MyMoney += howMuch;
    }

    public void LoseMoney(int howMuch)
    {
        MyMoney -= howMuch;
    }
}
