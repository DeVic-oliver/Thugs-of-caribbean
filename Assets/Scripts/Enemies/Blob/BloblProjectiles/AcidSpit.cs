using Assets.Scripts.Core.Components.Projectile;
using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies.Blob.BloblProjectiles
{
    public class AcidSpit : Projectile
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
       new void Update()
        {
            base.Update();
        }

        new void OnTriggerEnter(Collider other)
        {
            PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>(); 
            if (player != null)
            {
                player.TakeDamage(Random.Range(2, 6));
            }
        }
    }
}