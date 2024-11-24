using System;
using Codice.Client.BaseCommands;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.WeaponUseCases.CreateWeapon;
using ProjectExodus.Backend.UseCases.WeaponUseCases.GetWeapons;
using ProjectExodus.Backend.UseCases.WeaponUseCases.UpdateWeapon;

namespace ProjectExodus.GameLogic.Facades.WeaponActionFacade
{

    public class WeaponActionFacade : IWeaponActionFacade
    {

        #region - - - - - - Fields - - - - - -

        private readonly IUseCaseInteractor<CreateWeaponInputPort, ICreateWeaponOutputPort> m_CreateWeaponInteractor;
        private readonly IUseCaseInteractor<GetWeaponInputPort, IGetWeaponOutputPort> m_GetWeaponInteractor;
        private readonly IUseCaseInteractor<UpdateWeaponInputPort, IUpdateWeaponOutputPort> m_UpdateWeaponInteractor;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public WeaponActionFacade(
            IUseCaseInteractor<CreateWeaponInputPort, ICreateWeaponOutputPort> createWeaponInteractor,
            IUseCaseInteractor<GetWeaponInputPort, IGetWeaponOutputPort> getWeaponInteractor,
            IUseCaseInteractor<UpdateWeaponInputPort, IUpdateWeaponOutputPort> updateWeaponInteractor)
        {
            this.m_CreateWeaponInteractor = createWeaponInteractor
                ?? throw new ArgumentNullException(nameof(createWeaponInteractor));
            this.m_GetWeaponInteractor = getWeaponInteractor
                ?? throw new ArgumentNullException(nameof(getWeaponInteractor));
            this.m_UpdateWeaponInteractor = updateWeaponInteractor
                ?? throw new ArgumentNullException(nameof(updateWeaponInteractor));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IWeaponActionFacade.CreateWeapon(CreateWeaponInputPort inputPort, ICreateWeaponOutputPort outputPort)
            => this.m_CreateWeaponInteractor.Handle(inputPort, outputPort);

        void IWeaponActionFacade.GetWeapons(IGetWeaponOutputPort outputPort)
            => this.m_GetWeaponInteractor.Handle(new GetWeaponInputPort(), outputPort);

        void IWeaponActionFacade.UpdateWeapon(UpdateWeaponInputPort inputPort, IUpdateWeaponOutputPort outputPort)
            => this.m_UpdateWeaponInteractor.Handle(inputPort, outputPort);

        #endregion Methods

    }

}