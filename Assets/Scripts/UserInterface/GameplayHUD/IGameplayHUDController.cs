using ProjectExodus.GameLogic.Pause.PauseController;
using ProjectExodus.UserInterface.Controllers;

namespace ProjectExodus.UserInterface.GameplayHUD
{

    public interface IGameplayHUDController
    {

        #region - - - - - - Initializers - - - - - -

        void Initialize(IPauseController pauseController, IUserInterfaceController userInterfaceController);

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void SetMaxHealthValues(float maxPlating, float maxShield);

        void SetHealthValues(float platingHealth, float shieldHealth);

        void SetWeaponCooldownValues(float currentCooldown, float maxCooldown);

        #endregion Methods

    }

}