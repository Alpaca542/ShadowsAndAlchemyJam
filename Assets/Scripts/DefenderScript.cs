using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microlight.MicroBar;
using UnityEngine.Video;
using Unity.VisualScripting;
using System;
using TMPro;
using System.Linq;

public class DefenderScript1 : MonoBehaviour
{
    private bool Died;
    public float health;
    public MicroBar healthBar;
    public Gradient healthGradient;

    public GameObject myArrow;

    //public Sprite[] weaponSprites;
    public string[] weaponNames;
    public float[] weaponCDs;
    public int activeWeapon;
    private Animator anim;
    private Rigidbody2D rb;
    public float meleeDamage;

    public float AmountOfBombs;

    public float[] bullets;
    //public float[] bulletsForUse;

    public GameObject myGun;
    // public GameObject bullet;
    // public GameObject rocket;

    public GameObject[] indicators11;
    public TMP_Text[] bulletAmounts;

    private bool CanIShoot = true;
    public bool CanIShoot2 = true;
    public Image WeaponShowcase;

    public int fixedActiveWeapon;

    public bool doIShoot;
    public GameObject red;
    public GameObject green;
    public GameObject blue;
    public GameObject pureRed;
    public GameObject redANDblue;
    public GameObject greenANDblue;
    public GameObject Analyzed;
    public GameObject Graphed;
    public GameObject white;

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

    public bool AmountIsOK()
    {
        if (activeWeapon == 10)
        {
            return true;
        }
        else if (bullets[activeWeapon - 1] > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        if (GetComponent<PlayerScript>().selected)
        {
            if (Input.GetMouseButton(0) && CanIShoot && AmountIsOK())
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

            if (AmountOfBombs > 0)
            {
                myArrow.SetActive(true);
            }
            else
            {
                myArrow.SetActive(false);
            }
        }


        foreach (string gmb in weaponNames)
        {
            int ind = Array.IndexOf(weaponNames, gmb);

            if (GameObject.FindGameObjectWithTag("Cook").GetComponent<CookScript>().inventory.Keys.Contains(gmb))
            {
                bullets[ind] = GameObject.FindGameObjectWithTag("Cook").GetComponent<CookScript>().inventory[gmb];
            }
            else
            {
                bullets[ind] = 0;
            }
            BulletTextUpdate(ind);
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
        if (type == 1)//red
        {
           
            GameObject newBullet = SummonCrystall(red);
            if (newBullet != null)
            {
                newBullet.GetComponent<bulletScript>().damage = 5f;
                newBullet.GetComponent<bulletScript>().fromEnemy = false;
            }
            CanIShoot2 = false;
            Invoke(nameof(makemeshoot2), 1f);
        }
        else if (type == 2)//green
        {
            
            GameObject newBullet = SummonCrystall(green);
            if (newBullet != null)
            {
                newBullet.GetComponent<bulletScript>().damage = 5f;
                newBullet.GetComponent<bulletScript>().fromEnemy = false;
            }
            CanIShoot2 = false;
            Invoke(nameof(makemeshoot2), 1f);
        }
        else if (type == 3)//blue
        {
            
            GameObject newBullet = SummonCrystall(blue); ;
            if (newBullet != null)
            {
                newBullet.GetComponent<bulletScript>().damage = 5f;
                newBullet.GetComponent<bulletScript>().fromEnemy = false;
            }
            CanIShoot2 = false;
            Invoke(nameof(makemeshoot2), 1f);
        }
        else if (type == 4)//pureRed
        {

            CanIShoot = false;
            InvokeRepeating(nameof(SummonBulletWithSpread), 0.1f, 0.1f);
            Invoke(nameof(stopshooting), 1f);



        }
        else if (type == 5)//redANDblue
        {
            if (CanIShoot2)
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Shooting", true);
                anim.SetBool("Hitting", false);

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
            CanIShoot2 = false;
            Invoke(nameof(makemeshoot2), 1f);
        }
        else if (type == 6)//greenANDblue
        {
            // CanIShoot = false;
            
            GameObject newBullet = SummonRocket();
            if (newBullet != null)
            {
                newBullet.GetComponent<bulletScript>().damage = 5f;
                newBullet.GetComponent<bulletScript>().fromEnemy = false;
            }
            CanIShoot2 = false;
            Invoke(nameof(makemeshoot2), 1f);
        }
        else if (type == 7)//graphed
        {
            //CanIShoot = false;
            
            GameObject newBullet = SummonCrystall(Graphed);
            if (newBullet != null)
            {
                newBullet.GetComponent<bulletScript>().damage = 5f;
                newBullet.GetComponent<bulletScript>().fromEnemy = false;
            }
            CanIShoot2 = false;
            Invoke(nameof(makemeshoot2), 1f);
        }
        else if (type == 8)//Analyzed
        {
            
            //CanIShoot = false;
            GameObject newBullet = SummonCrystall(Analyzed);
            if (newBullet != null)
            {
                newBullet.GetComponent<bulletScript>().damage = 5f;
                newBullet.GetComponent<bulletScript>().fromEnemy = false;
            }
            CanIShoot2 = false;
            Invoke(nameof(makemeshoot2), 1f);
        }
        else if (type == 9)//white
        {
            
            //CanIShoot = false;
            GameObject newBullet = SummonCrystall(white);
            if (newBullet != null)
            {
                newBullet.GetComponent<bulletScript>().damage = 5f;
                newBullet.GetComponent<bulletScript>().fromEnemy = false;
            }
            CanIShoot2 = false;
            Invoke(nameof(makemeshoot2), 1f);
        }

        else if (type == 10)//knife
        {
        }

        Invoke(nameof(makemeshoot), weaponCDs[fixedActiveWeapon - 1]);
    }

    private void SummonBulletWithSpread()//pureRed
    {
        if (bullets[fixedActiveWeapon - 1] > 0)
        {
            GameObject newBullet = Instantiate(pureRed, myGun.transform.position, new Quaternion(myGun.transform.rotation.x, myGun.transform.rotation.y, myGun.transform.rotation.z + UnityEngine.Random.Range(-0.3f, 0.3f), myGun.transform.rotation.w));
            newBullet.GetComponent<bulletScript>().damage = 2f;
            newBullet.GetComponent<bulletScript>().fromEnemy = false;
            bullets[fixedActiveWeapon - 1]--;
            BulletTextUpdate(fixedActiveWeapon - 1);
        }
    }

    public void BulletTextUpdate(int which)
    {
        bulletAmounts[which].text = bullets[which].ToString();
    }

    private GameObject SummonBullet()
    {
        if (bullets[fixedActiveWeapon - 1] > 0)
        {
            bullets[fixedActiveWeapon - 1]--;
            BulletTextUpdate(fixedActiveWeapon - 1);
            return Instantiate(redANDblue, myGun.transform.position, new Quaternion(myGun.transform.rotation.x, myGun.transform.rotation.y, myGun.transform.rotation.z, myGun.transform.rotation.w));
        }
        else
        {
            return null;
        }
    }
    private GameObject SummonCrystall(GameObject crystall)
    {
        if (CanIShoot2)
        {



            if (bullets[fixedActiveWeapon - 1] > 0)
            {
                bullets[fixedActiveWeapon - 1]--;
                BulletTextUpdate(fixedActiveWeapon - 1);
                return Instantiate(crystall, myGun.transform.position, new Quaternion(myGun.transform.rotation.x, myGun.transform.rotation.y, myGun.transform.rotation.z, myGun.transform.rotation.w));
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
    private GameObject SummonRocket()
    {
        if (CanIShoot2)
        {
            if (bullets[fixedActiveWeapon - 1] > 0)
            {
                bullets[fixedActiveWeapon - 1]--;
                BulletTextUpdate(fixedActiveWeapon - 1);
                return Instantiate(greenANDblue, myGun.transform.position, new Quaternion(myGun.transform.rotation.x, myGun.transform.rotation.y, myGun.transform.rotation.z, myGun.transform.rotation.w));
            }
            else
            {
                return null;
            }
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
    public void makemeshoot2()
    {
        CancelInvoke(nameof(makemeshoot2));
        CanIShoot2 = true;
    }
    private void AnimateMe()
    {
        if (doIShoot)
        {
            if (activeWeapon == 10)
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
