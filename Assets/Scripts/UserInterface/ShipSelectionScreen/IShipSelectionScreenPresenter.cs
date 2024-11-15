using ProjectExodus.GameLogic.Infrastructure.Providers;

namespace ProjectExodus.UserInterface.ShipSelectionScreen
{

    public interface IShipSelectionScreenPresenter
    {

        #region - - - - - - Methods - - - - - -

        void Initialize(IShipAssetProvider shipAssetProvider);

        #endregion Methods

    }

}