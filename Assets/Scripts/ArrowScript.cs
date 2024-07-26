using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private float currentVelocity = 0;
    private void Update()
    {
        Vector2 movement = nearestPedestal().transform.position - transform.position;

        float targetAngle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;
        float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref currentVelocity, 0.2f);
        transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);
    }

    GameObject nearestPedestal()
    {
        GameObject[] pedestals = GameObject.FindGameObjectsWithTag("BombPoint");
        GameObject ClosestGmb = pedestals[0];

        foreach (GameObject gmb in pedestals)
        {
            if (Vector2.Distance(transform.position, gmb.transform.position) < Vector2.Distance(transform.position, ClosestGmb.transform.position))
            {
                ClosestGmb = gmb;
            }
        }
        return ClosestGmb;
    }
}
