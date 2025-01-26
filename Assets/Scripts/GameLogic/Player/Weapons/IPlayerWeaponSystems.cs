using System.Collections.Generic;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Infrastructure.Providers;

namespace ProjectExodus.GameLogic.Player.Weapons
{

    public interface IPlayerWeaponSystems
    {

        #region - - - - - - Methods - - - - - -

        void InitialiseWeaponSystems(
            IWeaponAssetProvider weaponAssetProvider, 
            List<WeaponModel> weapons);

        void ToggleWeaponFire(bool isFiring);

        #endregion Methods

    }

}