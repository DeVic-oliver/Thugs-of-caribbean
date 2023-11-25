namespace Devic.Scripts.Utils.Pools
{
    using Assets.Scripts.Core.Components.Projectile;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Pool;
    
    public class CannonBallPool
    {
        private CannonBall _projectile;
        private bool _collectionChecks = true;
        private int _maxPoolSize = 20;
        private IObjectPool<CannonBall> _pool;


        public CannonBallPool(CannonBall projectile)
        {
            _projectile = projectile;
            _pool = new ObjectPool<CannonBall>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, _collectionChecks, 10, _maxPoolSize);
        }

        public CannonBall GetProjectileFromPool()
        {
            var projectile = _pool.Get();
            return projectile;
        }

        public List<CannonBall> GetProjectileFromPool(int qtd)
        {
            List<CannonBall> list = new();
            for (int i = 0; i < qtd; i++)
            {
                var projectile = _pool.Get();
                list.Add(projectile);
            }
            return list;
        }

        public void BackToPool(CannonBall projectileToBack)
        {
            _pool.Release(projectileToBack);
        }

        public void SetProjectilPoolIfItHasNone(CannonBall projectile)
        {
            if (projectile.CannonBallPool == null)
                projectile.CannonBallPool = this;
        }

        private CannonBall CreatePooledItem()
        {
            var instance = MonoBehaviour.Instantiate(_projectile);
            return instance;
        }

        private void OnReturnedToPool(CannonBall projectil)
        {
            projectil.gameObject.SetActive(false);
        }

        private void OnTakeFromPool(CannonBall projectil)
        {
            projectil.gameObject.SetActive(true);
        }

        private void OnDestroyPoolObject(CannonBall projectil)
        {
            MonoBehaviour.Destroy(projectil);
        }

    }
}