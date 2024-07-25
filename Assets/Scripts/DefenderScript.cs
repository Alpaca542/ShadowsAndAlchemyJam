using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microlight.MicroBar;
using UnityEngine.Video;
using Unity.VisualScripting;

public class DefenderScript1 : MonoBehaviour
{
    private bool Died;
    public float health;
    public MicroBar healthBar;
    public Gradient healthGradient;

    public Sprite[] weaponSprites;
    public string[] weaponNames;
    public float[] weaponCDs;
    public int activeWeapon;
    private Animator anim;
    private Rigidbody2D rb;
    public float meleeDamage;

    public GameObject myGun;
    public GameObject bullet;
    public GameObject rocket;

    private bool CanIShoot = true;

    public Image WeaponShowcase;

    public void GetItem(string whatItem)
    {
        //idk
    }

    private void Update()
    {
        if (GetComponent<PlayerScript>().selected)
        {
            if (Input.GetMouseButtonDown(0) && CanIShoot)
            {
                CanIShoot = false;
                Shoot(activeWeapon);
            }
            else
            {
                AnimateMe();
            }
        }
    }
    private void FixedUpdate()
    {
        if (GetComponent<PlayerScript>().selected)
        {
            LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
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

    public void Shoot(int type)
    {
        if (type == 1)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Shooting", true);
            anim.SetBool("Hitting", false);

            InvokeRepeating(nameof(SummonBulletWithSpread), 0.1f, 0.1f);
            Invoke(nameof(stopshooting), 1f);
        }
        else if (type == 2)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Shooting", true);
            anim.SetBool("Hitting", false);

            int angle = 40;
            for (int i = 0; i < 10; i++)
            {
                GameObject newBullet = SummonBullet();
                newBullet.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + angle);
                newBullet.GetComponent<bulletScript>().damage = 2f;
                newBullet.GetComponent<bulletScript>().fromEnemy = false;
                angle -= 8;
            }
        }
        else if (type == 3)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Shooting", true);
            anim.SetBool("Hitting", false);

            GameObject newBullet = SummonRocket();
            newBullet.GetComponent<bulletScript>().damage = 5f;
            newBullet.GetComponent<bulletScript>().fromEnemy = false;
        }
        else if (type == 4)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Shooting", false);
            anim.SetBool("Hitting", true);
        }

        Invoke(nameof(makemeshoot), weaponCDs[activeWeapon - 1]);
    }

    private void SummonBulletWithSpread()
    {
        GameObject newBullet = Instantiate(bullet, myGun.transform.position, new Quaternion(myGun.transform.rotation.x, myGun.transform.rotation.y, myGun.transform.rotation.z + Random.Range(-0.3f, 0.3f), myGun.transform.rotation.w));
        newBullet.GetComponent<bulletScript>().damage = 2f;
        newBullet.GetComponent<bulletScript>().fromEnemy = false;
    }

    private GameObject SummonBullet()
    {
        return Instantiate(bullet, myGun.transform.position, new Quaternion(myGun.transform.rotation.x, myGun.transform.rotation.y, myGun.transform.rotation.z, myGun.transform.rotation.w));
    }

    private GameObject SummonRocket()
    {
        return Instantiate(rocket, myGun.transform.position, new Quaternion(myGun.transform.rotation.x, myGun.transform.rotation.y, myGun.transform.rotation.z, myGun.transform.rotation.w));
    }

    void stopshooting()
    {
        CancelInvoke(nameof(SummonBulletWithSpread));
    }

    public void makemeshoot()
    {
        CanIShoot = true;
    }

    private void AnimateMe()
    {
        if (rb.velocity.magnitude >= 0.2f)
        {
            anim.SetBool("Walking", true);
            anim.SetBool("Shooting", false);
            anim.SetBool("Hitting", false);
        }
        else
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Shooting", false);
            anim.SetBool("Hitting", false);
        }
    }

    public void TakeDamage(float dmg)
    {
        if (!Died)
        {
            if (health <= 0)
            {
                Died = true;
                Invoke(nameof(Die), 1f);
            }
            else
            {
                health -= dmg;
                healthBar.UpdateBar(health);
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        CanIShoot = true;
        healthBar.Initialize(health);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
}
