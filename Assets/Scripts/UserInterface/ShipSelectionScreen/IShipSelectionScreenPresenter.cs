using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.Management.GameStateManager;
using ProjectExodus.UserInterface.Controllers;

namespace ProjectExodus.UserInterface.ShipSelectionScreen
{

    public interface IShipSelectionScreenPresenter
    {

        #region - - - - - - Methods - - - - - -

        void Initialize(
            IGameStateManager gameStateManager,
            IShipAssetProvider shipAssetProvider, 
            IUserInterfaceController userInterfaceController,
            IWeaponAssetProvider weaponAssetProvider);

        #endregion Methods

    }

}