﻿//
// Game Developers Toolkit © 2023 by Thomas W Holtquist is licensed under CC BY-SA 4.0 
// https://www.nullsave.com
//

using System.Collections.Generic;
using UnityEngine;

namespace NullSave.GDTK
{
    [AutoDoc("This component will draw objects with the ObjectPullTarget component towards itself.")]
    [AutoDocLocation("object-pull")]
    public class ObjectPullSource2D : MonoBehaviour
    {

        #region Fields

        [Tooltip("Layermask for setting included pull layers")] public LayerMask affectedLayers;
        [Tooltip("Seconds to way before beginning pull")] public float delayBeforePull;
        [Tooltip("Radius around object to pull from")] public float pullRadius;
        [Tooltip("Seconds from start of pull until object reaches target")] public float pullDuration;
        [Tooltip("Offset to apply to end position")] public Vector3 pullToOffset;
        [Tooltip("Destroy pulled target after pull complete")] public bool destroyAfterPull;

        private List<ObjectPullTarget> targets;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            targets = new List<ObjectPullTarget>();
        }

        private void FixedUpdate()
        {
            // Update hits
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, pullRadius, transform.up, affectedLayers);
            foreach (RaycastHit2D hit in hits)
            {
                ObjectPullTarget opt = hit.transform.gameObject.GetComponent<ObjectPullTarget>();
                if (opt != null && !targets.Contains(opt))
                {
                    opt.ElapsedDelay = 0;
                    opt.ElapsedPull = 0;
                    targets.Add(opt);
                }
            }

            // Update pull
            foreach (ObjectPullTarget target in targets)
            {
                if (target.ElapsedDelay < delayBeforePull + target.additionalDelay)
                {
                    target.ElapsedDelay += Time.fixedDeltaTime;
                    if (target.ElapsedDelay >= delayBeforePull + target.additionalDelay)
                    {
                        target.StartPosition = target.transform.position;
                        target.ElapsedPull = delayBeforePull + target.additionalDelay - target.ElapsedDelay;
                    }
                }

                if (target.ElapsedDelay >= delayBeforePull + target.additionalDelay)
                {
                    target.ElapsedPull += Time.fixedDeltaTime;
                    target.transform.position = Vector3.Slerp(target.StartPosition, transform.position + pullToOffset, target.ElapsedPull / (pullDuration + target.additionalDuration));
                }

                if (target.ElapsedPull >= pullDuration + target.additionalDuration)
                {
                    if (destroyAfterPull)
                    {
                        InterfaceManager.ObjectManagement.DestroyObject(target.gameObject);
                    }
                }
            }
        }

        private void Reset()
        {
            affectedLayers = 1;
            pullRadius = 10;
            pullDuration = 1;
            delayBeforePull = 3;
        }

        #endregion

    }
}