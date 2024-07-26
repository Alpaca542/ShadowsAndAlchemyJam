using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class flyingMoney : MonoBehaviour
{
    public int myValue;

    private void Start()
    {
        transform.DOMove(GameObject.FindGameObjectWithTag("Defender").transform.position, 0.3f);
        Invoke(nameof(Die), 0.35f);
    }

    private void Die()
    {
        GameObject.FindGameObjectWithTag("MoneyManager").GetComponent<moneyManager>().GetMoney(myValue);
        Destroy(gameObject);
    }
}
