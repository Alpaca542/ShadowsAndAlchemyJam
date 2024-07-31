using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hint : MonoBehaviour
{
    PlayerScript cook;
    // Start is called before the first frame update
    void Start()
    {
        cook = GameObject.FindWithTag("Cook").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cook.selected)
        {
            if (Input.GetKey(KeyCode.H))
            {
                gameObject.GetComponent<Image>().color = new Color(0.8679245f, 0.8679245f, 0.8679245f, 1f);
            }
            else
            {


                gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }
}
