using ProjectExodus.Domain.Models;

namespace ProjectExodus.Backend.UseCases.WeaponUseCases.CreateWeapon
{

    public interface ICreateWeaponOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentCreatedWeapon(WeaponModel weapon);

        #endregion Methods

    }

}