using ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip;
using ProjectExodus.Backend.UseCases.ShipUseCases.GetShip;
using ProjectExodus.Backend.UseCases.ShipUseCases.UpdateShip;

namespace ProjectExodus.GameLogic.Facades.ShipActionFacade
{

    public interface IShipActionFacade
    {

        #region - - - - - - Methods - - - - - -

        void CreateShip(CreateShipInputPort inputPort, ICreateShipOutputPort outputPort);

        void GetShip(GetShipInputPort inputPort, IGetShipOutputPort outputPort);

        void UpdateShip(UpdateShipInputPort inputPort, IUpdateShipOutputPort outputPort);

        #endregion Methods

    }

}