using Assets.Scripts.Core.Components;
using Assets.Scripts.Core.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : Health, IDamageable
    {
        public void TakeDamage(int damageValue)
        {
            DecreaseHealth(damageValue);
        }

        new void Start()
        {
            base.Start();
        }

        new void Update()
        {
            base.Update();
        }
    }
}