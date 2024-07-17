using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerScript : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float damage;
    public bool Defender;

    [Header("Fields")]
    private Animator anim;
    public GameObject attackHitbox;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Attack()
    {
        attackHitbox.SetActive(true);
    }
    void Update()
    {
        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");
        if (Defender)
        {
            if (Input.GetMouseButton(0) && !attackHitbox.activeSelf)
                Attack();
        }

        rb.velocity = new Vector2(dirX, dirY) * speed;
    }
}
