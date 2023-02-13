using System.Collections;
using UnityEngine;
using Assets.Scripts.Core.Components;
namespace Assets.Scripts.Core.Components._2DComponents
{
    public class SpriteChangerByHealth : SpriteChanger
    {
        [SerializeField] private Health _healthComponent;
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {
        }
        private void SwitchSpriteByHealthPercentage()
        {
            var healthPercentage = _healthComponent.GetHealthPercentage();

            switch (healthPercentage)
            {
                case healthPercentage < 75f:

                default:
                    break;
            }

        }
    }
}