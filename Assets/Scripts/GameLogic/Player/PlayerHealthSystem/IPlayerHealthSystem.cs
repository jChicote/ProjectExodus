using ProjectExodus.UserInterface.GameplayHUD;

namespace ProjectExodus.GameLogic.Player.PlayerHealthSystem
{

    public interface IPlayerHealthSystem
    {

        #region - - - - - - Methods - - - - - -

        void SetHealth(float platingHealth, float shieldHealth);

        void SetHUDController(IGameplayHUDController gameplayHUDController);

        #endregion Methods

    }

}