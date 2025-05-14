 using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectExodus.ScriptableObjects
{

    [CreateAssetMenu(fileName = "UserInterfaceSettings", menuName = "ScriptableObjects/UserInterfaceSettings", order = 0)]
    public class UserInterfaceSettings : ScriptableObject
    {

        #region - - - - - - Fields - - - - - -

        [Header("Image Sources")]
        public List<ProfileImageIDPair> ProfileImages;

        [Header("Player / Weapon Targeting")] 
        public Color DefaultColor;
        public Color EnemyColor;
        public Color InteractableColor;
        public Color InvalidColor;

        [Header("Tractor Beam Targeting")] 
        public Color WeakBeamStrengthColor;
        public Color FullBeamStregnthColor;

        [Header("Pickup / Collectables")] 
        public List<PickupUserInterfaceAsset> PickupAssets;

        #endregion Fields

    }

    [Serializable]
    public class ProfileImageIDPair
    {

        #region - - - - - - Fields - - - - - -

        public int ID;
        public Sprite Image;

        #endregion Fields

    }

}