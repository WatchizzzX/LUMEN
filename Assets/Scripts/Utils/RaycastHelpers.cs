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

        public static bool ExecuteAllRays(IEnumerable<Ray> rays, float distance, LayerMask layerMask)
        {
            return rays.Any(ray => Physics.Raycast(ray, distance, layerMask));
        }
    }
}