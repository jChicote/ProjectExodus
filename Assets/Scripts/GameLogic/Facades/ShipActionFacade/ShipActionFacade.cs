using System;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip;

namespace ProjectExodus.GameLogic.Facades.ShipActionFacade
{

    public class ShipActionFacade : IShipActionFacade
    {

        #region - - - - - - Fields - - - - - -

        private readonly IUseCaseInteractor<CreateShipInputPort, ICreateShipOutputPort> m_CreateShipInteractor;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public ShipActionFacade(
            IUseCaseInteractor<CreateShipInputPort, ICreateShipOutputPort> createShipInteractor)
            => this.m_CreateShipInteractor = createShipInteractor
                ?? throw new ArgumentNullException(nameof(createShipInteractor));
            
        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IShipActionFacade.CreateShip(CreateShipInputPort inputPort, ICreateShipOutputPort outputPort)
            => this.m_CreateShipInteractor.Handle(inputPort, outputPort);

        #endregion Methods

    }

}