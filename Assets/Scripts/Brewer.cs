using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UI;

public class Brewer : MonoBehaviour
{
    public GameObject myText;
    public bool NearCook;
    public int myType;
    public carScript myCar;

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
        if (myType == 1)
        {
            GetComponent<Rigidbody2D>().velocity = transform.parent.GetComponent<Rigidbody2D>().velocity;
        }
        if (NearCook)
        {
            PlayerScript cook = GameObject.FindGameObjectWithTag("Cook").GetComponent<PlayerScript>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (myType == 0)
                {
                    cook.GetBottle();
                }
                else if (myType == 1)
                {
                    myCar.Sit();
                    cook.GetComponent<PlayerScript>().Sit(transform);
                    myText.GetComponent<TMP_Text>().text = "<i><b>shift</b> to stand up</i>";
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                {
                    if (myType == 1)
                    {
                        myCar.Stand();
                        cook.GetComponent<PlayerScript>().StopSitting();
                        myText.GetComponent<TMP_Text>().text = "<i><b>e</b> to sit</i>";
                    }
                }
            }
        }
    }
}
