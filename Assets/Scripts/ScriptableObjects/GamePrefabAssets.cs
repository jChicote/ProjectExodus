using System.Collections.Generic;
using ProjectExodus.ScriptableObjects.AssetEntities;
using UnityEngine;

namespace ProjectExodus.ScriptableObjects
{

    [CreateAssetMenu(fileName = "GamePrefabAssets", menuName = "ScriptableObjects/GamePrefabAssets", order = 0)]
    public class GamePrefabAssets : ScriptableObject
    {

        #region - - - - - - Fields - - - - - -

        public List<WeaponAssetObject> WeaponPrefabs;

        #endregion Fields

    }

}