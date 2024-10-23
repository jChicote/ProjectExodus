using System.Collections.Generic;
using ProjectExodus.Backend.UseCases.WeaponUseCases.CreateWeapon;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.DebugSupport.OutputHandlers
{

    public class DebugCreateWeapoOutputHandler : ICreateWeaponOutputPort
    {
        
        #region - - - - - - Fields - - - - - -

        public WeaponModel Result;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void PresentCreatedWeapon(WeaponModel weapon)
            => this.Result = weapon;

        #endregion Methods
  
    }

}