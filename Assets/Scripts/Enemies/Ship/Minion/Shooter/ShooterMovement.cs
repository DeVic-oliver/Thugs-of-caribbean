namespace Assets.Scripts.Enemies.Ship.Minion.Shooter
{
    public class ShooterMovement : EnemyShipMovement
    {
        void Update()
        {
            if (_shouldPursueTarget)
                _shipRotation.LookToTargetSmoothly(GetObjectTransform(), GetTargetTransform());
        }
    }
}