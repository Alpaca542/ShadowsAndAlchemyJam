using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class shrederSctipt : MonoBehaviour
{
    public Image AmIFULLIMAGE;
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

            AmIFilled = true;
            shrederUI.SetActive(false);
            // tube.GetComponent<SpriteRenderer>().color = Color.red;
            collision.gameObject.GetComponent<CookScript>().UnFreeze();
            active = false;
            AmIFULLIMAGE.color = new Color32(255, 0, 0, 255);
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
                cook.GetItem("pureRed");
                AmIFilled = false;
                AmIFULLIMAGE.color = new Color32(255, 255, 255, 150);
            }
            if (check) { Check(); }
        }
    }

    public void GetStarted()
    {
        CookScript cook = collision.gameObject.GetComponent<CookScript>();
        if (cook.ActiveSlot < cook.inventory.Count && cook.inventory.ElementAt(cook.ActiveSlot).Key == "red"&&!active)
        {
            if (!AmIFilled)
            {
                shrederUI.SetActive(true);
                cook.Freeze();
                ClearMePlease();
                cook.RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                check = true;
                active = true;
                
            }
        }
    }
    void ClearAllPoints()
    {
        foreach (var tr in shrederUI.transform.GetComponentsInChildren<Transform>())
        {
            if(tr!= shrederUI.transform.GetComponentsInChildren<Transform>()[0])
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
