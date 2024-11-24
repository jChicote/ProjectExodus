using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.ScriptableObjects;
using ProjectExodus.ScriptableObjects.AssetEntities;
using UnityEngine;

namespace ProjectExodus.GameLogic.Infrastructure.Providers
{

    public interface IShipAssetProvider
    {

        #region - - - - - - Methods - - - - - -

        ShipAssetObject Provide(int id);

        List<ShipAssetObject> GetAllShips();

        #endregion Methods

    }
    
    public class ShipAssetProvider : IShipAssetProvider
    {

        #region - - - - - - Fields - - - - - -

        private readonly GamePrefabAssets m_GamePrefabAssets;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public ShipAssetProvider(GamePrefabAssets gamePrefabAssets)
            => this.m_GamePrefabAssets = gamePrefabAssets ?? throw new ArgumentNullException(nameof(gamePrefabAssets));

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        ShipAssetObject IShipAssetProvider.Provide(int id)
        {
            if (this.m_GamePrefabAssets.ShipPrefabs.Count == 0)
            {
                Debug.LogWarning("[warning]: No ship assets are found. Ensure a list of ships are added.");
                return null;
            }
            
            return this.m_GamePrefabAssets.ShipPrefabs.Single(sao => sao.ID == id);
        }

        List<ShipAssetObject> IShipAssetProvider.GetAllShips()
            => this.m_GamePrefabAssets.ShipPrefabs;

        #endregion Methods

    }

}