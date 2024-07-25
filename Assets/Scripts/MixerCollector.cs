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
    public Image self;
    bool canbefinished = false;
    GameObject[] imgs;
    public Image I;
    public Image II;
    public Image III;
    public Image IV;
    public Image V;
    private void Start()
    {
        self.color = Color.white;
    }
    private void Update()
    {
        if (redANDblue &&
        pureRed &&
        greenANDblue &&
        Analyzed &&
        Graphed)
        {
            canbefinished = true;
        }
    }
    private void OnEnable()
    {
        self.color = Color.white;
        I.color = Color.white;
        II.color = Color.white;
        III.color = Color.white;
        IV.color = Color.white;
        V.color = Color.white;
        foreach (var img in imgs)
        {
            img.transform.position = img.GetComponent<Drager>().initialPos;
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
            self.color = Color.red;


        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("redANDblue"))
        {
            redANDblue = true;
            collision.GetComponent<Drager>().enabled = false;
            collision.gameObject.SetActive(false);
            V.color = Color.green;
        }
        if (collision.CompareTag("pureRed"))
        {
            pureRed = true;
            collision.GetComponent<Drager>().enabled = false;
            collision.gameObject.SetActive(false);
            I.color = Color.green;
        }
        if (collision.CompareTag("greenANDblue"))
        {
            greenANDblue = true;
            collision.GetComponent<Drager>().enabled = false;
            collision.gameObject.SetActive(false);
            IV.color = Color.green;
        }
        if (collision.CompareTag("Analyzed"))
        {
            Analyzed = true;
            collision.GetComponent<Drager>().enabled = false;
            collision.gameObject.SetActive(false);
            II.color = Color.green;
        }
        if (collision.CompareTag("Graphed"))
        {
            Graphed = true;
            collision.GetComponent<Drager>().enabled = false;
            collision.gameObject.SetActive(false);
            III.color = Color.green;
        }
    }
}
