using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrederSctipt : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shrederUI;
    public GameObject[] spawnPoints;
    bool interact = false;
    public GameObject tube;
    Collision2D collision;
    public BoilerScript boiler;
    public GameObject redMatter;
    public GameObject Impure;
    bool check = false;
    void Start()
    {
        
    }
    void Check()
    {
        int k = 0;
        foreach (Transform lol in gameObject.GetComponentsInChildren<Transform>())
        {
            if (lol.gameObject.CompareTag("RedBTN"))
            {
                k++;
            }
        }

        if ((k == 0))
        {

            shrederUI.SetActive(false);
            tube.GetComponent<SpriteRenderer>().color = Color.red;
            Invoke(nameof(turnTube),1f);
            collision.gameObject.GetComponent<CookScript>().UnFreeze();
            check = false;
        }
    }

    void turnTube()
    {
        tube.GetComponent<SpriteRenderer>().color = Color.white;
        boiler.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (interact)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                shrederUI.SetActive(true);
                collision.gameObject.GetComponent<CookScript>().Freeze();
                ClearMePlease();
                collision.gameObject.GetComponent<CookScript>().WhatIHave = "0";
                interact = false;
                check = true;
            }
            
        }
        if(check) { Check(); }
        
    }
    public void ClearMePlease()
    {
        foreach (GameObject go in spawnPoints)
        {
            if ((Random.Range(0, 3) == 0)|| (Random.Range(0, 3) == 1))
            {
                if ((Random.Range(0, 2) == 0))
                {
                    Instantiate(Impure, go.transform.position,Quaternion.identity, shrederUI.transform);
                }  
                else
                {
                    Instantiate(redMatter, go.transform.position, Quaternion.identity, shrederUI.transform);
                }
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cook"))
        {
            
            if ((collision.gameObject.GetComponent<CookScript>().WhatIHave == "red"))
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

            
                collision.gameObject.GetComponent<CookScript>().UnFreeze();
            
        }
    }
}
