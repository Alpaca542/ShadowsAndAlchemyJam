using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public Text Display;
    public Image Display2;
    public string[] sentences;
    public bool ShouldIStopAfterpb;
    public Sprite[] faces;
    public int[] stopindexes = { 7 };
    public int IndexInMain;
    public string Stringpb;
    public GameObject btnContinue;
    public GameObject cnv;
    public GameObject cnvInGame;
    public GameObject cnvInGame2;
    public GameObject btnContinueFake;
    public float typingspeed = 0.02f;
    IEnumerator coroutine;
    public bool startImmediately;
    private void Start()
    {
        Time.timeScale = 1f;
        if (startImmediately && !PlayerPrefs.HasKey("Storyline"))
        {
            PlayerPrefs.SetInt("Storyline", 1);
            coroutine = Type(sentences[IndexInMain], faces[IndexInMain], false);
            StartCoroutine(coroutine);
        }
        else
        {
            cnvInGame.SetActive(true);
            cnvInGame2.SetActive(true);
            btnContinue.SetActive(false);
            cnv.SetActive(false);
            IndexInMain = stopindexes[0];
        }
    }
    public void StartCrtnRemotely(string WhatToType, Sprite WhatToShow, bool ShouldIStopAfter)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = Type(WhatToType, WhatToShow, ShouldIStopAfter);
        StartCoroutine(coroutine);
    }
    public IEnumerator Type(string WhatToType, Sprite WhatToShow, bool ShouldIStopAfter)
    {
        //PLAYSOUND
        ShouldIStopAfterpb = ShouldIStopAfter;
        Stringpb = WhatToType;

        if (Camera.main.GetComponent<playerFollow>().player.tag == "Player" && !startImmediately)
        {
            Camera.main.GetComponent<playerFollow>().player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        cnv.SetActive(true);
        cnvInGame.SetActive(false);
        cnvInGame2.SetActive(false);
        btnContinue.SetActive(false);
        btnContinueFake.SetActive(false);
        Display.text = "";
        if (WhatToShow.name == "player")
        {
            Display2.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(420, 420);
        }
        else
        {
            Display2.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
        }
        Display2.sprite = WhatToShow;
        foreach (char letter1 in WhatToType.ToCharArray())
        {
            Display.text += letter1;
            if (letter1 == ".".ToCharArray()[0] || letter1 == "!".ToCharArray()[0] || letter1 == "?".ToCharArray()[0])
            {
                yield return new WaitForSeconds(0.1f / (1 / Time.timeScale));
            }
            else if (letter1 == " ".ToCharArray()[0])
            {
                yield return new WaitForSeconds(0.05f / (1 / Time.timeScale));
            }
            else
            {
                yield return new WaitForSeconds(typingspeed / (1 / Time.timeScale));
            }
        }
        //gameObject.GetComponent<soundManager>().sound.loop = false;
        if (ShouldIStopAfter)
        {
            btnContinueFake.SetActive(true);
        }
        else
        {
            btnContinue.SetActive(true);
        }
        //GetComponent<AudioSource>().Stop();
    }
    public void StartMainLine()
    {
        coroutine = Type(sentences[IndexInMain], faces[IndexInMain], false);
        StartCoroutine(coroutine);
    }
    public void ContinueTyping()
    {
        if (cnv.activeSelf)
        {
            IndexInMain++;
            if (Array.IndexOf(stopindexes, IndexInMain) == -1)
            {
                coroutine = Type(sentences[IndexInMain], faces[IndexInMain], false);
                StartCoroutine(coroutine);
            }
            else
            {
                Time.timeScale = 1f;
                cnvInGame.SetActive(true);
                cnvInGame2.SetActive(true);
                btnContinue.SetActive(false);
                cnv.SetActive(false);
                if (IndexInMain == stopindexes[0])
                {
                    //
                }
            }
        }
    }

    public void StopTyping()
    {
        Time.timeScale = 1f;
        cnvInGame.SetActive(true);
        cnvInGame2.SetActive(true);
        btnContinue.SetActive(false);
        cnv.SetActive(false);
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && cnv.activeSelf)
        {
            //gameObject.GetComponent<soundManager>().sound.loop = false;
            //GetComponent<AudioSource>().Stop();
            StopCoroutine(coroutine);
            if (Display.text == Stringpb)
            {
                if (ShouldIStopAfterpb)
                {
                    StopTyping();
                }
                else
                {
                    ContinueTyping();
                }
            }
            else
            {
                if (ShouldIStopAfterpb)
                {
                    Display.text = Stringpb;
                    btnContinueFake.SetActive(true);
                }
                else
                {
                    Display.text = sentences[IndexInMain];
                    btnContinue.SetActive(true);
                }
            }
        }
    }
}