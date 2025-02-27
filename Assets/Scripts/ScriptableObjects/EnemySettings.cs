using System.Collections.Generic;
using UnityEngine;

namespace ProjectExodus
{

    [CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObjects/EnemySettings", order = 0)]
    public class EnemySettings : ScriptableObject
    {

        #region - - - - - - Fields - - - - - -

        [Header("Game Enemies")]
        public List<EnemyAssetObject> Enemies;

        #endregion Fields

    }

}