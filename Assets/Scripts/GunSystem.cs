using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public Transform myCameraHead;
    private UICanvasController myUICanvas;
    public Animator myAnimator;

    public Transform firePosition;
    public GameObject bullet;

    public GameObject muzzleFlash, bulletHole, waterLeak, bloodEffect;

    public bool canAutoFire;
    private bool shooting, readyToShoot = true;
    public float timeBetweenShots;

    public int bulletsAvailable, totalBullets, magazineSize;

    public float reloadTime;
    private bool reloading = false;

    //aiming
    public Transform aimPosition;
    private float aimSpeed = 2f;
    private Vector3 gunStartPosition;
    public float zoomAmount;

    public int damageAmount = 2;
    public string gunName;
    string gunAnimationName;

    public int pickupBulletAmount;


    // Start is called before the first frame update
    void Start()
    {
        totalBullets -= magazineSize;
        bulletsAvailable = magazineSize;

        gunStartPosition = transform.localPosition;

        myUICanvas = FindObjectOfType<UICanvasController>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        GunManager();
        UpdateAmmoText();
        AnimationManager();
    }

    private void AnimationManager()
    {
        switch (gunName)
        {
            case "Pistol":
                gunAnimationName = "PistolReload";
                break;
            case "Rifle":
                gunAnimationName = "RifleReload";
                break;
            default:
                break;
        }
    }

    private void GunManager()
    {
        if (Input.GetKeyDown(KeyCode.R) && bulletsAvailable < magazineSize && !reloading)
        {
            Reload();
        }

        if (Input.GetMouseButton(1))
            transform.position = Vector3.MoveTowards(transform.position, aimPosition.position, aimSpeed * Time.deltaTime);
        else
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, gunStartPosition, aimSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(1))
        {
            FindObjectOfType<CameraMove>().ZoomIn(zoomAmount);
        }

        if (Input.GetMouseButtonUp(1))
            FindObjectOfType<CameraMove>().ZoomOut();
    }

    private void Shoot()
    {
        if (canAutoFire)
        {
            shooting = Input.GetMouseButton(0);
        }
        else
        {
            shooting = Input.GetMouseButtonDown(0);
        }

        //Checking left mouse button
        if (shooting && readyToShoot && bulletsAvailable > 0 && !reloading)
        {
            readyToShoot = false;

            RaycastHit hit;

            if (Physics.Raycast(myCameraHead.position, myCameraHead.forward, out hit, 100f))
            {
                //check distance between firing and object

                if (Vector3.Distance(myCameraHead.position, hit.point) > 2f)
                {
                    firePosition.LookAt(hit.point);
                    if (hit.collider.tag == "Shootable")
                        Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));

                    if (hit.collider.CompareTag("WaterLeak"))
                        Instantiate(waterLeak, hit.point, Quaternion.LookRotation(hit.normal));

                }
                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<EnemyHealthSystem>().TakeDamage(damageAmount);
                    Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
            else
            {
                firePosition.LookAt(myCameraHead.position + (myCameraHead.forward * 50f));
            }

            //reducing num of bullets
            bulletsAvailable--;

            Instantiate(muzzleFlash, firePosition.position, firePosition.rotation, firePosition);
            Instantiate(bullet, firePosition.position, firePosition.rotation, firePosition);

            StartCoroutine(ResetShot());

        }
    }

    public void AddAmmo()
    {
        totalBullets += pickupBulletAmount;
        
    }

    private void Reload()
    {
        myAnimator.SetTrigger(gunAnimationName);

        AudioManager.instance.PlaySFX(7);
       
        reloading = true;
        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        int bulletsToAdd = magazineSize - bulletsAvailable;

        if (totalBullets > bulletsToAdd)
        {
            totalBullets -= bulletsToAdd;
            bulletsAvailable = magazineSize;
        }
        else
        {
            bulletsAvailable += totalBullets;
            totalBullets = 0;
        }
        reloading = false;
        yield return new WaitForSeconds(reloadTime);
        
    }

    IEnumerator ResetShot()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        readyToShoot = true;
    }

    private void UpdateAmmoText()
    {
        myUICanvas.ammoText.SetText(bulletsAvailable + "/" + magazineSize);
        myUICanvas.totalAmmoText.SetText(totalBullets.ToString());
    }
}
