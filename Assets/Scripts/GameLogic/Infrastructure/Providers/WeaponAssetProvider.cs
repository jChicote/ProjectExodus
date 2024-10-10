using System;
using System.Linq;
using ProjectExodus.ScriptableObjects;
using ProjectExodus.ScriptableObjects.AssetEntities;

namespace ProjectExodus.GameLogic.Infrastructure.Providers
{

    public interface IWeaponAssetProvider
    {

        #region - - - - - - Methods - - - - - -

        WeaponAssetObject Provide(int id);

        #endregion Methods

    }

    public class WeaponAssetProvider : IWeaponAssetProvider
    {

        #region - - - - - - Fields - - - - - -

        private readonly GamePrefabAssets m_GamePrefabAssets;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public WeaponAssetProvider(GamePrefabAssets gamePrefabAssets)
            => this.m_GamePrefabAssets = gamePrefabAssets ?? throw new ArgumentNullException(nameof(gamePrefabAssets));

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        WeaponAssetObject IWeaponAssetProvider.Provide(int id)
            => this.m_GamePrefabAssets.WeaponPrefabs.Single(wap => wap.ID == id);

        #endregion Methods
  
    }

}