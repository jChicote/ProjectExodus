using System;
using UnityEngine;

namespace ProjectExodus.ScriptableObjects.AssetEntities
{

    [Serializable]
    public class ShipAssetObject : AssetObject
    {

        #region - - - - - - Fields - - - - - -

        public string Name;

        public GameObject Asset;

        public Sprite ShipSprite;

        public float BaseShieldHealth;

        public float BasePlatingHealth;

        #endregion Fields

    }

}