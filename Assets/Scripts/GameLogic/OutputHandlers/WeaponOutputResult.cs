using ProjectExodus.Backend.UseCases.WeaponUseCases.CreateWeapon;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.GameLogic.OutputHandlers
{

    public class CreateWeaponOutputResult: ICreateWeaponOutputPort
    {

        #region - - - - - - Properties - - - - - -

        public WeaponModel Result { get; set; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        public void PresentCreatedWeapon(WeaponModel weapon)
            => this.Result = weapon;

        #endregion Methods

    }

}