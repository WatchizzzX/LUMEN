﻿//
// Game Developers Toolkit © 2023 by Thomas W Holtquist is licensed under CC BY-SA 4.0 
// https://www.nullsave.com
//

using System.Collections.Generic;
using UnityEngine;

namespace NullSave.GDTK
{
    [AutoDocLocation("extensions")]
    [AutoDoc("This class contains methods that extends raycasting.")]
    public class RaycastExtensions
    {

        #region Public Methods

        [AutoDoc("Perform a sphere based cone raycast and return all hits")]
        [AutoDocParameter("Origin")]
        [AutoDocParameter("Radius at end")]
        [AutoDocParameter("Direction of cone")]
        [AutoDocParameter("Distance/Length of cone")]
        [AutoDocParameter("Angle of cone")]
        [AutoDocParameter("Layermask")]
        public static RaycastHit[] SphereConeCastAll(Vector3 origin, float endRadius, Vector3 direction, float distance, float coneAngle, LayerMask layerMask)
        {
            List<RaycastHit> result = new List<RaycastHit>();
            float angle;

            // Get initial raycast results
            RaycastHit[] raycastHits = Physics.SphereCastAll(origin, endRadius, direction, distance + endRadius, layerMask);

            // Check results
            for(int i=0; i < raycastHits.Length; i++)
            {
                Vector3 hitPoint = raycastHits[i].point;
                if (hitPoint == Vector3.zero) hitPoint = raycastHits[i].transform.position;

                float hitDistance = Vector3.Distance(origin, hitPoint);
                if(hitDistance < distance + endRadius)
                {
                    // Get the progression of the march
                    float percent = Mathf.Clamp(hitDistance / distance, 0, 1);
                    if(percent == 1)
                    {
                        // Max distance no need to re-cast
                        angle = Vector3.Angle(direction, hitPoint - origin);
                        if (angle < coneAngle)
                        {
                            result.Add(raycastHits[i]);
                        }
                    }
                    else
                    {
                        // Re-cast at progression level to ensure hit & angle
                        if (SphereConeReCastAll(origin, endRadius * percent, direction, distance, raycastHits[i].collider, out angle))
                        {
                            if (angle < coneAngle)
                            {
                                result.Add(raycastHits[i]);
                            }
                        }
                    }
                }
            }

            // Return valid hits
            return result.ToArray();
        }

        #endregion

        #region Private Methods

        // Re-cast with a sphere appropriate to the cone progress
        private static bool SphereConeReCastAll(Vector3 origin, float radius, Vector3 direction, float maxDistance, Collider collider, out float angle)
        {
            angle = 0;
            RaycastHit[] sphereCastHits = Physics.SphereCastAll(origin, radius, direction, maxDistance);
            for (int i = 0; i < sphereCastHits.Length; i++)
            {
                if (sphereCastHits[i].collider == collider)
                {
                    Vector3 hitPoint = sphereCastHits[i].point;
                    if (hitPoint == Vector3.zero) hitPoint = sphereCastHits[i].transform.position;

                    angle = Vector3.Angle(direction, hitPoint - origin);
                    return true;
                }
            }

            return false;
        }

        #endregion

    }
}
