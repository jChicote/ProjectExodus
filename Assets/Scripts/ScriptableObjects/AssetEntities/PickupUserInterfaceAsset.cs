using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class PickupUserInterfaceAsset
{
    
    #region - - - - - - Fields - - - - - -

    public PickupEnum PickupEnum;

    [FormerlySerializedAs("Spite")] public Sprite Sprite;

    #endregion Fields
    
}
