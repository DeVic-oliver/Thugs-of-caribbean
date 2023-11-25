namespace Assets.Scripts.Player
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Devic.Scripts.Utils.Pools;
    using System.Collections.Generic;
    using Assets.Scripts.Core.Components.Projectile;
    using System.Collections;
    using Assets.Scripts.Core.Enums;
    using Assets.Scripts.Core.Enums.Parser;
    using UnityEngine.Events;

    public class PlayerAttack : MonoBehaviour
    {
        public static int ShootsRemaing { get; private set; }
        public static float ShootsRemaingPercentage { get; private set; }
        public static bool IsReloading { get; private set; }
        public static CannonTypes CurrentCannon { get; private set; }

        public UnityEvent<CannonTypes> OnSwapCannonType;
        public UnityEvent OnShoot;


        [Header("Cannons Setup")]
        [SerializeField] private float _reloadTime;
        [SerializeField] private int _shootsBeforeReload;
        [SerializeField] private CannonBall _cannonBall;
        [SerializeField] private Transform _mainCannon;
        [SerializeField] private List<Transform> _cannons;


        private CannonBallPool _pool;


        private void Awake()
        {
            _pool = new(_cannonBall);
        }

        void Start()
        {
            ShootsRemaing = _shootsBeforeReload;
            IsReloading = false;
            CurrentCannon = CannonTypes.Single;
        }

        private void Update()
        {
            AutoReloadIfNoAmmo();
            ShootsRemaingPercentage = GetShotsPercentage();
        }

        private void AutoReloadIfNoAmmo()
        {
            if (!HasShootsRemaning() && !IsReloading)
            {
                IsReloading = true;
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
            IsReloading = false;
        }

        private float GetShotsPercentage()
        {
            return ((ShootsRemaing * 100f) / _shootsBeforeReload) / 100f;
        }

        public void ChangeCurrentCannonType(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                CurrentCannon = (CannonTypeParser.IsSingleCannon(CurrentCannon)) ? CannonTypes.Multiple : CannonTypes.Single;
                OnSwapCannonType?.Invoke(CurrentCannon);
            }
        }

        public void FireCannon(InputAction.CallbackContext context) 
        {
            if(context.performed && HasShootsRemaning())
            {
                OnShoot?.Invoke();

                if (CurrentCannon == CannonTypes.Single)
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
