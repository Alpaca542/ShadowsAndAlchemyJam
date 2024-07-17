using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitboxScript : MonoBehaviour
{
    public float damage;

    private void Start()
    {
        Invoke(nameof(turnOff), GetComponent<Animation>().clip.length);
    }

    void turnOff()
    {
        gameObject.SetActive(false);
    }
}
