using UnityEngine;

/// <summary>
/// Responsible for marking fields or properties that are 'required' for the component to run.
/// </summary>
/// <remarks>This is an aid for the Editor's Inspector to highlight fields that need to be set.</remarks>
[System.AttributeUsage(System.AttributeTargets.Field)]
public class RequiredFieldAttribute : PropertyAttribute
{
}