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
    private Animator anim;
    public GameObject attackHitbox;
    private Rigidbody2D rb;
    public LayerMask brewerLayer;

    void Start()
    {
        attackHitbox.GetComponent<AttackHitboxScript>().damage = damage;
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
        LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        AnimateMe();
    }
    public void Sit(Transform seat)
    {
        sit = true;
        transform.parent = seat.transform;
        transform.position = seat.position;
        transform.rotation = seat.rotation;
    }
    public void StopSitting()
    {
        sit = false;
        transform.parent = null;
    }
    void LookAt(Vector3 target)
    {
        Quaternion rotation = Quaternion.LookRotation(target - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }
    void FixedUpdate()
    {
        if (!sit)
        {
            float dirX = Input.GetAxis("Horizontal");
            float dirY = Input.GetAxis("Vertical");

            rb.velocity = new Vector2(dirX, dirY) * speed;
        }

    }

    bool LookAround()
    {
        return Physics2D.OverlapCircle(transform.position, 2f, brewerLayer);
    }
}
