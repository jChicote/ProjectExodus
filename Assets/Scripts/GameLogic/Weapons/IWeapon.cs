using ProjectExodus.Domain.Models;

namespace ProjectExodus.GameLogic.Weapons
{

    public interface IWeapon
    {

        #region - - - - - - Methods - - - - - -

        void InitializeWeapon(WeaponModel weaponModel);

        int GetWeaponID();

        void ToggleWeaponFire(bool isFiring);

        #endregion Methods

    }

}