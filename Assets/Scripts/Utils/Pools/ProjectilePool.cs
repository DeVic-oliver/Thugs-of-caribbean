using Assets.Scripts.Core.Components.Projectile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Devic.Scripts.Utils.Pools
{
    public class ProjectilePool
    {
        // Collection checks will throw errors if we try to release an item that is already in the pool.
        private Projectile _projectile;
        public bool collectionChecks = true;
        public int maxPoolSize = 20;

        private IObjectPool<Projectile> m_Pool;

        public ProjectilePool(Projectile projectile)
        {
            _projectile = projectile;
            m_Pool = new ObjectPool<Projectile>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
        }

        Projectile CreatePooledItem()
        {
            var instance = MonoBehaviour.Instantiate(_projectile);
            return instance;
        }

        // Called when an item is returned to the pool using Release
        void OnReturnedToPool(Projectile projectil)
        {
            projectil.gameObject.SetActive(false);
        }

        // Called when an item is taken from the pool using Get
        void OnTakeFromPool(Projectile projectil)
        {
            projectil.gameObject.SetActive(true);
        }

        // If the pool capacity is reached then any items returned will be destroyed.
        // We can control what the destroy behavior does, here we destroy the GameObject.
        void OnDestroyPoolObject(Projectile projectil)
        {
            MonoBehaviour.Destroy(projectil);
        }

        public Projectile GetProjectileFromPool()
        {
            var projectile = m_Pool.Get();
            return projectile;
        }
        public List<Projectile> GetProjectileFromPool(int qtd)
        {
            List<Projectile> list = new();
            for (int i = 0; i < qtd; i++)
            {
                var projectile = m_Pool.Get();
                list.Add(projectile);
            }
            return list;
        }

        public void BackToPool(Projectile projectileToBack)
        {
            m_Pool.Release(projectileToBack);
        }

    }
}