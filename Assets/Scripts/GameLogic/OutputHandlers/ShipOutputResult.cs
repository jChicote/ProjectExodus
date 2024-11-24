using ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip;
using ProjectExodus.Backend.UseCases.ShipUseCases.GetShip;
using ProjectExodus.Backend.UseCases.ShipUseCases.UpdateShip;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.GameLogic.OutputHandlers
{

    public class ShipOutputResult : ICreateShipOutputPort
    {

        #region - - - - - - Properties - - - - - -

        public ShipModel Result { get; set; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        void ICreateShipOutputPort.PresentCreatedShip(ShipModel ship)
            => this.Result = ship;

        #endregion Methods

    }
    
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
    
    public class UpdateShipOutputResult : IUpdateShipOutputPort
    {

        #region - - - - - - Properties - - - - - -

        public bool IsSuccessful { get; set; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        void IUpdateShipOutputPort.PresentSuccessful()
            => this.IsSuccessful = true;

        void IUpdateShipOutputPort.PresentShipNotFound()
            => this.IsSuccessful = false;

        #endregion Methods
  
    }

}