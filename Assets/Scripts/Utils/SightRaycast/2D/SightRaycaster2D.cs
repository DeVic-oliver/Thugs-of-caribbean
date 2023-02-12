using UnityEngine;
using UnityEngine.InputSystem.HID;

namespace Assets.Scripts.Utils.SightRaycast._2D
{
    public static class SightRaycaster2D
    {
        public static bool CheckGameObjectOnSight(Transform origin, float rangeDetection, string targetLayerMask)
        {
            var originVector = new Vector2(origin.position.x, origin.position.y);
            var targetMask = LayerMask.GetMask(targetLayerMask);
            RaycastHit2D hit2D = Physics2D.Raycast(originVector, origin.up, rangeDetection, targetMask);

            if (hit2D.collider != null)
            {
                return true;
            }
            return false;
        }
        public static bool CheckGameObjectOnSight(Transform origin, float rangeDetection, LayerMask targetLayerMask)
        {
            var originVector = new Vector2(origin.position.x, origin.position.y);
            RaycastHit2D hit2D = Physics2D.Raycast(originVector, Vector2.up, rangeDetection, targetLayerMask);

            if (hit2D.collider != null)
            {
                return true;
            }
            return false;
        }
    }
}