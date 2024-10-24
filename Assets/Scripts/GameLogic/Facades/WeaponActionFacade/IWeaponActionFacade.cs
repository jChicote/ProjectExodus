using ProjectExodus.Backend.UseCases.WeaponUseCases.CreateWeapon;
using ProjectExodus.Backend.UseCases.WeaponUseCases.GetWeapons;

namespace ProjectExodus.GameLogic.Facades.WeaponActionFacade
{

    public interface IWeaponActionFacade
    {

        #region - - - - - - Methods - - - - - -

        void CreateWeapon(CreateWeaponInputPort inputPort, ICreateWeaponOutputPort outputPort);

        void GetWeapons(IGetWeaponOutputPort outputPort);

        #endregion Methods

    }

}