using ProjectExodus.UserInterface.GameplayHUD;

namespace ProjectExodus.GameLogic.Player.PlayerHealthSystem
{

    public interface IPlayerHealthSystem
    {

        #region - - - - - - Methods - - - - - -

        void Initializer(IGameplayHUDController gameplayHUDController, float platingHealth, float shieldHealth);

        void UpgradePlating(float upgradeValue);

        void UpgradeShields(float upgradeValue);

        #endregion Methods

    }

}