using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public int selectedWeapon = 0;
    private WeaponBase curWeapon = null;
    private PhotonView PV;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        PV.RPC("RPC_SelectWeapon", RpcTarget.All, selectedWeapon);
    }

    void Update()
    {
        if(!PV.IsMine) return;
        int previousSelectedWeapon = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedWeapon = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2) selectedWeapon = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3) selectedWeapon = 2;
        if (selectedWeapon != previousSelectedWeapon)
        {
            PV.RPC("RPC_SelectWeapon", RpcTarget.All, selectedWeapon);
            // SelectWeapon();
        }
    }

    public event Action onWeaponChangedHook;

    public void onWeaponChanged() => onWeaponChangedHook?.Invoke();

    [PunRPC]
    void RPC_SelectWeapon(int rpcSelectedWeapon)
    {
        int i = 0;
        bool found = false;
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(i == rpcSelectedWeapon);
            found = found || i == rpcSelectedWeapon;
            if (i == rpcSelectedWeapon && PV != null && PV.IsMine)
                curWeapon = weapon.GetComponent<WeaponBase>();
            i++;
        }

        if(PV != null && PV.IsMine)
        {
            curWeapon = found ? curWeapon : null;
            if (!(curWeapon is null))
                curWeapon.canShoot = true; // Switching while shooting can stuck canShoot, this fix the issue
            onWeaponChanged();
        }
    }

    public WeaponBase GetCurrentWeapon() => curWeapon;
}
