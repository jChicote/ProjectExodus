using ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip;
using ProjectExodus.Backend.UseCases.ShipUseCases.GetShip;

namespace ProjectExodus.GameLogic.Facades.ShipActionFacade
{

    public interface IShipActionFacade
    {

        #region - - - - - - Methods - - - - - -

        void CreateShip(CreateShipInputPort inputPort, ICreateShipOutputPort outputPort);

        void GetShip(GetShipInputPort inputPort, IGetShipOutputPort outputPort);

        #endregion Methods

    }

}