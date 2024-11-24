using ProjectExodus.Backend.UseCases.ShipUseCases.GetShip;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.GameLogic.OutputHandlers
{

    public class GetShipOutputResult : IGetShipOutputPort
    {

        #region - - - - - - Properties - - - - - -

        public ShipModel Result { get; set; }
        
        public bool IsSuccessful { get; set; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        void IGetShipOutputPort.PresentShip(ShipModel ship)
        {
            this.Result = ship;
            this.IsSuccessful = true;
        }

        void IGetShipOutputPort.PresentShipNotFound() 
            => this.IsSuccessful = false;

        #endregion Methods
  
    }

}