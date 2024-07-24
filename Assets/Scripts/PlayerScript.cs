using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float damage;
    public bool Defender;

    [Header("Fields")]
    public bool sit;
    public bool selected;
    private Animator anim;
    private Rigidbody2D rb;
    public GameObject mySeat;

    [Header("Cook")]
    public LayerMask brewerLayer;


    public GameObject car;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Sit(Transform seat)
    {
        sit = true;
        mySeat = seat.gameObject;
        transform.position = seat.position;
        transform.rotation = seat.rotation;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

    public void StopSitting()
    {
        sit = false;
        mySeat = null;
        rb.isKinematic = false;
    }

    void FixedUpdate()
    {
        if (!sit)
        {
            if (selected)
            {
                float dirX = Input.GetAxis("Horizontal");
                float dirY = Input.GetAxis("Vertical");

                if(gameObject.GetComponent<DefenderScript1>()!=null)
                {

                    rb.velocity = Vector2.up * dirY * speed + Vector2.right * dirX * speed;
                
                }
                else if(gameObject.GetComponent<CookScript>() != null)
                {
                    Vector3 director = transform.up;
                Vector3 director2 = transform.right;
                
                    if (gameObject.GetComponent<CookScript>().InCar)
                    {
                        director = car.transform.up;
                        director2 = car.transform.right;
                    }
                
                    
                    rb.velocity = director * dirY * speed+ director2 * dirX * speed;
                }
                
               

            }

        }
        else
        {
            transform.position = mySeat.transform.position;
        }
    }
}
