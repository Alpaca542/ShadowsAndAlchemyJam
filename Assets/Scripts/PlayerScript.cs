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
    public float rotationSmoothTime = 0.2f;
    private float currentVelocity;

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
                Vector2 movement = new Vector2(dirX, dirY).normalized * speed;

                if (Defender)
                {
                    rb.velocity = Vector2.up * dirY * speed + Vector2.right * dirX * speed;
                }
                else if (!Defender)
                {
                    Vector3 director = Vector2.up;
                    Vector3 director2 = Vector2.right;

                    if (gameObject.GetComponent<CookScript>().InCar)
                    {
                        director = car.transform.up;
                        director2 = car.transform.right;
                    }
                    if (Mathf.Abs(movement.x) > 0.2f || Mathf.Abs(movement.y) > 0.2f)
                    {
                        GetComponent<CookScript>().myBody.SetBool("Walking", true);
                        float targetAngle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;
                        float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref currentVelocity, rotationSmoothTime);
                        transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);
                    }
                    else
                    {
                        GetComponent<CookScript>().myBody.SetBool("Walking", false);
                    }


                    rb.velocity = director * dirY * speed + director2 * dirX * speed;
                }



            }

        }
        else
        {
            transform.position = mySeat.transform.position;
        }
    }
}
