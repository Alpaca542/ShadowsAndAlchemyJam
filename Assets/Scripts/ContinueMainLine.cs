using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueMainLine : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(123333);
        GameObject.FindGameObjectWithTag("DialogueMng").GetComponent<DialogueScript>().StartMainLine(Camera.main.orthographicSize);
    }
}
