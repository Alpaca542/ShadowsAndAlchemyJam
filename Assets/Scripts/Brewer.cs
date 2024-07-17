using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brewer : MonoBehaviour
{
    public GameObject myText;
    public bool NearCook;
    public int myType;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cook")
        {
            NearCook = true;
            myText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cook")
        {
            NearCook = false;
            myText.SetActive(false);
        }
    }
    private void Update()
    {
        if (NearCook)
        {
            PlayerScript cook = GameObject.FindGameObjectWithTag("Cook").GetComponent<PlayerScript>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (myType == 0)
                {
                    cook.GetBottle();
                }
            }
        }
    }
}
