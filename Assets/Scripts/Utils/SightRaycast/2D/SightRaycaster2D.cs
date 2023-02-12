using UnityEngine;

namespace Assets.Scripts.Utils.SightRaycast._2D
{
    public static class SightRaycaster2D
    {
        public static bool CheckGameObjectOnSight(Vector2 fireOrigin, Collider2D collider, float maxRange, string targetLayerMask)
        {
            var origin = new Vector2(fireOrigin.x, fireOrigin.y + collider.bounds.extents.y);
            var targetMask = LayerMask.GetMask(targetLayerMask);
            bool raycast = Physics2D.Raycast(origin, Vector2.up, maxRange, targetMask);

            if (raycast)
            {
                return true;
            }
            return false;
        }
        public static bool CheckGameObjectOnSight(Vector2 fireOrigin, float originOffset, float maxRange, LayerMask targetLayerMask)
        {
            var origin = new Vector2(fireOrigin.x, fireOrigin.y + originOffset);
            bool raycast = Physics2D.Raycast(origin, Vector2.up, maxRange, targetLayerMask);

            if (raycast)
            {
                return true;
            }
            return false;
        }
    }
}