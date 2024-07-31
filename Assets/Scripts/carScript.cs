using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Scripting.APIUpdating;
using System.Data.Common;
using UnityEngine.SceneManagement;

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
    public AudioSource myEngine;

    public Sprite defSprite;

    private bool forwardPlaying;
    private bool enginePlaying;
    


    public GameObject damageParticle;
    public GameObject dieParticle;
    public GameObject preDieParticle;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        gameObject.GetComponent<soundManager>().PlaySound(0, 1f, 1f);
        Instantiate(damageParticle, transform.position, Quaternion.identity);
        if(health<=20f)
        {
            Instantiate(preDieParticle, transform.position, Quaternion.identity);
        }
        if (health <= 0)
        {
            Instantiate(dieParticle, transform.position, Quaternion.identity);
            gameObject.GetComponent<soundManager>().PlaySound(1, 1f, 1f);
            GameObject.FindWithTag("loser").GetComponent<loser>().lose();
            
        }
    }
    
    void FixedUpdate()
    {
        if (Moveable)
        {
            if (!enginePlaying)
            {
                enginePlaying = true;
                DOTween.To(() => myEngine.volume, x => myEngine.volume = x, 1, 2);
            }
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if (Mathf.Abs(vertical) > 0.1)
            {
                if (horizontal > 0.2f)
                {
                    if (!forwardPlaying)
                    {
                        forwardPlaying = true;
                        // GetComponent<AudioSource>().loop = true;
                        // GetComponent<AudioSource>().Play();
                    }
                }
                else
                {
                    forwardPlaying = false;
                }
                rb.velocity = transform.right * speed * Mathf.Sign(vertical);
                float RotKoef = rb.velocity.magnitude * 0.6f;
                rb.angularVelocity = -horizontal * AngularSpeed * RotKoef;
            }
        }
        else
        {
            DOTween.To(() => myEngine.volume, x => myEngine.volume = x, 0, 1);
            enginePlaying = false;
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
        else if (other.tag == "Defender")
        {
            GameObject.FindGameObjectWithTag("DialogueMng").GetComponent<DialogueScript>().StartCrtnRemotely("I don't think going there is a good idea...", defSprite, true, Camera.main.orthographicSize);
            other.transform.DOMove(other.transform.position + (other.transform.position - transform.position), 0.5f);
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
        if (shouldBeMoved)
        {
            CookInMe = false;
        }
    }

    public void Stand(bool shouldBeMoved)
    {
        if (shouldBeMoved)
            Moveable = false;
        animMover.clip = animClips[3];
        animMover.Play();
        if (shouldBeMoved)
        {
            CookInMe = true;
        }
    }
}
