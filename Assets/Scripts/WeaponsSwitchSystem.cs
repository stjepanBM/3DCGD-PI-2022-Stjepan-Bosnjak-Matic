using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSwitchSystem : MonoBehaviour
{
    private GunSystem activeGun;
    public List<GunSystem> allGuns = new List<GunSystem>();
    public int currentGunNumber;

    void Start()
    {
        foreach (GunSystem gun in allGuns)
        {
            gun.gameObject.SetActive(false);
        }

        activeGun = allGuns[currentGunNumber];
        activeGun.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchGun();
        }
    }

    private void SwitchGun()
    {
        activeGun.gameObject.SetActive(false);
        currentGunNumber++;

        AudioManager.instance.PlaySFX(6);

        if (currentGunNumber >= allGuns.Count)
        {
            currentGunNumber = 0;
        }
        else
        {
            activeGun = allGuns[currentGunNumber];
            activeGun.gameObject.SetActive(true);
        }
    }
}
