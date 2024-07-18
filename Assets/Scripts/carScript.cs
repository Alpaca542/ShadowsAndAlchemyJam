using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class carScript : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 10f;
    public float AngularSpeed = 10f;

    [Header("Fileds")]
    public AnimationClip[] animClips;
    public Animation animMover;
    private Rigidbody2D rb;
    public bool Moveable = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Moveable)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Cook")
        {
            Camera.main.GetComponent<playerFollow>().player = transform;
            animMover.clip = animClips[0];
            animMover.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Cook")
        {
            Camera.main.GetComponent<playerFollow>().player = other.transform;
            animMover.clip = animClips[1];
            animMover.Play();
        }
    }

    public void Sit()
    {
        Moveable = true;
        animMover.clip = animClips[2];
        animMover.Play();
    }

    public void Stand()
    {
        Moveable = false;
        animMover.clip = animClips[3];
        animMover.Play();
    }
}
