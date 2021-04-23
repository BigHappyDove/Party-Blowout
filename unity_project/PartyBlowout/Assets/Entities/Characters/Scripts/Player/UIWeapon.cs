using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class UIWeapon : MonoBehaviour
{
    private WeaponInventory _weaponInventory;
    private WeaponBase _curWeapon;
    private PhotonView _pv;
    [SerializeField] private TextMeshProUGUI ammoInMag;
    [SerializeField] private TextMeshProUGUI ammoInReserve;

    void Start()
    {
        _pv = GetComponent<PhotonView>();
        if (!_pv.IsMine)
        {
            Destroy(ammoInMag.transform.parent.gameObject);
            Destroy(this);
            return;
        }
        _weaponInventory = GetComponent<WeaponInventory>();
        if (_weaponInventory == null)
            throw new Exception("UIWeapon: WEAPON INVENTORY NOT FOUND!");
        _curWeapon = _weaponInventory.GetCurrentWeapon();
        UpdateAmmo();
        _weaponInventory.onWeaponChangedHook += ChangeCurWeapon;
        WeaponBase.onWeaponShootHook += UpdateAmmo;
    }

    // So we don't have to check everytime if the weapon changed
    private void OnDestroy()
    {
        if (!_pv.IsMine) return;
        _weaponInventory.onWeaponChangedHook -= ChangeCurWeapon;
        WeaponBase.onWeaponShootHook -= UpdateAmmo;
    }

    /// <summary>
    /// Called by the event onWeaponChanged : Edit the _curWeapon var.
    /// </summary>
    private void ChangeCurWeapon()
    {
        _curWeapon = _weaponInventory.GetCurrentWeapon();
        UpdateAmmo();
    }


    void UpdateAmmo()
    {
        if (!(_curWeapon is null))
        {
            ammoInMag.SetText(_curWeapon.currentAmmoClip.ToString());
            ammoInReserve.SetText("/ " + _curWeapon.ammoInReserve);
        }
        else
        {
            ammoInMag.SetText("");
            ammoInReserve.SetText("");
        }
    }
}
