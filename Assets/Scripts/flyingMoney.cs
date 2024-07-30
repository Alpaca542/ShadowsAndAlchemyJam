using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class flyingMoney : MonoBehaviour
{
    public int myValue;

    private void Update()
    {
        var step = 5 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Defender").transform.position, step);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Defender")
        {
            GameObject.FindGameObjectWithTag("MoneyManager").GetComponent<moneyManager>().GetMoney(myValue);
            Destroy(gameObject);
            gameObject.GetComponent<soundManager>().PlaySound(0, 1, 1f);
        }
    }
}