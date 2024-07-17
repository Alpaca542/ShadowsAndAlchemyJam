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

        //Camera.main.transform.position = player2.transform.position;
        Camera.main.GetComponent<playerFollow>().player = player2.transform;

        inactiveCamera.transform.position = player1.transform.position;
        inactiveCamera.GetComponent<playerFollow>().player = player1.transform;

        player1.GetComponent<PlayerScript>().selected = false;
        player2.GetComponent<PlayerScript>().selected = true;
    }
}
