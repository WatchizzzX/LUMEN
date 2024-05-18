using System.Collections.Generic;
using System.Linq;
using UnityEditor.Graphs;
using UnityEngine;

namespace Utils
{
    public static class RaycastHelpers
    {
        public static void VisualizeRays(IEnumerable<Ray> rays, Color color, float distance)
        {
            foreach (var ray in rays)
            {
                VisualizeRay(ray, color, distance);
            }
        }

        public static void VisualizeRay(Ray ray, Color color, float distance)
        {
            Debug.DrawRay(ray.origin, ray.direction, color, distance, true);
        }

        public static bool CheckAnyRays(IEnumerable<Ray> rays, float distance, LayerMask layerMask)
        {
            return rays.Any(ray => Physics.Raycast(ray, distance, layerMask));
        }

        public static bool CheckAllRays(IEnumerable<Ray> rays, float distance, LayerMask layerMask)
        {
            return rays.Any(ray => Physics.Raycast(ray, distance, layerMask));
        }

        public static bool TryGetFirstRaycastHit(IEnumerable<Ray> rays, float distance, LayerMask layerMask,
            out RaycastHit hit)
        {
            foreach (var ray in rays)
            {
                if (TryGetRaycastHit(ray, distance, layerMask, out hit))
                    return true;
            }

            hit = new RaycastHit();
            return false;
        }

        public static bool TryGetRaycastHit(Ray ray, float distance, LayerMask layerMask, out RaycastHit hit)
        {
            return Physics.Raycast(ray.origin, ray.direction, out hit, distance, layerMask);
        }
    }
}