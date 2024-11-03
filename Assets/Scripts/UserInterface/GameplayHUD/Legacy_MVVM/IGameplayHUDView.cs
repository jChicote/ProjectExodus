using System;

namespace ProjectExodus.UserInterface.GameplayHUD.Legacy
{

    [Obsolete]
    public interface IGameplayHUDView
    {

        #region - - - - - - Methods - - - - - -

        void BindToViewModel(IGameplayHUDNotifyEvents viewNotifyEvents);

        void SetGameplayHUDValues(GameplayHUDValues values);
        
        #endregion Methods

    }

}