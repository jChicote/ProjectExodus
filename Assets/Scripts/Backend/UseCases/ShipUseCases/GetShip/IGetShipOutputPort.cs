using ProjectExodus.Domain.Models;

namespace ProjectExodus.Backend.UseCases.ShipUseCases.GetShip
{

    public interface IGetShipOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentShip(ShipModel ship);

        void PresentShipNotFound();

        #endregion Methods

    }

}