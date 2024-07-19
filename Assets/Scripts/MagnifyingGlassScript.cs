using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnifyingGlassScript : MonoBehaviour
{
    public analyzercript myAnalyzer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "invis")
        {
            other.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            // myAnalyzer.
        }
    }
}
