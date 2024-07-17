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
            myText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cook")
        {
            myText.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (myType == 0)
            {
                //use for 0, etc
            }
        }
    }
}
