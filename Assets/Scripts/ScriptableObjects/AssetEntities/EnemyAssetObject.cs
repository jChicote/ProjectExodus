using System;
using ProjectExodus.ScriptableObjects.AssetEntities;
using UnityEngine;

namespace ProjectExodus
{

    [Serializable]
    public class EnemyAssetObject : AssetObject
    {

        #region - - - - - - Fields - - - - - -

        public string Name;

        public GameObject SpawnTemplate;

        public float Health;

        public float AttackDamage;

        public float Speed;

        public float TrackingDistance;
        
        #endregion Fields

    }

}