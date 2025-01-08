using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Provides error messages if a field through the Inspector is not provided a value if marked as 'RequiredField'.
/// </summary>
[CustomEditor(typeof(MonoBehaviour), true)]
public class RequiredFieldEditor : Editor
{
    
    #region - - - - - - Methods - - - - - -

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        object _TargetObject = target;
        FieldInfo[] fields = _TargetObject.GetType()
            .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var field in fields)
        {
            RequiredFieldAttribute _RequiredAttribute = (RequiredFieldAttribute)Attribute
                .GetCustomAttribute(field, typeof(RequiredFieldAttribute));
            if (_RequiredAttribute != null)
            {
                object _Value = field.GetValue(_TargetObject);
                if (_Value == null || (_Value is object unityObject && unityObject == null))
                    EditorGUILayout.HelpBox($"{field.Name} is required but not assigned.", MessageType.Error);
            }
        }
    }

    #endregion Methods

}
