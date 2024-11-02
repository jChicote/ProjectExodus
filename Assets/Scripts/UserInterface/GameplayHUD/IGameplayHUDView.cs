namespace ProjectExodus.UserInterface.GameplayHUD
{

    public interface IGameplayHUDView
    {

        #region - - - - - - Initializers - - - - - -

        void Initialize(int maxAmmoCount, float maxPlatingHealth, float maxShieldHealth);

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void BindToViewModel(IGameplayHUDNotifyEvents viewNotifyEvents);

        #endregion Methods

    }

}