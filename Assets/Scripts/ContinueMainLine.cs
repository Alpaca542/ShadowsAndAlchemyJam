using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueMainLine : MonoBehaviour
{
    private void Start()
    {
        GameObject.FindGameObjectWithTag("DialogueMng").GetComponent<DialogueScript>().StartMainLine(Camera.main.orthographicSize);
    }
}
