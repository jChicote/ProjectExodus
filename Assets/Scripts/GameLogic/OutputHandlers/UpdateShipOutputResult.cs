using ProjectExodus.Backend.UseCases.ShipUseCases.UpdateShip;

namespace ProjectExodus.GameLogic.OutputHandlers
{

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