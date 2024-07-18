using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AttackHitboxScript : MonoBehaviour
{
    public float damage;

    private void Start()
    {
        CancelInvoke(nameof(turnOff));
        Invoke(nameof(turnOff), GetComponent<Animation>().clip.length);
    }

    void turnOff()
    {
        gameObject.SetActive(false);
    }
}
