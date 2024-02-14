﻿//
// Game Developers Toolkit © 2023 by Thomas W Holtquist is licensed under CC BY-SA 4.0 
// https://www.nullsave.com
//

using UnityEngine;

namespace NullSave.GDTK
{
    [AutoDocLocation("ui")]
    [AutoDoc("A simple graphic control showing the location of 'North'.")]
    public class Compass : MonoBehaviour
    {

        #region Fields

        [Tooltip("Object 'holding' the compass")] public Transform compassHolder;
        [Tooltip("Object the represents the 'North Pole'")] public Transform northPole;
        [Tooltip("UI Element pointing to North")] public RectTransform northIndicator;

        private Quaternion MissionDirection;
        private Vector3 northDirection;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            if (compassHolder == null || northDirection == null || northPole == null)
            {
                StringExtensions.LogError(gameObject, "Compass", "All properties are required");
                enabled = false;
            }
        }

        private void Reset()
        {
            compassHolder = transform;
        }

        private void Update()
        {
            northDirection.z = compassHolder.eulerAngles.y;

            Vector3 dir =  northPole.position - compassHolder.position;

            MissionDirection = Quaternion.LookRotation(dir);
            MissionDirection.z = -MissionDirection.y;
            MissionDirection.x = MissionDirection.y = 0;

            northIndicator.localRotation = MissionDirection * Quaternion.Euler(northDirection);
        }

        #endregion

    }
}