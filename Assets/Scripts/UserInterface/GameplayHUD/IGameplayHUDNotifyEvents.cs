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

        event Action<float, float> OnShipHealthUpdate;

        #endregion Events

    }

}