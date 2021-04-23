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
        SelectWeapon();
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
        if (selectedWeapon != previousSelectedWeapon) SelectWeapon();
    }

    public event Action onWeaponChangedHook;
    public void onWeaponChanged() {onWeaponChangedHook?.Invoke();}

    void SelectWeapon()
    {
        int i = 0;
        bool found = false;
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(i == selectedWeapon);
            found = found || i == selectedWeapon;
            if (i == selectedWeapon) curWeapon = weapon.GetComponent<WeaponBase>();
            i++;
        }

        curWeapon = found ? curWeapon : null;
        if(PV.IsMine)
            onWeaponChanged();
    }

    public WeaponBase GetCurrentWeapon() => curWeapon;
}
