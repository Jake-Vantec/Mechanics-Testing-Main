using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gun : MonoBehaviour
{
    [Header("Gun Stats")]
    public float damage = 10f;
    public float range = 100f;

    [Header("Ammo Stats")]
    public int currentAmmo = 30; // Your current ammo in a clip
    public int maxAmmo = 30; // Max amount of ammo in a clip
    public int currentMagazineAmmo = 30; // Current amount of ammo your carrying
    public int maxMagazineAmmo = 230; // Max amount of ammo you can carry 
    public int ammoPickupNum = 30; // Amount you get from picking up ammo box
    int shotsFiredInClip = 0;
    public int numOfEnemiesKilled = 0;

    [Header("Visuals")]
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    [SerializeField] Animator animator;

    [Header("Weapon Related UI")]
    [SerializeField] TextMeshProUGUI ammoCount;
    [SerializeField] TextMeshProUGUI enemiesKilledCountText;

    [Header("Audio")]
    [SerializeField] AudioSource shotSFX;
    [SerializeField] AudioSource reloadSFX;

    bool hasAmmo = true;
    bool canShoot = true;


    void Awake()
    {
        animator = GetComponent<Animator>();     
    }

    // Update is called once per frame
    void Update()
    {
        FireWeapon();
        Reload();
        ShootingAnimation();
        TrackAmmo();
        DisplayEnemiesKilled();
    }

    void FireWeapon()
    {
        if (Input.GetButtonDown("Fire1") && currentAmmo > 1)
        {
            if(canShoot == true)
            {
                Shoot();
                shotSFX.Play();
                muzzleFlash.Play();
                currentAmmo -= 1;
                shotsFiredInClip += 1;
                StartCoroutine(CanShoot());
            }
        }
        else if (currentAmmo <= 0)
        {
            Debug.Log("Out of ammo!");
        }
    }

    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && hasAmmo == true)
        {
            reloadSFX.Play();
            currentMagazineAmmo -= shotsFiredInClip;
            currentAmmo = maxAmmo;
            shotsFiredInClip = 0;
            StartCoroutine(CanShootAfterReload());
        }
    }

    void Shoot()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo, range))
        {
            Target target = hitInfo.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);

                if(target.health <= 0f)
                {
                    numOfEnemiesKilled += 1;
                    Debug.Log("Enemy Killed!");
                }
            }
        }
    }

    void TrackAmmo()
    {
        ammoCount.text = "Ammo: " + currentAmmo.ToString() + "/" + currentMagazineAmmo.ToString(); 

        if(currentMagazineAmmo <= 0)
        {
            currentMagazineAmmo = 0;
            hasAmmo = false;
        }
        else
            hasAmmo = true;
    }


    void DisplayEnemiesKilled()
    {
        enemiesKilledCountText.text = "Enemies Killed: " + numOfEnemiesKilled.ToString();
    }

    void ShootingAnimation()
    {
        if (Input.GetButtonDown("Fire1") && currentAmmo >= 0.1)
        {
            animator.SetBool("isShooting", true);
        }
        else
        {
            Invoke("ResetIsShootingAnim", 0.9f);
        }
    }

    void ResetIsShootingAnim()
    {
        animator.SetBool("isShooting", false);
    }

    IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.35f);
        canShoot = true;
    }
    IEnumerator CanShootAfterReload()
    {
        canShoot = false;
        animator.SetBool("isShooting", false);
        yield return new WaitForSeconds(1.5f);
        canShoot = true;
    }
}
