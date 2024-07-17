using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DefenderScript : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float damage;
    [Header("Fields")]
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float dirX = Input.GetAxis("Vertical");
        float dirY = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(dirX, dirY);
    }
}
