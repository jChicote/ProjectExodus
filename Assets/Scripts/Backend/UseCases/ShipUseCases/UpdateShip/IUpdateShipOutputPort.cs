namespace ProjectExodus.Backend.UseCases.ShipUseCases.UpdateShip
{

    public interface IUpdateShipOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentSuccessful();

        void PresentShipNotFound();

        #endregion Methods

    }

}