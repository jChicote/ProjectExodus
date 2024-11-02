using System;
using ProjectExodus.Common.Services;

namespace ProjectExodus.UserInterface.GameplayHUD
{

    public interface IGameplayHUDNotifyEvents
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