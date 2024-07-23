using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microlight.MicroBar;
using UnityEngine.Video;

public class DefenderScript1 : MonoBehaviour
{
    private bool Died;
    private float health;
    public MicroBar healthBar;
    public Gradient healthGradient;

    public Sprite[] weaponSprites;
    public Sprite[] weaponNames;
    public int activeWeapon;
    private Animator anim;
    private Rigidbody2D rb;

    public GameObject myGun;
    public GameObject bullet;
    public GameObject rocket;

    private bool CanIShoot = true;

    public Image WeaponShowcase;

    private void Update()
    {
        if (GetComponent<PlayerScript>().selected)
        {
            if (Input.GetMouseButtonDown(0) && CanIShoot)
            {
                CanIShoot = false;
                Shoot(activeWeapon);
            }
            LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            AnimateMe();
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
            InvokeRepeating(nameof(SummonBulletWithSpread), 0.1f, 0.1f);
            Invoke(nameof(stopshooting), 1f);
        }
        else if (type == 2)
        {
            int angle = 50;
            for (int i = 0; i < 11; i++)
            {
                GameObject newBullet = SummonBullet();
                newBullet.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + angle);
                newBullet.GetComponent<bulletScript>().damage = 2f;
                newBullet.GetComponent<bulletScript>().fromEnemy = false;
                angle -= 10;
            }
        }
        else if (type == 3)
        {
            GameObject newBullet = SummonRocket();
            newBullet.GetComponent<bulletScript>().damage = 5f;
            newBullet.GetComponent<bulletScript>().fromEnemy = false;
        }

        Invoke(nameof(makemeshoot), 3f);
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
        }
        else
        {
            anim.SetBool("Walking", false);
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
                if (health - dmg > health)
                {
                    healthBar.UpdateBar(health, UpdateAnim.Heal);
                }
                else
                {
                    healthBar.UpdateBar(health, UpdateAnim.Damage);
                }
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
