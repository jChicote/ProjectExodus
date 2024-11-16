using System.Collections.Generic;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Infrastructure.Providers;

namespace ProjectExodus.UserInterface.ShipSelectionScreen
{

    public interface IShipSelectionScreenPresenter
    {

        #region - - - - - - Methods - - - - - -

        void Initialize(List<ShipModel> availableShips, IShipAssetProvider shipAssetProvider);

        #endregion Methods

    }

}