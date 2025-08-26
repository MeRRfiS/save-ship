using UnityEngine;

namespace Assets.Project.Scripts.ActFeatures
{
    public static class ActableDropHelper
    {
        public static Vector3 FindDropPosition(Transform origin, float checkDistance)
        {
            Vector3[] directions =
            {
            origin.right,
            -origin.right,
            origin.up,
            -origin.up
        };

            foreach (var dir in directions)
            {
                if (!Physics2D.Raycast(origin.position, dir, checkDistance))
                    return origin.position + dir;
            }

            // fallback: залишаємо на місці
            return origin.position;
        }
    }
}
