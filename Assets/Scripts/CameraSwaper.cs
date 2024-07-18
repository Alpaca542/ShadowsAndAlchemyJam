using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwaper : MonoBehaviour
{
    public GameObject inactiveCamera;
    public Animation canvasAnimation;
    public void Swap()
    {
        canvasAnimation.Play();
        GameObject player1 = Camera.main.GetComponent<playerFollow>().player.gameObject;
        GameObject player2 = inactiveCamera.GetComponent<playerFollow>().player.gameObject;

        if (player1.tag == "Car")
        {
            player1 = GameObject.FindGameObjectWithTag("Cook");
        }

        if (player2.tag == "Car")
        {
            player2 = GameObject.FindGameObjectWithTag("Cook");
        }

        SwapSizes();
        SwapPlayers(player1, player2);
        SwapSelections(player1, player2);

        inactiveCamera.transform.position = player1.transform.position;

        if (player2.GetComponent<PlayerScript>().sit && player2.GetComponent<PlayerScript>().mySeat.GetComponent<Brewer>().myType == 1)
        {
            GameObject.FindGameObjectWithTag("Car").GetComponent<carScript>().Moveable = true;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Car").GetComponent<carScript>().Moveable = false;
        }
    }

    void SwapSizes()
    {
        float tempStor = Camera.main.GetComponent<Camera>().orthographicSize;
        Camera.main.GetComponent<Camera>().orthographicSize = inactiveCamera.GetComponent<Camera>().orthographicSize;
        inactiveCamera.GetComponent<Camera>().orthographicSize = tempStor;
    }
    void SwapPlayers(GameObject player1, GameObject player2)
    {
        Camera.main.GetComponent<playerFollow>().player = player2.transform;
        inactiveCamera.GetComponent<playerFollow>().player = player1.transform;
    }

    void SwapSelections(GameObject player1, GameObject player2)
    {
        player1.GetComponent<PlayerScript>().selected = false;
        player2.GetComponent<PlayerScript>().selected = true;
    }
}
