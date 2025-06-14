using ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.DebugSupport.OutputHandlers
{

    public class DebugCreateShipOutputHandler : ICreateShipOutputPort
    {

        #region - - - - - - Fields - - - - - -

        public ShipModel Result;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        public void PresentCreatedShip(ShipModel ship)
            => this.Result = ship;

        #endregion Methods

    }

}