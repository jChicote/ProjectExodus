using ProjectExodus.Domain.Models;

namespace ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip
{

    public interface ICreateShipOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentCreatedShip(ShipModel ship);

        #endregion Methods

    }

}