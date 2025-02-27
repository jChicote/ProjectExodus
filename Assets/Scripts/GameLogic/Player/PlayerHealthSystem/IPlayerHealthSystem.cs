using MBT;
using ProjectExodus.UserInterface.GameplayHUD;

namespace ProjectExodus.GameLogic.Player.PlayerHealthSystem
{

    public interface IPlayerHealthSystem
    {

        #region - - - - - - Methods - - - - - -

        void Initializer(IGameplayHUDController gameplayHUDController, IPlayerObserver playerObserver, float platingHealth, float shieldHealth);

        void UpgradePlating(float upgradeValue);

        void UpgradeShields(float upgradeValue);

        void MakeInvincible(bool isInvincible);

        #endregion Methods

    }

}