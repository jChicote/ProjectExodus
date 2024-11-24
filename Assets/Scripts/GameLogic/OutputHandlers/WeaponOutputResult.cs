using System;
using System.Collections.Generic;
using ProjectExodus.Backend.UseCases.WeaponUseCases.CreateWeapon;
using ProjectExodus.Backend.UseCases.WeaponUseCases.GetWeapons;
using ProjectExodus.Backend.UseCases.WeaponUseCases.UpdateWeapon;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.GameLogic.OutputHandlers
{

    public class CreateWeaponOutputResult: ICreateWeaponOutputPort
    {

        #region - - - - - - Properties - - - - - -

        public WeaponModel Result { get; set; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        void ICreateWeaponOutputPort.PresentCreatedWeapon(WeaponModel weapon)
            => this.Result = weapon;

        #endregion Methods

    }
    
    public class GetWeaponOutputResult : IGetWeaponOutputPort
    {

        #region - - - - - - Properties - - - - - -

        public IEnumerable<WeaponModel> Result { get; set; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        void IGetWeaponOutputPort.PresentWeapons(IEnumerable<WeaponModel> weapons)
            => this.Result = weapons;

        #endregion Methods

    }

    public class UpdateWeaponOutputResult : IUpdateWeaponOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void IUpdateWeaponOutputPort.PresentSuccessfulUpdate()
        {
        }

        #endregion Methods
  
    }

}