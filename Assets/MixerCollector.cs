using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixerCollector : MonoBehaviour
{
    public MixerScript mixer;
    bool redANDblue = false;
    bool pureRed = false;
    bool greenANDblue = false;
    bool Analyzed = false;
    bool Graphed = false;

    bool canbefinished = false;

    public Image I;
    public Image II;
    public Image III;
    public Image IV;
    public Image V;
    private void Update()
    {
        if(redANDblue&&
        pureRed&&
        greenANDblue &&
        Analyzed &&
        Graphed)
        {
            canbefinished = true;
        }
    }

    public void BtnScript()
    {
        if (canbefinished)
        {
            mixer.FinishGame();
            redANDblue = false;
            pureRed = false;
            greenANDblue = false;
            Analyzed = false;
            Graphed = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("redANDblue"))
        {
            redANDblue = true;
            collision.GetComponent<Drager>().enabled = false;
            Destroy(collision.gameObject);
            V.color = Color.green;
        }
        if (collision.CompareTag("pureRed"))
        {
            pureRed = true;
            collision.GetComponent<Drager>().enabled = false;
            Destroy(collision.gameObject);
            I.color = Color.green;
        }
        if (collision.CompareTag("greenANDblue"))
        {
            greenANDblue = true;
            collision.GetComponent<Drager>().enabled = false;
            Destroy(collision.gameObject);
            IV.color = Color.green;
        }
        if (collision.CompareTag("Analyzed"))
        {
            Analyzed = true;
            collision.GetComponent<Drager>().enabled = false;
            Destroy(collision.gameObject);
            II.color = Color.green;
        }
        if (collision.CompareTag("Graphed"))
        {
            Graphed = true;
            collision.GetComponent<Drager>().enabled = false;
            Destroy(collision.gameObject);
            III.color = Color.green;
        }
    }
}
