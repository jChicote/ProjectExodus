using System;
using ProjectExodus.Common.Services;
using ProjectExodus.UserInterface.Common;

namespace ProjectExodus.UserInterface.GameplayHUD
{

    public interface IGameplayHUDNotifyEvents : IGuiNotifyEvents
    {

        #region - - - - - - Fields - - - - - -

        ICommand PauseGameCommand { get; }

        #endregion Fields

        #region - - - - - - Events - - - - - -

        event Action<int> OnAmmoCountUpdate;

        event Action<HealthBarsStatusDto> OnShipHealthUpdate;

        #endregion Events

    }

}