using ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip;

namespace ProjectExodus.GameLogic.Facades.ShipActionFacade
{

    public interface IShipActionFacade
    {

        #region - - - - - - Methods - - - - - -

        void CreateShip(CreateShipInputPort inputPort, ICreateShipOutputPort outputPort);

        #endregion Methods

    }

}