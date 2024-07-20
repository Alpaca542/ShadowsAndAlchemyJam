using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public string whetAmI;
    public Sprite myTexture;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = myTexture;
    }
}
