namespace ProjectExodus.UserInterface.GameplayHUD
{

    public interface IGameplayHUDController
    {

        #region - - - - - - Initializers - - - - - -

        void Initialize();

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void SetMaxHealthValues(float maxPlating, float maxShield);

        void SetHealthValues(float platingHealth, float shieldHealth);
        
        #endregion Methods

    }

}