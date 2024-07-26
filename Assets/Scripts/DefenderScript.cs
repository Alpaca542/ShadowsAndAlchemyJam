using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microlight.MicroBar;
using UnityEngine.Video;
using Unity.VisualScripting;
using System.Dynamic;
using TMPro;

public class DefenderScript1 : MonoBehaviour
{
    private bool Died;
    public float health;
    public MicroBar healthBar;
    public Gradient healthGradient;

    public Sprite[] weaponSprites;
    // public string[] weaponNames;
    public float[] weaponCDs;
    public int activeWeapon;
    private Animator anim;
    private Rigidbody2D rb;
    public float meleeDamage;

    public float[] bullets;
    //public float[] bulletsForUse;

    public GameObject myGun;
    public GameObject bullet;
    public GameObject rocket;

    public GameObject[] indicators11;
    public TMP_Text[] bulletAmounts;

    private bool CanIShoot = true;

    public Image WeaponShowcase;

    public int fixedActiveWeapon;

    public bool doIShoot;

    public void GetItem(string whatItem)
    {
        if (whatItem == "heal")
        {
            TakeDamage(-100);
        }
    }

    public void SetWeapon(int which)
    {
        activeWeapon = which;
        for (int i = 0; i < indicators11.Length; i++)
        {
            if (i == which)
            {
                indicators11[i].SetActive(true);
            }
            else
            {
                indicators11[i].SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (GetComponent<PlayerScript>().selected)
        {
            if (Input.GetMouseButton(0) && CanIShoot)
            {
                fixedActiveWeapon = activeWeapon;
                Shoot(fixedActiveWeapon);
                doIShoot = true;
                AnimateMe();
            }
            else if (activeWeapon != 1)
            {
                doIShoot = false;
                AnimateMe();
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
            if (bullets[activeWeapon] > 0)
            {
                CanIShoot = false;
                InvokeRepeating(nameof(SummonBulletWithSpread), 0.1f, 0.1f);
                Invoke(nameof(stopshooting), 1f);
            }
        }
        else if (type == 2)
        {
            if (bullets[activeWeapon] > 0)
            {
                CanIShoot = false;
                int angle = 40;
                for (int i = 0; i < 10; i++)
                {
                    GameObject newBullet = SummonBullet();
                    if (newBullet != null)
                    {
                        newBullet.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + angle);
                        newBullet.GetComponent<bulletScript>().damage = 2f;
                        newBullet.GetComponent<bulletScript>().fromEnemy = false;
                    }
                    angle -= 8;
                }
            }
        }
        else if (type == 3)
        {
            if (bullets[activeWeapon] > 0)
            {
                CanIShoot = false;
                GameObject newBullet = SummonRocket();
                if (newBullet != null)
                {
                    newBullet.GetComponent<bulletScript>().damage = 5f;
                    newBullet.GetComponent<bulletScript>().fromEnemy = false;
                }
            }
        }
        else if (type == 4)
        {
        }
        else if (type == 5)
        {
            if (bullets[activeWeapon] > 0)
            {
                CanIShoot = false;
                GameObject newBullet = SummonRocket();
                if (newBullet != null)
                {
                    newBullet.GetComponent<bulletScript>().damage = 5f;
                    newBullet.GetComponent<bulletScript>().fromEnemy = false;
                }
            }
        }
        else if (type == 6)
        {
            if (bullets[activeWeapon] > 0)
            {
                CanIShoot = false;
                GameObject newBullet = SummonRocket();
                if (newBullet != null)
                {
                    newBullet.GetComponent<bulletScript>().damage = 5f;
                    newBullet.GetComponent<bulletScript>().fromEnemy = false;
                }
            }
        }
        else if (type == 7)
        {
            if (bullets[activeWeapon] > 0)
            {
                CanIShoot = false;
                GameObject newBullet = SummonRocket();
                if (newBullet != null)
                {
                    newBullet.GetComponent<bulletScript>().damage = 5f;
                    newBullet.GetComponent<bulletScript>().fromEnemy = false;
                }
            }
        }
        else if (type == 8)
        {
            if (bullets[activeWeapon] > 0)
            {
                CanIShoot = false;
                GameObject newBullet = SummonRocket();
                if (newBullet != null)
                {
                    newBullet.GetComponent<bulletScript>().damage = 5f;
                    newBullet.GetComponent<bulletScript>().fromEnemy = false;
                }
            }
        }
        else if (type == 9)
        {
            if (bullets[activeWeapon] > 0)
            {
                CanIShoot = false;
                GameObject newBullet = SummonRocket();
                if (newBullet != null)
                {
                    newBullet.GetComponent<bulletScript>().damage = 5f;
                    newBullet.GetComponent<bulletScript>().fromEnemy = false;
                }
            }
        }
        else if (type == 10)
        {
            if (bullets[activeWeapon] > 0)
            {
                CanIShoot = false;
                GameObject newBullet = SummonRocket();
                if (newBullet != null)
                {
                    newBullet.GetComponent<bulletScript>().damage = 5f;
                    newBullet.GetComponent<bulletScript>().fromEnemy = false;
                }
            }
        }

        Invoke(nameof(makemeshoot), weaponCDs[fixedActiveWeapon - 1]);
    }

    private void SummonBulletWithSpread()
    {
        if (bullets[fixedActiveWeapon - 1] > 0)
        {
            GameObject newBullet = Instantiate(bullet, myGun.transform.position, new Quaternion(myGun.transform.rotation.x, myGun.transform.rotation.y, myGun.transform.rotation.z + Random.Range(-0.3f, 0.3f), myGun.transform.rotation.w));
            newBullet.GetComponent<bulletScript>().damage = 2f;
            newBullet.GetComponent<bulletScript>().fromEnemy = false;
            bullets[fixedActiveWeapon - 1]--;
            BulletTextUpdate(fixedActiveWeapon - 1);
        }
    }

    private void BulletTextUpdate(int which)
    {
        bulletAmounts[which].text = bullets[which].ToString();
    }

    private GameObject SummonBullet()
    {
        if (bullets[fixedActiveWeapon - 1] > 0)
        {
            bullets[fixedActiveWeapon - 1]--;
            BulletTextUpdate(fixedActiveWeapon - 1);
            return Instantiate(bullet, myGun.transform.position, new Quaternion(myGun.transform.rotation.x, myGun.transform.rotation.y, myGun.transform.rotation.z, myGun.transform.rotation.w));
        }
        else
        {
            return null;
        }
    }

    private GameObject SummonRocket()
    {
        if (bullets[fixedActiveWeapon - 1] > 0)
        {
            bullets[fixedActiveWeapon - 1]--;
            BulletTextUpdate(fixedActiveWeapon - 1);
            return Instantiate(rocket, myGun.transform.position, new Quaternion(myGun.transform.rotation.x, myGun.transform.rotation.y, myGun.transform.rotation.z, myGun.transform.rotation.w));
        }
        else
        {
            return null;
        }
    }

    void stopshooting()
    {
        CancelInvoke(nameof(SummonBulletWithSpread));
        doIShoot = false;
    }

    public void makemeshoot()
    {
        CanIShoot = true;
    }

    private void AnimateMe()
    {
        if (doIShoot)
        {
            if (activeWeapon == 4)
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Shooting", false);
                anim.SetBool("Hitting", true);
            }
            else
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Shooting", true);
                anim.SetBool("Hitting", false);
            }

        }
        else if (rb.velocity.magnitude >= 0.2f)
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
                Mathf.Clamp(health, 0, 100);
                if (dmg > 0)
                {
                    healthBar.UpdateBar(health, UpdateAnim.Damage);
                }
                else
                {
                    healthBar.UpdateBar(health, UpdateAnim.Heal);
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
