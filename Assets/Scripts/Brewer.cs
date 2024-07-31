using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

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

    public Sprite HelperFace;
    public string HelperText;


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
                if (myType == 8)
                {
                    if (other.gameObject.tag == "Defender")
                    {
                        other.GetComponent<DefenderScript1>().inAShop = false;
                    }
                    Camera.main.transform.parent.GetComponent<playerFollow>().enabled = true;
                    Camera.main.DOOrthoSize(5f, 0.3f);
                    GetComponent<MainShop>().Close();
                }
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
                    if (myType == 0)
                    {
                        if (!PlayerPrefs.HasKey("0"))
                        {
                            PlayerPrefs.SetInt("0", 1);
                            Camera.main.transform.parent.GetComponent<playerFollow>().enabled = false;
                            GameObject.FindGameObjectWithTag("DialogueMng").GetComponent<DialogueScript>().StartCrtnRemotely(HelperText, HelperFace, true, Camera.main.orthographicSize);
                            Camera.main.DOOrthoSize(0.5f, 0.5f).SetUpdate(true);
                            Camera.main.transform.parent.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.5f).SetUpdate(true);
                        }
                        else
                        {

                            GetComponent<BoilerScript>().collision = connectedPlayer;
                            GetComponent<BoilerScript>().GetLoot();
                        }
                    }
                    else if (myType == 1)
                    {
                        if (!PlayerPrefs.HasKey("1"))
                        {
                            PlayerPrefs.SetInt("1", 1);
                            Camera.main.transform.parent.GetComponent<playerFollow>().enabled = false;
                            GameObject.FindGameObjectWithTag("DialogueMng").GetComponent<DialogueScript>().StartCrtnRemotely(HelperText, HelperFace, true, Camera.main.orthographicSize);
                            Camera.main.DOOrthoSize(0.5f, 0.5f).SetUpdate(true);
                            Camera.main.transform.parent.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.5f).SetUpdate(true);
                        }
                        else
                        {
                            active = true;
                            myCar.Sit(true);
                            connectedPlayerScript.GetComponent<PlayerScript>().Sit(transform);
                            myText.GetComponent<TMP_Text>().text = "<i><b>shift</b> to stand up</i>";
                        }
                    }
                    else if (myType == 2)
                    {
                        if (!PlayerPrefs.HasKey("1"))
                        {
                            PlayerPrefs.SetInt("1", 1);
                            Camera.main.transform.parent.GetComponent<playerFollow>().enabled = false;
                            GameObject.FindGameObjectWithTag("DialogueMng").GetComponent<DialogueScript>().StartCrtnRemotely(HelperText, HelperFace, true, Camera.main.orthographicSize);
                            Camera.main.DOOrthoSize(0.5f, 0.5f).SetUpdate(true);
                            Camera.main.transform.parent.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.5f).SetUpdate(true);
                        }
                        else
                        {
                            active = true;
                            myCar.Stand(false);
                            connectedPlayerScript.GetComponent<PlayerScript>().Sit(transform);
                            myText.GetComponent<TMP_Text>().text = "<i><b>shift</b> to stand up</i>";
                        }
                    }
                    else if (myType == 3)
                    {
                        if (!PlayerPrefs.HasKey("2"))
                        {
                            PlayerPrefs.SetInt("2", 1);
                            Camera.main.transform.parent.GetComponent<playerFollow>().enabled = false;
                            GameObject.FindGameObjectWithTag("DialogueMng").GetComponent<DialogueScript>().StartCrtnRemotely(HelperText, HelperFace, true, Camera.main.orthographicSize);
                            Camera.main.DOOrthoSize(0.5f, 0.5f).SetUpdate(true);
                            Camera.main.transform.parent.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.5f).SetUpdate(true);
                        }
                        else
                        {
                            GetComponent<grapherScript>().collision = connectedPlayer;
                            GetComponent<grapherScript>().GetLoot();
                        }
                    }
                    else if (myType == 4)
                    {
                        if (!PlayerPrefs.HasKey("3"))
                        {
                            PlayerPrefs.SetInt("3", 1);
                            Camera.main.transform.parent.GetComponent<playerFollow>().enabled = false;
                            GameObject.FindGameObjectWithTag("DialogueMng").GetComponent<DialogueScript>().StartCrtnRemotely(HelperText, HelperFace, true, Camera.main.orthographicSize);
                            Camera.main.DOOrthoSize(0.5f, 0.5f).SetUpdate(true);
                            Camera.main.transform.parent.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.5f).SetUpdate(true);
                        }
                        else
                        {
                            GetComponent<MerryGoRound>().collision = connectedPlayer;
                            GetComponent<MerryGoRound>().GetLoot();
                        }
                    }
                    else if (myType == 5)
                    {
                        if (!PlayerPrefs.HasKey("4"))
                        {
                            PlayerPrefs.SetInt("4", 1);
                            Camera.main.transform.parent.GetComponent<playerFollow>().enabled = false;
                            GameObject.FindGameObjectWithTag("DialogueMng").GetComponent<DialogueScript>().StartCrtnRemotely(HelperText, HelperFace, true, Camera.main.orthographicSize);
                            Camera.main.DOOrthoSize(0.5f, 0.5f).SetUpdate(true);
                            Camera.main.transform.parent.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.5f).SetUpdate(true);
                        }
                        else
                        {
                            GetComponent<analyzercript>().collision = connectedPlayer;
                            GetComponent<analyzercript>().GetLoot();
                        }
                    }
                    else if (myType == 6)
                    {
                        if (!PlayerPrefs.HasKey("5"))
                        {
                            PlayerPrefs.SetInt("5", 1);
                            Camera.main.transform.parent.GetComponent<playerFollow>().enabled = false;
                            GameObject.FindGameObjectWithTag("DialogueMng").GetComponent<DialogueScript>().StartCrtnRemotely(HelperText, HelperFace, true, Camera.main.orthographicSize);
                            Camera.main.DOOrthoSize(0.5f, 0.5f).SetUpdate(true);
                            Camera.main.transform.parent.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.5f).SetUpdate(true);
                        }
                        else
                        {
                            GetComponent<shrederSctipt>().collision = connectedPlayer;
                            GetComponent<shrederSctipt>().GetLoot();
                        }
                    }
                    else if (myType == 7)
                    {
                        if (!PlayerPrefs.HasKey("6"))
                        {
                            PlayerPrefs.SetInt("6", 1);
                            Camera.main.transform.parent.GetComponent<playerFollow>().enabled = false;
                            GameObject.FindGameObjectWithTag("DialogueMng").GetComponent<DialogueScript>().StartCrtnRemotely(HelperText, HelperFace, true, Camera.main.orthographicSize);
                            Camera.main.DOOrthoSize(0.5f, 0.5f).SetUpdate(true);
                            Camera.main.transform.parent.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.5f).SetUpdate(true);
                        }
                        else
                        {
                            GetComponent<MixerScript>().collision = connectedPlayer;
                            GetComponent<MixerScript>().GetStarted();
                        }
                    }
                    else if (myType == 8)
                    {
                        if (!PlayerPrefs.HasKey("7"))
                        {
                            PlayerPrefs.SetInt("7", 1);
                            Camera.main.transform.parent.GetComponent<playerFollow>().enabled = false;
                            GameObject.FindGameObjectWithTag("DialogueMng").GetComponent<DialogueScript>().StartCrtnRemotely(HelperText, HelperFace, true, Camera.main.orthographicSize);
                            Camera.main.DOOrthoSize(0.5f, 0.5f).SetUpdate(true);
                            Camera.main.transform.parent.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.5f).SetUpdate(true);
                        }
                        else
                        {
                            if (connectedPlayer.gameObject.tag == "Defender")
                            {
                                connectedPlayer.GetComponent<DefenderScript1>().inAShop = true;
                            }
                            GetComponent<MainShop>().isCookHere = connectedPlayer.GetComponent<CookScript>();
                            GetComponent<MainShop>().collision = connectedPlayer.gameObject;
                            GetComponent<MainShop>().Open();
                            Camera.main.transform.parent.GetComponent<playerFollow>().enabled = false;
                            Camera.main.transform.parent.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.3f);
                            Camera.main.DOOrthoSize(0.7f, 0.3f);
                        }
                    }
                    else if (myType == 9)
                    {
                        if (!PlayerPrefs.HasKey("8"))
                        {
                            PlayerPrefs.SetInt("8", 1);
                            Camera.main.transform.parent.GetComponent<playerFollow>().enabled = false;
                            GameObject.FindGameObjectWithTag("DialogueMng").GetComponent<DialogueScript>().StartCrtnRemotely(HelperText, HelperFace, true, Camera.main.orthographicSize);
                            Camera.main.DOOrthoSize(0.5f, 0.5f).SetUpdate(true);
                            Camera.main.transform.parent.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.5f).SetUpdate(true);
                        }
                        else if (connectedPlayer.GetComponent<DefenderScript1>().bullets[8] > 0)
                        {
                            // GetComponent<PedestalScript>().SetBomb();
                        }
                    }
                    else if (myType == 10)
                    {
                        if (!PlayerPrefs.HasKey("9"))
                        {
                            PlayerPrefs.SetInt("9", 1);
                            Camera.main.transform.parent.GetComponent<playerFollow>().enabled = false;
                            GameObject.FindGameObjectWithTag("DialogueMng").GetComponent<DialogueScript>().StartCrtnRemotely(HelperText, HelperFace, true, Camera.main.orthographicSize);
                            Camera.main.DOOrthoSize(0.5f, 0.5f).SetUpdate(true);
                            Camera.main.transform.parent.DOMove(new Vector3(transform.position.x, transform.position.y, -10), 0.5f).SetUpdate(true);
                        }
                        else
                        {
                            GetComponent<giverScript>().collision = connectedPlayer;
                            if (connectedPlayer.GetComponent<CookScript>() != null)
                            {
                                GetComponent<giverScript>().GetLoot();
                            }
                            else
                            {
                                GetComponent<giverScript>().GiveLoot();
                            }
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
