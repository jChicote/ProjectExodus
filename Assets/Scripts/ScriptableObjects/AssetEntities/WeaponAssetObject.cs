using System;
using UnityEngine;

namespace ProjectExodus.ScriptableObjects.AssetEntities
{

    [Serializable]
    public class WeaponAssetObject : AssetObject
    {

        #region - - - - - - Fields - - - - - -

        public string Name;

        public GameObject Asset;

        public Sprite WeaponSprite;

        #endregion Fields

    }

}