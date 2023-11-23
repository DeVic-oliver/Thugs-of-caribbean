using Assets.Scripts.Weapons.Guns;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Image _currentGunAmmoImage;
    [SerializeField] private Image _currentGunAmmoImageBackground;
    [SerializeField] protected TextMeshProUGUI _ammoStatusText;
    private bool _isAmmoTextDisplayed;

    [SerializeField] private TextMeshProUGUI _ammoAmountUI;

    [SerializeField] private GunBase _mainGun;
    [SerializeField] private GunBase _gun2;
    private GunBase[] _guns = new GunBase[2];
    
    private GunBase _currentGun;


    void Start()
    {
        PopulatePlayerGuns();
        _currentGun = _guns[0];
        _ammoAmountUI.text = _currentGun.GetMagazineAmmoAmount().ToString();
        _isAmmoTextDisplayed = false;
        ChangeCurrentGunSprites();
    }
    private void PopulatePlayerGuns()
    {
        _guns[0] = _mainGun;
        _guns[1] = _gun2;
    }

    private void Update()
    {
        DisplayAmmoTextIfCurrentGunIsReloading();
        UpdateAmmoAmountUI();
    }
    private void DisplayAmmoTextIfCurrentGunIsReloading()
    {
        if(!_isAmmoTextDisplayed && _currentGun.IsReloading) 
        {
            _ammoStatusText.gameObject.SetActive(true);
            _isAmmoTextDisplayed = true;
        }else if (_isAmmoTextDisplayed && !_currentGun.IsReloading)
        {
            _ammoStatusText.gameObject.SetActive(false);
            _isAmmoTextDisplayed = false;
        }
    }
    private void UpdateAmmoAmountUI()
    {
        _ammoAmountUI.text = _currentGun.GetMagazineAmmoAmount().ToString();
        FillAmmoImageWithMagazineAmount();
    }

    private void FillAmmoImageWithMagazineAmount()
    {
        _currentGunAmmoImage.fillAmount = _currentGun.AmmoInMagazinePercentage;
    }

    public void ChangeCurrentGun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int weaponIndex = Convert.ToInt32(context.control.name) - 1;
            _currentGun = GetGunFromArray(weaponIndex);
            ChangeCurrentGunSprites();
        }
    }
    private void ChangeCurrentGunSprites()
    {
        _currentGunAmmoImage.sprite = _currentGun.GetProjectileSprite();
        _currentGunAmmoImageBackground.sprite = _currentGun.GetProjectileSprite();
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
