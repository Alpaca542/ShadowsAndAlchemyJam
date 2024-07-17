using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.UI;

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
    private Rigidbody2D rb;

    [Header("Defender")]
    public GameObject attackHitbox;

    [Header("Cook")]
    public LayerMask brewerLayer;
    public int amountOfBottles = 0;
    public int max_amountOfBottles = 6;
    public GameObject bottleGrid;

    void Start()
    {
        if (Defender)
        {
            attackHitbox.GetComponent<AttackHitboxScript>().damage = damage;
        }
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetBottle()
    {
        if (amountOfBottles < max_amountOfBottles)
        {
            amountOfBottles++;
            UpdateBottles();
        }
    }

    public void RemoveBottle()
    {
        if (amountOfBottles > 0)
        {
            amountOfBottles--;
            UpdateBottles();
        }
    }

    void UpdateBottles()
    {
        int temp = amountOfBottles;
        foreach (Transform gmb in bottleGrid.transform)
        {
            if (temp > 0)
            {
                temp--;
                gmb.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            else
            {
                gmb.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 5);
            }
        }
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
        transform.parent = seat.transform;
        transform.position = seat.position;
        transform.rotation = seat.rotation;
        rb.velocity = Vector2.zero;
    }

    public void StopSitting()
    {
        sit = false;
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
                float dirX = Input.GetAxis("Horizontal");
                float dirY = Input.GetAxis("Vertical");

                rb.velocity = new Vector2(dirX, dirY) * speed;
                LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }

        }
    }

    bool LookAround()
    {
        return Physics2D.OverlapCircle(transform.position, 2f, brewerLayer);
    }
}
