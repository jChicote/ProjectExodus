using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectExodus
{

    [CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObjects/EnemySettings", order = 0)]
    public class EnemySettings : ScriptableObject
    {

        #region - - - - - - Fields - - - - - -

        [Header("Game Enemies")]
        public List<EnemyAssetObject> Enemies;
        
        [FormerlySerializedAs("m_SpawnerDifficulty")]
        [Header("Spawner Details")]
        public SpawnerDifficultyMultiplier SpawnerDifficulty;
        
        [Space]
        public SpawnerInfo PawnSpawnerInfo;
        public SpawnerInfo FighterSpawnerInfo;
        public SpawnerInfo DroneSpawnerInfo;
        public SpawnerInfo KnightSpawnerInfo;

        #endregion Fields

    }

}