using ProjectExodus.Backend.UseCases.WeaponUseCases.CreateWeapon;
using ProjectExodus.Backend.UseCases.WeaponUseCases.GetWeapons;
using ProjectExodus.Backend.UseCases.WeaponUseCases.UpdateWeapon;

namespace ProjectExodus.GameLogic.Facades.WeaponActionFacade
{

    public interface IWeaponActionFacade
    {

        #region - - - - - - Methods - - - - - -

        void CreateWeapon(CreateWeaponInputPort inputPort, ICreateWeaponOutputPort outputPort);

        void GetWeapons(IGetWeaponOutputPort outputPort);

        void UpdateWeapon(UpdateWeaponInputPort inputPort, IUpdateWeaponOutputPort outputPort);

        #endregion Methods

    }

}