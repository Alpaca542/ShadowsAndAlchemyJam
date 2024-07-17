using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    public bool ForCook = true;
    Collider2D touchedPlayer;
    public GameObject SitUI;
    public GameObject SitBTN;
    public GameObject StopSitBTN;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ForCook)
        {


            if (collision.gameObject.CompareTag("Cook"))
            {
                SitUI.SetActive(true);
                SitBTN.SetActive(true);
                StopSitBTN.SetActive(false);
                touchedPlayer = collision;
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Defender"))
            {
                //Camera.main.gameObject.GetComponent<playerFollow>().player.gameObject.GetComponent<PlayerScript>().Sit(transform);
                SitUI.SetActive(true);
                SitBTN.SetActive(true);
                StopSitBTN.SetActive(false);
                touchedPlayer = collision;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        SitUI.SetActive(false);
        touchedPlayer = null;
    }
    public void MakeMeSit()
    {
        touchedPlayer.gameObject.GetComponent<PlayerScript>().Sit(transform);
        SitBTN.SetActive(false);
        StopSitBTN.SetActive(true);

    }
    public void MakeMeStopSit()
    {
        touchedPlayer.gameObject.GetComponent<PlayerScript>().StopSitting();
        SitBTN.SetActive(true);
        StopSitBTN.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
