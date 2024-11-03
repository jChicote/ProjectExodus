using ProjectExodus.UserInterface.GameplayHUD.Mediator;

namespace ProjectExodus.UserInterface.GameplayHUD
{

    public interface IGameplayHUDController
    {

        #region - - - - - - Initializers - - - - - -

        void Initialize(IGameplayHUDMediator gameplayHUDMediator);

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void SetHUDValues(GameplayHUDValues values);

        #endregion Methods

    }

}