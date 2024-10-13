using System.Collections.Generic;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.Backend.UseCases.WeaponUseCases.GetWeapons
{

    public interface IGetWeaponOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentWeapons(IEnumerable<WeaponModel> weapons);

        #endregion Methods

    }

}