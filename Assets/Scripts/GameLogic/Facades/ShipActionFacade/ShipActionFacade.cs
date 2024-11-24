using System;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip;
using ProjectExodus.Backend.UseCases.ShipUseCases.GetShip;
using ProjectExodus.Backend.UseCases.ShipUseCases.UpdateShip;

namespace ProjectExodus.GameLogic.Facades.ShipActionFacade
{

    public class ShipActionFacade : IShipActionFacade
    {

        #region - - - - - - Fields - - - - - -

        private readonly IUseCaseInteractor<CreateShipInputPort, ICreateShipOutputPort> m_CreateShipInteractor;
        private readonly IUseCaseInteractor<GetShipInputPort, IGetShipOutputPort> m_GetShipInteractor;
        private readonly IUseCaseInteractor<UpdateShipInputPort, IUpdateShipOutputPort> m_UpdateShipInteractor;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public ShipActionFacade(
            IUseCaseInteractor<CreateShipInputPort, ICreateShipOutputPort> createShipInteractor,
            IUseCaseInteractor<GetShipInputPort, IGetShipOutputPort> getShipInteractor,
            IUseCaseInteractor<UpdateShipInputPort, IUpdateShipOutputPort> updateShipInteractor)
        {
            this.m_CreateShipInteractor = 
                createShipInteractor ?? throw new ArgumentNullException(nameof(createShipInteractor));
            this.m_GetShipInteractor =
                getShipInteractor ?? throw new ArgumentNullException(nameof(getShipInteractor));
            this.m_UpdateShipInteractor =
                updateShipInteractor ?? throw new ArgumentNullException(nameof(updateShipInteractor));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IShipActionFacade.CreateShip(CreateShipInputPort inputPort, ICreateShipOutputPort outputPort)
            => this.m_CreateShipInteractor.Handle(inputPort, outputPort);

        void IShipActionFacade.GetShip(GetShipInputPort inputPort, IGetShipOutputPort outputPort)
            => this.m_GetShipInteractor.Handle(inputPort, outputPort);

        void IShipActionFacade.UpdateShip(UpdateShipInputPort inputPort, IUpdateShipOutputPort outputPort)
            => this.m_UpdateShipInteractor.Handle(inputPort, outputPort);

        #endregion Methods

    }

}