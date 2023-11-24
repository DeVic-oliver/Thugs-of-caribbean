namespace Assets.Scripts.Player
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.UI;
    using TMPro;
    using Devic.Scripts.Utils.Pools;
    using System.Collections.Generic;
    using Assets.Scripts.Core.Components.Projectile;
    using System.Collections;

    public class PlayerAttack : MonoBehaviour
    {
        public enum CannonTypes
        {
            Single,
            Multiple
        }

        public int ShootsRemaing { get; private set; }

        [Header("Visual Feedback")]
        [SerializeField] private Image _currentGunAmmoImage;
        [SerializeField] private Image _currentGunAmmoImageBackground;
        [SerializeField] protected TextMeshProUGUI _ammoStatusText;
        [SerializeField] private TextMeshProUGUI _ammoAmountUI;

        [Header("Cannons Setup")]
        [SerializeField] private float _reloadTime;
        [SerializeField] private int _shootsBeforeReload;
        [SerializeField] private CannonBall _cannonBall;
        [SerializeField] private Transform _mainCannon;
        [SerializeField] private List<Transform> _cannons;


        private CannonBallPool _pool;
        private bool _isReloading;
        private bool _isAmmoTextDisplayed;
        private CannonTypes _currentCannon;


        private void Awake()
        {
            _pool = new(_cannonBall);
        }

        void Start()
        {
            ShootsRemaing = _shootsBeforeReload;
            _isReloading = false;
        }

        private void Update()
        {
            AutoReloadIfNoAmmo();
            //IfReloadingDisplayText();
            //UpdateAmmoAmountUI();
        }

        private void AutoReloadIfNoAmmo()
        {
            if (!HasShootsRemaning() && !_isReloading)
            {
                _isReloading = true;
                StartCoroutine(nameof(ReloadShoots));
            }
        }

        private IEnumerator ReloadShoots()
        {
            var count = 0f;
            while (count <= _reloadTime)
            {
                count += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            ShootsRemaing = _shootsBeforeReload;
            _isReloading = false;
        }

        private void IfReloadingDisplayText()
        {
            if(!_isAmmoTextDisplayed && _isReloading) 
            {
                _ammoStatusText.gameObject.SetActive(true);
                _isAmmoTextDisplayed = true;
            }
        }

        private void UpdateAmmoAmountUI()
        {
            _ammoAmountUI.text = ShootsRemaing.ToString();
            _currentGunAmmoImage.fillAmount = GetAmmoPercentage();
        }

        private float GetAmmoPercentage()
        {
            return ((ShootsRemaing * 100f) / _shootsBeforeReload) / 100f;
        }

        public void ChangeCurrentCannonType(InputAction.CallbackContext context)
        {
            if (context.performed)
                _currentCannon = (_currentCannon == CannonTypes.Single) ? CannonTypes.Multiple : CannonTypes.Single;
        }

        public void FireCannon(InputAction.CallbackContext context) 
        {
            if(context.performed && HasShootsRemaning())
            {
                if(_currentCannon == CannonTypes.Single)
                {
                    CreateCannonBall(_mainCannon.transform);
                }
                else
                {
                    foreach (Transform cannonTransfom in _cannons)
                        CreateCannonBall(cannonTransfom);
                }

                ShootsRemaing--;
            }
        }

        private bool HasShootsRemaning()
        {
            return (ShootsRemaing > 0);
        }

        private void CreateCannonBall(Transform transfomToCannonBall)
        {
            CannonBall ball = _pool.GetProjectileFromPool();
            ball.transform.SetPositionAndRotation(transfomToCannonBall.position, transfomToCannonBall.rotation);
            _pool.SetProjectilPoolIfItHasNone(ball);
        }

    }

}
