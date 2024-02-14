using System;

namespace NullSave.GDTK
{
    [AutoDocLocation("interaction/rpg-actions")]
    [AutoDoc("The RPG Action adds an empty action to an Interactable RPG Object. This can help ensure other actions are on the slot you desire.")]
    public class RPGEmptyAction : RPGAction
    {

        #region Properties

        [AutoDoc("Callback to invoke when using this action")]
        public override Action callback { get => null; }

        [AutoDoc("Parameter (usually string display text) to use with this action")]
        public override object parameter { get => null; }

        #endregion

    }

}