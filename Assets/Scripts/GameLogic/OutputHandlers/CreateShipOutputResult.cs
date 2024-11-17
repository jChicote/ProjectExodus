using System;
using ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.GameLogic.OutputHandlers
{

    public class CreateShipOutputResult : ICreateShipOutputPort
    {

        #region - - - - - - Properties - - - - - -

        public ShipModel Result { get; set; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        void ICreateShipOutputPort.PresentCreatedShip(ShipModel ship)
            => this.Result = ship;

        #endregion Methods

    }

}