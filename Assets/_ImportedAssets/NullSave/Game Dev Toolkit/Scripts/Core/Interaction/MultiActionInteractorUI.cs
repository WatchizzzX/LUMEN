using System;
using System.Collections.Generic;
using UnityEngine;

namespace NullSave.GDTK
{
    [AutoDocLocation("interaction/ui")]
    [AutoDoc("This component allows you to respond to an action on a single Interactable")]
    public class MultiActionInteractorUI : InteractorUI
    {

        #region Fields

        [Tooltip("List of UI elements used to respond to actions")] public List<InteractorUIAction> actions;

        private string m_text;
        bool itemWasSet;

        #endregion

        #region Properties

        [AutoDoc("Gets callbacks associated with the action")]
        public Action[] callbacks { get; private set; }

        [AutoDoc("Get if this UI is overriding the default interaction input")]
        public override bool overrideInteraction { get => true; }

        [AutoDoc("Gets parameters associated with the action")]
        public object[] parameters { get; private set; }

        #endregion

        #region Unity Methods

        public override void OnEnable()
        {
            base.OnEnable();

            foreach (var action in actions)
            {
                action.parent = this;
            }
        }

        #endregion

        #region Public Methods

        [AutoDocParameter("Parameters to pass")]
        [AutoDocParameter("Callbacks to pass")]
        public override void Initialize(object[] args, Action[] callbacks)
        {
            itemWasSet = false;

            parameters = args;
            this.callbacks = callbacks;

            foreach (var action in actions)
            {
                if (args != null && action.textSource == InteractorUIAction.TextSource.Args && action.argIndex < args.Length && args[action.argIndex] != null)
                {
                    action.label.text = action.format.Replace("{0}", (string)args[action.argIndex]);
                    itemWasSet = true;
                }
                else
                {
                    action.actionIndex = 0;
                }

                if (args == null || action.argIndex >= args.Length || args[action.argIndex] == null)
                {
                    action.onNoAssociatedAction?.Invoke();
                    action.label.text = action.format.Replace("{0}", string.Empty);
                }
                else
                {
                    action.onAssociatedAction?.Invoke();
                }
            }

            FallbackText();
        }

        [AutoDoc("Set the text for the UI")]
        [AutoDocParameter("Text value")]
        public override void SetText(string value)
        {
            m_text = value;
            FallbackText();
        }

        [AutoDoc("Perform interaction using an Interactor UI Action")]
        [AutoDocParameter("Action to use")]
        public override void UseAction(InteractorUIAction action)
        {
            if (!itemWasSet)
            {
                InteractWithTarget();
                return;
            }

            if (callbacks != null && action.callbackIndex < callbacks.Length)
            {
                callbacks[action.callbackIndex]?.Invoke();
                return;
            }

            InteractWithTarget();
        }

        #endregion

        #region Private Methods

        private void FallbackText()
        {
            if (!itemWasSet)
            {
                if (actions.Count > 0)
                {
                    actions[0].label.text = actions[0].format.Replace("{0}", m_text);
                    actions[0].onAssociatedAction?.Invoke();
                }
            }
        }

        #endregion

    }
}