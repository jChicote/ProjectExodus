using ProjectExodus.UserInterface.GameplayHUD.Mediator;

namespace ProjectExodus.UserInterface.GameplayHUD
{

    public interface IGameplayHUDController
    {

        #region - - - - - - Methods - - - - - -

        void Initialize(IGameplayHUDMediator gameplayHUDMediator);

        #endregion Methods

    }

}