using System.Collections.Generic;
using UnityEngine;

namespace ProjectExodus
{

    public class EnemySettings : ScriptableObject
    {

        #region - - - - - - Fields - - - - - -

        [Header("Game Enemies")]
        public List<EnemyAssetObject> Enemies;

        #endregion Fields

    }

}