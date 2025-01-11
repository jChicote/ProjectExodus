using ProjectExodus.Domain.Models;

namespace ProjectExodus.GameLogic.Weapons
{

    public interface IWeapon
    {

        #region - - - - - - Methods - - - - - -

        void InitializeWeapon(WeaponModel weaponModel);

        void ToggleWeaponFire(bool isFiring);

        #endregion Methods

    }

}