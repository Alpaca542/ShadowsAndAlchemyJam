using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class redMatterStorage : MonoBehaviour
{
    public string whichMatter;//red;green;blue
    public float matter;
    public Text MatterText;
    bool interact = false;
    Collision2D collision;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cook"))
        {
            interact = true;
            this.collision = collision;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cook"))
        {
            interact = false;
        }
    }
    void Update()
    {
        MatterText.text = whichMatter + ": " + Convert.ToString(matter);
        if (collision != null && collision.gameObject.tag == "Cook")
        {
            if (interact)
            {
                if (Input.GetKeyDown(KeyCode.E) && matter > 0)
                {
                    GetComponent<soundManager>().PlaySound(0, 0.9f, 1.1f);
                    matter -= 1;
                    collision.gameObject.GetComponent<CookScript>().GetItem(whichMatter);
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (collision.gameObject.GetComponent<CookScript>().inventory.ContainsKey(whichMatter) && collision.gameObject.GetComponent<CookScript>().inventory[whichMatter] > 0)
                    {
                        matter += 1;
                        collision.gameObject.GetComponent<CookScript>().RemoveItem(whichMatter);
                    }
                }
            }
        }
    }
}
