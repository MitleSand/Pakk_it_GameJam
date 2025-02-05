#if UNITY_EDITOR
using Unity.Collections;
using UnityEditor;
using UnityEngine;
namespace  Shadowprofile.Attributes.HierarchyS
{
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}

}
#endif