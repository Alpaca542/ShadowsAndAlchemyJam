using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UI;

public class Brewer : MonoBehaviour
{
    public GameObject myText;
    private GameObject connectedPlayer;
    public bool forCookOnly;
    public int myType;
    public carScript myCar;
    public bool StickToTheParent;
    public bool activatable;
    public bool startable;
    private bool active;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (connectedPlayer == null)
        {
            if (forCookOnly)
            {
                if (other.gameObject.tag == "Cook")
                {
                    connectedPlayer = other.gameObject;
                    myText.SetActive(true);
                }
            }
            else if (other.gameObject.tag == "Cook" || other.gameObject.tag == "Defender")
            {
                connectedPlayer = other.gameObject;
                myText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (connectedPlayer != null)
        {
            if (forCookOnly)
            {
                if (other.gameObject.tag == "Cook")
                {
                    connectedPlayer = null;
                    myText.SetActive(false);
                }
            }
            else if (other.gameObject.tag == "Cook" || other.gameObject.tag == "Defender")
            {
                connectedPlayer = null;
                myText.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (connectedPlayer != null)
        {
            PlayerScript connectedPlayerScript = connectedPlayer.GetComponent<PlayerScript>();

            if (connectedPlayerScript.selected)
            {
                if (Input.GetKeyDown(KeyCode.E) && (!active || !activatable)) // activate
                {
                    active = true;

                    if (myType == 0)
                    {
                        GetComponent<BoilerScript>().collision = connectedPlayer;
                        GetComponent<BoilerScript>().GetLoot();
                    }
                    else if (myType == 1)
                    {
                        myCar.Sit(true);
                        connectedPlayerScript.GetComponent<PlayerScript>().Sit(transform);
                        myText.GetComponent<TMP_Text>().text = "<i><b>shift</b> to stand up</i>";
                    }
                    else if (myType == 2)
                    {
                        myCar.Stand(false);
                        connectedPlayerScript.GetComponent<PlayerScript>().Sit(transform);
                        myText.GetComponent<TMP_Text>().text = "<i><b>shift</b> to stand up</i>";
                    }
                    else if (myType == 3)
                    {
                        GetComponent<grapherScript>().collision = connectedPlayer;
                        GetComponent<grapherScript>().GetLoot();
                    }
                    else if (myType == 4)
                    {
                        GetComponent<MerryGoRound>().collision = connectedPlayer;
                        GetComponent<MerryGoRound>().GetLoot();
                    }
                    else if (myType == 5)
                    {
                        GetComponent<analyzercript>().collision = connectedPlayer;
                        GetComponent<analyzercript>().GetLoot();
                    }
                    else if (myType == 6)
                    {
                        GetComponent<shrederSctipt>().collision = connectedPlayer;
                        GetComponent<shrederSctipt>().GetLoot();
                    }
                    else if (myType == 7)
                    {
                        GetComponent<MixerScript>().collision = connectedPlayer;
                        GetComponent<MixerScript>().GetStarted();
                    }
                    else if (myType == 8)
                    {
                        GetComponent<MainShop>().isCookHere = connectedPlayer.GetComponent<CookScript>();
                        GetComponent<MainShop>().collision = connectedPlayer.gameObject;
                        GetComponent<MainShop>().Open();
                    }
                    else if (myType == 9)
                    {
                        GetComponent<PedestalScript>().SetBomb();
                    }
                    else if (myType == 10)
                    {
                        GetComponent<giverScript>().collision = connectedPlayer;
                        if(connectedPlayer.GetComponent<CookScript>()!=null)
                        {
                            GetComponent<giverScript>().GetLoot();
                        }
                        else
                        {
                            GetComponent<giverScript>().GiveLoot();
                        }
                    }
                }
                else if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) && active) && activatable)// deactivate
                {
                    active = false;

                    if (myType == 1)
                    {
                        myCar.Stand(true);
                        connectedPlayerScript.GetComponent<PlayerScript>().StopSitting();
                        myText.GetComponent<TMP_Text>().text = "<i><b>e</b> to sit</i>";
                    }
                    else if (myType == 2)
                    {
                        myCar.Sit(false);
                        connectedPlayerScript.GetComponent<PlayerScript>().StopSitting();
                        myText.GetComponent<TMP_Text>().text = "<i><b>e</b> to sit</i>";
                    }
                }
                if (Input.GetKeyDown(KeyCode.Space) && (!active || !activatable))
                {
                    if (myType == 0)
                    {
                        GetComponent<BoilerScript>().collision = connectedPlayer;
                        GetComponent<BoilerScript>().GetStarted();
                    }
                    if (myType == 3)
                    {
                        GetComponent<grapherScript>().collision = connectedPlayer;
                        GetComponent<grapherScript>().GetStarted();
                    }
                    if (myType == 4)
                    {
                        GetComponent<MerryGoRound>().collision = connectedPlayer;
                        GetComponent<MerryGoRound>().GetStarted();
                    }
                    if (myType == 5)
                    {
                        GetComponent<analyzercript>().collision = connectedPlayer;
                        GetComponent<analyzercript>().GetStarted();
                    }
                    else if (myType == 6)
                    {
                        GetComponent<shrederSctipt>().collision = connectedPlayer;
                        GetComponent<shrederSctipt>().GetStarted();
                    }
                }
            }
        }
        if (StickToTheParent)
        {
            GetComponent<Rigidbody2D>().velocity = transform.parent.GetComponent<Rigidbody2D>().velocity;
        }
    }
}
