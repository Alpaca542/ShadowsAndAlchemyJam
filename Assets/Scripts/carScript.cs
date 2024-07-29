using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
    public float health;

    public bool CookInMe;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            //Lose
        }
    }
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
            Camera.main.transform.parent.GetComponent<playerFollow>().player = transform;
            animMover.clip = animClips[0];
            animMover.Play();
            CookInMe = true;
            other.transform.DOMove(transform.position, 0.5f);
            other.GetComponent<CookScript>().InCar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Cook")
        {
            Camera.main.GetComponent<playerFollow>().player = other.transform;
            Camera.main.transform.parent.GetComponent<playerFollow>().player = other.transform;
            animMover.clip = animClips[1];
            animMover.Play();
            CookInMe = false;
            other.transform.DOMove(other.transform.position + (other.transform.position - transform.position), 0.5f);
            other.GetComponent<CookScript>().InCar = false;
        }
    }

    public void Sit(bool shouldBeMoved)
    {
        if (shouldBeMoved)
            Moveable = true;
        animMover.clip = animClips[2];
        animMover.Play();
        CookInMe = false;
    }

    public void Stand(bool shouldBeMoved)
    {
        if (shouldBeMoved)
            Moveable = false;
        animMover.clip = animClips[3];
        animMover.Play();
        CookInMe = true;
    }
}
