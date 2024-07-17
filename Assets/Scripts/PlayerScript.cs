using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float damage;
    public bool Defender;

    [Header("Fields")]
    private bool sit;
    public bool selected;
    private Animator anim;
    public GameObject attackHitbox;
    private Rigidbody2D rb;
    public LayerMask brewerLayer;

    public Transform MySeat;

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
        MySeat = seat;
        transform.position = seat.position;
        transform.rotation = seat.rotation;
        rb.velocity = Vector2.zero;
    }
    public void StopSitting()
    {
        sit = false;
        MySeat = null;
        transform.parent = null;
    }
    void LookAt(Vector3 target)
    {
        if (transform.position != target)
        {
            Vector3 diff = target - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
    }
    void FixedUpdate()
    {
        if (selected)
        {
            if (!sit)
            {
                float dirX = Input.GetAxis("Vertical");
                if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) > 0.6f || dirX < 0)
                {
                    rb.velocity = transform.up * dirX * speed;
                    LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
            }
            else
            {
                transform.position = MySeat.position;

            }

        }
    }

    bool LookAround()
    {
        return Physics2D.OverlapCircle(transform.position, 2f, brewerLayer);
    }
}
