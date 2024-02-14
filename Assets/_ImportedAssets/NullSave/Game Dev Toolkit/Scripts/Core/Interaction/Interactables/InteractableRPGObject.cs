using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullSave.GDTK
{
    [AutoDocLocation("interaction/interactables")]
    [AutoDoc("This Interactable component enables an object to use up to 4 custom actions at a time.")]
    public class InteractableRPGObject : InteractableObject
    {

        #region Fields

        private RPGAction[] actions;
        private Action[] callbacks;
        private object[] parameters;
        private bool busy;
        private Interactor curAgent;

        #endregion

        #region Properties

        [AutoDoc("List of callbacks to be used by the Interactor UI")]
        public override Action[] Callbacks { get => callbacks; }

        [AutoDoc("Currently active Interactor")]
        public override Interactor CurrentAgent
        {
            get { return curAgent; }
            set
            {
                if (curAgent == value) return;
                curAgent = value;
                if(value != null)
                {
                    foreach(RPGAction action in actions)
                    {
                        action.OnInteractionCalled();
                    }
                    RebuildCallbacksAndParameters();
                }    
            }
        }

        [AutoDoc("Is the object currently interactable")]
        public override bool IsInteractable
        {
            get
            {
                if (busy) return false;
                return base.IsInteractable;
            }
            set => base.IsInteractable = value;
        }

        [AutoDoc("List of parameters to be used by the Interactor UI")]
        public override object[] Parameters { get => parameters; }

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            actions = GetComponents<RPGAction>();
            if (actions.Length == 0)
            {
                StringExtensions.LogWarning(this, "InteractableRPGObject", "No actions defined");
            }
            else if (actions.Length > 4)
            {
                StringExtensions.LogWarning(this, "InteractableRPGObject", "More than 4 actions defined, this may cause instabilities");
            }

            callbacks = new Action[actions.Length];
            parameters = new object[actions.Length];

            foreach(var action in actions)
            {
                action.onLockStateChanged += UpdateLockStatus;
            }
        }

        private void OnDisable()
        {
            foreach (var action in actions)
            {
                action.onLockStateChanged -= UpdateLockStatus;
            }
        }

        public override void Reset()
        {
            base.Reset();
            customUI = Resources.Load<InteractorUI>("Interaction/Multi Action Interactor UI");
        }

        #endregion

        #region Private Methods

        private void RebuildCallbacksAndParameters()
        {
            List<RPGAction> forbiddenActions = new List<RPGAction>();

            foreach(var action in actions)
            {
                if (action.preventActions != null) forbiddenActions.AddRange(action.preventActions);
            }

            int index = 0;
            foreach (var action in actions)
            {
                if (forbiddenActions.Contains(action))
                {
                    parameters[index] = null;
                    callbacks[index++] = null;
                }
                else
                {
                    parameters[index] = action.parameter;
                    callbacks[index++] = () =>
                    {
                        action.callback?.Invoke();
                        RebuildCallbacksAndParameters();
                    };
                }
            }

            CurrentAgent?.activeUI?.Initialize(Parameters, Callbacks);
        }

        private void UpdateLockStatus()
        {
            bool locked = false;
            foreach (var item in actions)
            {
                if(item.lockInteractions)
                {
                    locked = true;
                    break;
                }
            }

            busy = locked;
        }

        #endregion

    }
}
