using Assets.Scripts.Weapons.Guns;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Image _ammoAmount;

    [SerializeField] private GunBase _mainGun;
    [SerializeField] private GunBase _gun2;
    [SerializeField] private GunBase _gun3;
    [SerializeField] private GunBase _gun4;
    private GunBase[] _guns = new GunBase[4];
    
    private GunBase _currentGun;

    // Start is called before the first frame update
    void Start()
    {
        PopulatePlayerGuns();
        _currentGun = _guns[0];
    }
    private void PopulatePlayerGuns()
    {
        _guns[0] = _mainGun;
        _guns[1] = _gun2;
        _guns[2] = _gun3;
        _guns[3] = _gun4;
    }

    private void Update()
    {
        FillAmmoImageWithMagazineAmount();
    }

    private void FillAmmoImageWithMagazineAmount()
    {
        _ammoAmount.fillAmount = _currentGun.AmmoInMagazinePercentage;
    }

    public void ChangeCurrentGun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int weaponIndex = Convert.ToInt32(context.control.name) - 1;
            _currentGun = GetGunFromArray(weaponIndex);
        }
    }
    private GunBase GetGunFromArray(int index)
    {
        if (_guns[index] != null) 
        { 
            return _guns[index];
        }

        return _guns[0];
    }

    public void FireGun(InputAction.CallbackContext context) 
    {
        if(context.performed)
        {
            _currentGun.Shoot();
        }
    }
}
