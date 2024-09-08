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