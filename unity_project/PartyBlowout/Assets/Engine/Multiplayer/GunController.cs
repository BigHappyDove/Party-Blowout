using System;
using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Gun Settings")]
    public float fireRate = 0.1f;
    public int clipSize = 30;
    public int reservedAmmoCapacity = 270;
    public float recoil = 1f;

    bool canShoot;
    int currentAmmoClip;
    int ammoInReserve;

    private void Start()
    {
        currentAmmoClip = clipSize;
        ammoInReserve = reservedAmmoCapacity;
        canShoot = true;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && canShoot && currentAmmoClip > 0)
        {
            canShoot = false;
            currentAmmoClip--;
            StartCoroutine(ShootGun());
        }
    }

    IEnumerator ShootGun()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
