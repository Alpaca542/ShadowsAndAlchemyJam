using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    private Vector3 playerVector;
    public int speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        playerVector = player.position;
        transform.position = Vector3.Lerp(transform.position, new Vector3(playerVector.x, playerVector.y, transform.position.z), Time.deltaTime * speed);
    }
}
