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

    [Header("Defender")]
    public GameObject attackHitbox;

    [Header("Cook")]
    public LayerMask brewerLayer;

    void Start()
    {
        if (Defender)
        {
            attackHitbox.GetComponent<AttackHitboxScript>().damage = damage;
        }
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Attack()
    {
        attackHitbox.SetActive(true);
    }

    private void AnimateMe()
    {
        if (rb.velocity.magnitude >= 0.2f)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    private void Update()
    {
        if (selected)
        {
            if (Defender)
            {
                if (Input.GetMouseButton(0) && !attackHitbox.activeSelf)
                    Attack();
            }
            else
            {
                if (LookAround())
                {
                    //LookAround().machine.activate
                }
            }
            AnimateMe();
        }
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

                rb.velocity = new Vector2(dirX, dirY) * speed;
                //LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }

        }
        else
        {
            transform.position = mySeat.transform.position;
        }
    }

    bool LookAround()
    {
        return Physics2D.OverlapCircle(transform.position, 2f, brewerLayer);
    }
}
