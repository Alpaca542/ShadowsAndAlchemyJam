using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class redMatterStorage : MonoBehaviour
{
    public string whichMatter;//red;green;blue
    public float matter;
    public Text MatterText;
    bool interact = false;
    Collision2D collision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Cook"))
        {
            if (((collision.gameObject.GetComponent<CookScript>().WhatIHave == "0") || (collision.gameObject.GetComponent<CookScript>().WhatIHave == whichMatter)))
            {
                interact = true;
                this.collision = collision;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cook"))
        {
            
                interact = false;
                
            
        }
    }
    void Update()
    {
        MatterText.text = whichMatter + ": "+Convert.ToString(matter);

        if(interact)
        {

            if (((collision.gameObject.GetComponent<CookScript>().WhatIHave == "0") || (collision.gameObject.GetComponent<CookScript>().WhatIHave == whichMatter)))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Spaceee");
                    if (collision.gameObject.GetComponent<CookScript>().WhatIHave == "0")
                    {
                        if (matter > 0)
                        {
                            matter -= 1;
                            collision.gameObject.GetComponent<CookScript>().WhatIHave = whichMatter;


                        }
                    }
                    else
                    {
                        matter += 1;
                        collision.gameObject.GetComponent<CookScript>().WhatIHave = "0";
                    }

                }
            }

        }
    }
}
