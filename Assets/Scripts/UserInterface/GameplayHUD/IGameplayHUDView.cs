namespace ProjectExodus.UserInterface.GameplayHUD
{

    public interface IGameplayHUDView
    {

        #region - - - - - - Methods - - - - - -

        void BindToViewModel(IGameplayHUDNotifyEvents viewNotifyEvents);

        void SetGameplayHUDValues(GameplayHUDValues values);
        
        #endregion Methods

    }

}