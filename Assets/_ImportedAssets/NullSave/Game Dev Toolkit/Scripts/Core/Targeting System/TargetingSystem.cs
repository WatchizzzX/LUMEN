﻿//
// Game Developers Toolkit © 2023 by Thomas W Holtquist is licensed under CC BY-SA 4.0 
// https://www.nullsave.com
//

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NullSave.GDTK
{
    [AutoDoc("This component allows you to target one or more other GameObjets with a LockOnTarget component attached.")]
    [AutoDocLocation("targeting-systems")]
    [DefaultExecutionOrder(-700)]
    public class TargetingSystem : MonoBehaviour
    {

        #region Enumerations

        public enum TagMode
        {
            None = 0,
            RequireTags = 1,
            IgnoreTags = 2,
        }

        #endregion

        #region Structures

        private struct TargetEval
        {
            public float distance;
            public LockOnTarget target;

            public TargetEval(float distance, LockOnTarget target)
            {
                this.distance = distance;
                this.target = target;
            }

        }

        #endregion

        #region Fields

        [Tooltip("Activate 2D lock-on mode")] public bool is2DMode;
        [Tooltip("Automatically lock-on to target(s) in range")] public bool autoLockOn;
        [Tooltip("Lock on to targets in range with button press")] public bool withButton;
        [Tooltip("Button to use for locking on")] public string lockOnButton;
        [Tooltip("Lock on to targets in range with key press")] public bool withKey;
        [Tooltip("Key to use for locking on")] public KeyCode lockOnKey;
        [Tooltip("Layers to include when checking for targets")] public LayerMask layerMask;
        [Tooltip("Method used for selecting based on tags")] public TagMode tagMode;
        [Tooltip("List of tags used for filtering targets")] public List<string> tagFilter;
        [Tooltip("Radius to check for targets")] public float lockRadius;

        [Tooltip("Lock on to targets in range with button press")] public bool removeWithButton;
        [Tooltip("Button to use for locking on")] public string removeLockOnButton;
        [Tooltip("Lock on to targets in range with key press")] public bool removeWithKey;
        [Tooltip("Key to use for locking on")] public KeyCode removeLockOnKey;


        [Tooltip("Require unobstructed view of target")] public bool requireLineOfSight;
        [Tooltip("Layers to include when checking for obstructions")] public LayerMask obstructionLayer;
        [Tooltip("Offset from transform position when looking for target")] public Vector3 losOffset;

        [Tooltip("Prefab to instance on targets that can be locked on but are not")] public GameObject availableTargetPrefab;
        [Tooltip("Prefab to instance on targets that are actively locked on")] public GameObject lockedTargetPrefab;

        [Tooltip("Event raised when available target list changes")] public SimpleEvent onAvailableTargetsChanged;
        [Tooltip("Event raised when locked on target list changes")] public SimpleEvent onLockedTargetsChanged;

        [SerializeField] private bool m_showIndicators;
        private List<LockOnTarget> m_optionalTargets;
        private List<LockOnTarget> m_lockedTargets;

        #endregion

        #region Properties

        [AutoDoc("List of all available targets")]
        public IReadOnlyList<LockOnTarget> allTargets
        {
            get
            {
                if (m_lockedTargets == null) m_lockedTargets = new List<LockOnTarget>();
                if (m_optionalTargets == null) m_optionalTargets = new List<LockOnTarget>();
                List<LockOnTarget> result = new List<LockOnTarget>();
                result.AddRange(m_lockedTargets);
                result.AddRange(m_optionalTargets);
                return result;
            }
        }

        [AutoDoc("Gets the closets available lock on target")]
        public LockOnTarget bestLockOption { get; private set; }

        [AutoDoc("List of all locked on targets")]
        public IReadOnlyList<LockOnTarget> lockedTargets
        {
            get
            {
                if (m_lockedTargets == null) m_lockedTargets = new List<LockOnTarget>();
                return m_lockedTargets;
            }
        }

        [AutoDoc("List on all non-locked targets")]
        public IReadOnlyList<LockOnTarget> optionalTargets
        {
            get
            {
                if (m_optionalTargets == null) m_optionalTargets = new List<LockOnTarget>();
                return m_optionalTargets;
            }
        }

        [AutoDoc("Show lock-on indicators")]
        public bool showIndicators
        {
            get { return m_showIndicators; }
            set
            {
                if (value == m_showIndicators) return;
                m_showIndicators = value;
                if (!value)
                {
                    HideIndicators();
                }
                else
                {
                    ShowIndicators();
                }
            }
        }

        #endregion

        #region Unity Methods

        private void OnDrawGizmosSelected()
        {
            if (requireLineOfSight)
            {
                Gizmos.color = Color.cyan;
                Vector3 start = transform.position + losOffset;
                Gizmos.DrawLine(start, (transform.forward * 2) + start);
            }
        }

        private void Update()
        {
            List<TargetEval> targets = new List<TargetEval>();
            LockOnTarget target;

            if (removeWithButton && InterfaceManager.Input.GetButtonDown(removeLockOnButton))
            {
                RemoveAllLockOns();
            }
            else if (removeWithKey && InterfaceManager.Input.GetKeyDown(removeLockOnKey))
            {
                RemoveAllLockOns();
            }

            // Remove out of range targets
            foreach (LockOnTarget lt in lockedTargets.ToList())
            {
                if (lt == null)
                {
                    RemoveLockedTarget(lt);
                }
                else
                {
                    if (lt.transform == null || Mathf.Abs(Vector3.Distance(transform.position, lt.transform.position)) > lockRadius)
                    {
                        RemoveLockedTarget(lt);
                    }
                    else if (requireLineOfSight && !HasLineOfSight(lt.transform))
                    {
                        RemoveLockedTarget(lt);
                    }
                }
            }

            // Remove out of range optional targets
            foreach (LockOnTarget lot in optionalTargets.ToList())
            {
                if (lot == null)
                {
                    RemoveAvailableTarget(lot);
                }
                else
                {
                    if (lot.transform == null || Mathf.Abs(Vector3.Distance(transform.position, lot.transform.position)) > lockRadius)
                    {
                        lot.UpdateIndicator(null, false);
                        RemoveAvailableTarget(lot);
                    }
                    else if (requireLineOfSight && !HasLineOfSight(lot.transform))
                    {
                        lot.UpdateIndicator(null, false);
                        RemoveAvailableTarget(lot);
                    }
                }
            }

            // Process new targets
            if (is2DMode)
            {
                TargetEval eval;
                RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, lockRadius, transform.forward, 0.1f, layerMask);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.transform != transform && FitsTagFilter(hit.transform.gameObject))
                    {
                        target = hit.transform.gameObject.GetComponentInChildren<LockOnTarget>();
                        if (target != null)
                        {
                            if (!requireLineOfSight || HasLineOfSight(hit.transform))
                            {
                                eval = new TargetEval(Mathf.Abs(Vector3.Distance(transform.position, hit.transform.position)), target);
                                if (eval.distance <= lockRadius)
                                {
                                    targets.Add(eval);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                TargetEval eval;
                RaycastHit[] hits = Physics.SphereCastAll(transform.position, lockRadius, transform.forward, 0.1f, layerMask);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform != transform && FitsTagFilter(hit.transform.gameObject))
                    {
                        target = hit.transform.gameObject.GetComponentInChildren<LockOnTarget>();
                        if (target != null)
                        {
                            if (!requireLineOfSight || HasLineOfSight(hit.transform))
                            {
                                eval = new TargetEval(Mathf.Abs(Vector3.Distance(transform.position, hit.transform.position)), target);
                                if (eval.distance <= lockRadius)
                                {
                                    targets.Add(eval);
                                }
                            }
                        }
                    }
                }
            }

            // Sort if needed
            if (targets.Count > 1)
            {
                targets.OrderBy(x => x.distance);
            }

            // Add as needed
            foreach (TargetEval te in targets)
            {
                if (autoLockOn || GetManualLockRequest())
                {
                    AddLockedTarget(te.target);
                    RemoveAvailableTarget(te.target);
                }
                else
                {
                    if (m_lockedTargets.Contains(te.target))
                    {
                        te.target.UpdateIndicator(showIndicators ? lockedTargetPrefab : null, true);
                        RemoveAvailableTarget(te.target);
                    }
                    else
                    {
                        te.target.UpdateIndicator(showIndicators ? availableTargetPrefab : null, false);
                        AddAvailableTarget(te.target);
                    }
                }
            }

        }

        private void Reset()
        {
            lockOnButton = "Fire2";
            layerMask = 1;
            obstructionLayer = 1;
            lockRadius = 5;
            showIndicators = true;
        }

        #endregion

        #region Public Methods

        [AutoDoc("Gets the closest target")]
        public LockOnTarget GetClosestTarget()
        {
            List<TargetEval> targets = BuildEvalList();
            if (targets.Count == 0) return null;
            return targets[0].target;
        }

        [AutoDoc("Gets a list of closest targets")]
        [AutoDocParameter("Number of targets to return")]
        public List<LockOnTarget> GetClosestTargets(int count)
        {
            int remaining = count;
            List<LockOnTarget> result = new List<LockOnTarget>();
            List<TargetEval> targets = BuildEvalList();

            while (remaining > 0 && targets.Count > 0)
            {
                result.Add(targets[0].target);
                targets.RemoveAt(0);
                remaining -= 1;
            }

            return result;
        }

        [AutoDoc("Gets a list of locked targets")]
        [AutoDocParameter("Number of targets to return")]
        public List<LockOnTarget> GetLockedTargets(int count)
        {
            int remaining = count;
            List<LockOnTarget> result = new List<LockOnTarget>();
            List<TargetEval> evals = new List<TargetEval>();

            // Get locked targets
            foreach (LockOnTarget target in lockedTargets)
            {
                evals.Add(new TargetEval
                {
                    target = target,
                    distance = Vector3.Distance(transform.position, target.transform.position)
                });
            }
            evals = evals.OrderBy(x => x.distance).ToList();

            while (remaining > 0 && evals.Count > 0)
            {
                result.Add(evals[0].target);
                evals.RemoveAt(0);
                remaining -= 1;
            }

            return result;
        }

        [AutoDoc("Checks if there is a line of sight from system to target")]
        [AutoDocParameter("Target to check")]
        public bool HasLineOfSight(Transform target)
        {
            if (Physics.Raycast(transform.position + losOffset, (target.position - transform.position + losOffset), out RaycastHit hit, 10000, obstructionLayer))
            {
                return hit.transform == target;
            }

            return true;
        }

        [AutoDoc("Removes all active lock-ons")]
        public void RemoveAllLockOns()
        {
            foreach (LockOnTarget target in m_lockedTargets)
            {
                target.UpdateIndicator(null, false);
            }
            m_lockedTargets.Clear();
        }

        [AutoDoc("Toggles the lock-on mode of a target")]
        [AutoDocParameter("Target to toggle")]
        public void ToggleLock(LockOnTarget target)
        {
            if (optionalTargets.Contains(target))
            {
                RemoveAvailableTarget(target);
                AddLockedTarget(target);
                return;
            }

            if (lockedTargets.Contains(target))
            {
                RemoveLockedTarget(target);
                AddAvailableTarget(target);
            }
        }

        #endregion

        #region Private Methods

        private void AddAvailableTarget(LockOnTarget target)
        {
            if (m_optionalTargets.Contains(target)) return;
            m_optionalTargets.Add(target);
            onAvailableTargetsChanged?.Invoke();
        }

        private void AddLockedTarget(LockOnTarget target)
        {
            if (m_lockedTargets.Contains(target)) return;
            m_lockedTargets.Add(target);
            onLockedTargetsChanged?.Invoke();
        }

        private List<TargetEval> BuildEvalList()
        {
            List<TargetEval> result = new List<TargetEval>();

            foreach (LockOnTarget target in lockedTargets)
            {
                result.Add(new TargetEval
                {
                    target = target,
                    distance = Vector3.Distance(transform.position, target.transform.position)
                });
            }

            foreach (LockOnTarget target in optionalTargets)
            {
                result.Add(new TargetEval
                {
                    target = target,
                    distance = Vector3.Distance(transform.position, target.transform.position)
                });
            }

            return result.OrderBy(x => x.distance).ToList();
        }

        private bool FitsTagFilter(GameObject target)
        {
            switch(tagMode)
            {
                case TagMode.RequireTags:
                    return tagFilter.Contains(target.tag);
                case TagMode.IgnoreTags:
                    return !tagFilter.Contains(target.tag);
                default:
                    return true;
            }
        }

        private bool GetManualLockRequest()
        {
            if (withButton && InterfaceManager.Input.GetButtonDown(lockOnButton)) return true;
            if (withKey && InterfaceManager.Input.GetKeyDown(lockOnKey)) return true;
            return false;
        }

        private void HideIndicators()
        {
            if (!Application.isPlaying) return;

            foreach (LockOnTarget target in m_lockedTargets)
            {
                target.UpdateIndicator(null, true);
            }

            foreach (LockOnTarget target in m_optionalTargets)
            {
                target.UpdateIndicator(null, false);
            }
        }

        private void RemoveAvailableTarget(LockOnTarget target)
        {
            if (!m_optionalTargets.Contains(target)) return;
            m_optionalTargets.Remove(target);
            onAvailableTargetsChanged?.Invoke();
        }

        private void RemoveLockedTarget(LockOnTarget target)
        {
            if (!m_lockedTargets.Contains(target)) return;
            m_lockedTargets.Remove(target);
            target.UpdateIndicator(null, false);
            onLockedTargetsChanged?.Invoke();
        }

        private void ShowIndicators()
        {
            if (m_lockedTargets != null)
            {
                foreach (LockOnTarget target in m_lockedTargets)
                {
                    target.UpdateIndicator(lockedTargetPrefab, true);
                }
            }

            if (m_optionalTargets != null)
            {
                foreach (LockOnTarget target in m_optionalTargets)
                {
                    target.UpdateIndicator(availableTargetPrefab, false);
                }
            }
        }

        #endregion

    }
}