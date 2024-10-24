using System;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.WeaponUseCases.CreateWeapon;
using ProjectExodus.Backend.UseCases.WeaponUseCases.GetWeapons;

namespace ProjectExodus.GameLogic.Facades.WeaponActionFacade
{

    public class WeaponActionFacade : IWeaponActionFacade
    {

        #region - - - - - - Fields - - - - - -

        private readonly IUseCaseInteractor<CreateWeaponInputPort, ICreateWeaponOutputPort> m_CreateWeaponInteractor;
        private readonly IUseCaseInteractor<GetWeaponInputPort, IGetWeaponOutputPort> m_GetWeaponInteractor;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public WeaponActionFacade(
            IUseCaseInteractor<CreateWeaponInputPort, ICreateWeaponOutputPort> createWeaponInteractor,
            IUseCaseInteractor<GetWeaponInputPort, IGetWeaponOutputPort> getWeaponInteractor)
        {
            this.m_CreateWeaponInteractor = createWeaponInteractor
                ?? throw new ArgumentNullException(nameof(createWeaponInteractor));
            this.m_GetWeaponInteractor = getWeaponInteractor
                ?? throw new ArgumentNullException(nameof(getWeaponInteractor));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IWeaponActionFacade.CreateWeapon(CreateWeaponInputPort inputPort, ICreateWeaponOutputPort outputPort)
            => this.m_CreateWeaponInteractor.Handle(inputPort, outputPort);

        void IWeaponActionFacade.GetWeapons(IGetWeaponOutputPort outputPort)
            => this.m_GetWeaponInteractor.Handle(new GetWeaponInputPort(), outputPort);

        #endregion Methods

    }

}