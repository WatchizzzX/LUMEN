using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NullSave.GDTK
{
    [CustomPropertyDrawer(typeof(ImageInfo), true)]
    public class ImageInfoDrawer : GDTKPropertyDrawer
    {

        #region Unity Methods

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int lines = 1;
            if (property.FindPropertyRelative("z_imageError").boolValue) lines++;
            return (EditorGUIUtility.singleLineHeight + 2) * lines;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (fieldInfo.GetCustomAttributes(typeof(TooltipAttribute), true).FirstOrDefault() is TooltipAttribute tt) label.tooltip = tt.tooltip;

            Rect rect = position;
            rect.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.BeginProperty(position, label, property);


            EditorGUI.BeginChangeCheck();
            SimpleProperty(rect, property, "sprite");
            rect.y += EditorGUIUtility.singleLineHeight + 2;
            if (EditorGUI.EndChangeCheck())
            {
                GDTKEditor.SetImageInfoSprite(property);
            }

            if (SimpleBool(property, "z_imageError"))
            {
                EditorGUI.LabelField(rect, "Must be in Resources or an Asset Bundle", GDTKEditor.Styles.ErrorTextStyle);
                rect.y += EditorGUIUtility.singleLineHeight + 2;
            }

            EditorGUI.EndProperty();
        }


        #endregion

    }
}