using ProjectExodus.Domain.Models;

namespace ProjectExodus.GameLogic.Weapons
{

    public interface IWeapon
    {

        #region - - - - - - Properties - - - - - -

        WeaponType Type { get; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        void InitializeWeapon(WeaponModel weaponModel);

        int GetWeaponID();

        void ToggleWeaponFire(bool isFiring);

        #endregion Methods

    }

}