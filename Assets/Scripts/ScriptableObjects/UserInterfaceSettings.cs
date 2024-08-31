using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ProjectExodus.ScriptableObjects
{

    [CreateAssetMenu(fileName = "UserInterfaceSettings", menuName = "ScriptableObjects/UserInterfaceSettings", order = 0)]
    public class UserInterfaceSettings : ScriptableObject
    {

        #region - - - - - - Fields - - - - - -

        [Header("Image Sources")]
        [SerializeField] private List<ProfileImageIDPair> m_ProfileImages;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public Dictionary<int, Sprite> ProfileImages
        {
            get
            {
                Dictionary<int, Sprite> _DictProfileImages = new Dictionary<int, Sprite>();
                foreach (ProfileImageIDPair _Pair in this.m_ProfileImages)
                    _DictProfileImages[_Pair.Key] = _Pair.Image;

                return _DictProfileImages;
            }
        }

        #endregion Properties
  
    }

    [Serializable]
    public class ProfileImageIDPair
    {

        #region - - - - - - Fields - - - - - -

        public int Key;
        public Sprite Image;

        #endregion Fields

    }

}