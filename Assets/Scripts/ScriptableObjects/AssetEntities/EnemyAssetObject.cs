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

        [Header("Templates")]
        public GameObject SpawnTemplate;
        public GameObject DeathEffect;

        [Header("Enemy Metadata")]
        public float Health;
        public float Lifetime;
        public float Speed;
        public int MaxCollisionHitCount;
        public float TurnSpeed;

        [Header("Attack Defaults")]
        public float AttackDamage;
        public float TrackingDistance;

        #endregion Fields

    }

}