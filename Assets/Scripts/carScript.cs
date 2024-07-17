using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 10f;
    public float AngularSpeed = 10f;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Mathf.Abs(vertical) > 0.1)
        {


            rb.velocity = transform.right * speed * Mathf.Sign(vertical);
            float RotKoef = rb.velocity.magnitude * 0.6f;
            rb.angularVelocity = -horizontal * AngularSpeed * RotKoef;
        }
    }
}
