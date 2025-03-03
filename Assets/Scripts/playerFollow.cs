using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    private Vector3 playerVector;
    public GameObject myUI;
    public int speed;

    // Update is called once per frame
    void LateUpdate()
    {
        playerVector = player.position;
        playerVector.z = -10;
        transform.position = Vector3.Lerp(transform.position, playerVector, speed * Time.fixedDeltaTime);
        if (speed > 0)
        {
            if (player.GetComponent<carScript>() != null)
            {
                if (player.GetComponent<carScript>().CookInMe)
                    transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, speed * Time.fixedDeltaTime);
                else
                    transform.rotation = Quaternion.identity;
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, speed * Time.fixedDeltaTime);
            }
        }
    }
}
