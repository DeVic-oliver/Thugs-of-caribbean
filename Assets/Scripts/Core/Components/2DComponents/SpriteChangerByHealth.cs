namespace Assets.Scripts.Core.Components._2DComponents
{
    using UnityEngine;

    public enum HealthStates
    {
        HEALTHY,
        DAMAGED,
        CRITICAL,
        DESTROYED
    }

    public class SpriteChangerByHealth : SpriteChanger<HealthStates>
    {
        [Space(10)]
        [SerializeField] private Health _healthComponent;


        protected override void Start()
        {
            base.Start();
        }

        private void LateUpdate()
        {
            SwitchSpriteByHealthPercentage();
        }

        private void SwitchSpriteByHealthPercentage()
        {
            var healthPercentage = _healthComponent.GetHealthPercentage();

            if(healthPercentage >= 50f && healthPercentage <= 80f) 
            {
                ChangeCurrentSpriteTo(HealthStates.DAMAGED);

            }else if (healthPercentage >= 1f && healthPercentage <= 49f)
            {
                ChangeCurrentSpriteTo(HealthStates.CRITICAL);

            }else if(healthPercentage <= 0f)
            {
                ChangeCurrentSpriteTo(HealthStates.DESTROYED);
            }
            else
            {
                ChangeCurrentSpriteTo(HealthStates.HEALTHY);
            }

        }
    }
}