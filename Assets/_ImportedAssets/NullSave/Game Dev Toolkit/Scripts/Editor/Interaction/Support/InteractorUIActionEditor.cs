using UnityEditor;
using static NullSave.GDTK.InteractorLabel;

namespace NullSave.GDTK
{
    [CustomEditor(typeof(InteractorUIAction))]
    public class InteractorUIActionEditor : GDTKEditor
    {

        #region Unity Methods

        public override void OnInspectorGUI()
        {
            MainContainerBegin();

            SectionHeader("Behavior", GetIcon("icons/behavior"));
            SimpleProperty("interactionType");
            switch ((NavigationTypeSimple)SimpleValue<int>("interactionType"))
            {
                case NavigationTypeSimple.ByKey:
                    SimpleProperty("interactionKey");
                    break;
                case NavigationTypeSimple.ByButton:
                    SimpleProperty("interactionButton");
                    break;
            }
            SimpleProperty("actionIndex");
            SimpleProperty("callbackIndex");

            SectionHeader("UI", GetIcon("icons/ui"));
            SimpleProperty("label");
            SimpleProperty("format");
            SimpleProperty("textSource");
            if ((TextSource)SimpleValue<int>("textSource") == TextSource.Args)
            {
                SimpleProperty("argIndex");
            }

            SectionHeader("Events", GetIcon("icons/event"));
            SimpleProperty("onAssociatedAction");
            SimpleProperty("onNoAssociatedAction");


            MainContainerEnd();
        }

        #endregion

    }
}