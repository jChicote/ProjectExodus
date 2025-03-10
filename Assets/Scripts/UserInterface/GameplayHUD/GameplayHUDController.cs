using System;
using ProjectExodus.GameLogic.Pause.PauseController;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.UserInterface.Controllers;
using UnityEngine;

namespace ProjectExodus.UserInterface.GameplayHUD
{

    public class GameplayHUDController : MonoBehaviour, IGameplayHUDController, IScreenStateController
    {

        #region - - - - - - Fields - - - - - -

        private GameplayHUDView m_View;
        private IUserInterfaceController m_UserInterfaceController;
        private IPauseController m_PauseController;

        #endregion Fields
  
        #region - - - - - - Initializers - - - - - -

        void IGameplayHUDController.Initialize(
            IPauseController pauseController, 
            IUserInterfaceController userInterfaceController)
        {
            this.m_PauseController = pauseController ?? throw new ArgumentNullException(nameof(pauseController));
            this.m_UserInterfaceController = userInterfaceController ??
                                             throw new ArgumentNullException(nameof(userInterfaceController));
            
            this.m_View = this.GetComponent<GameplayHUDView>();
            this.BindMethodsToView();
        }

        #endregion Initializers

        #region - - - - - - Health Methods - - - - - -

        void IGameplayHUDController.SetMaxHealthValues(float maxPlating, float maxShield)
            => this.m_View.SetMaxHealthValues(maxPlating, maxShield);

        void IGameplayHUDController.SetHealthValues(float platingHealth, float shieldHealth)
            => this.m_View.UpdateHealthBars(platingHealth, shieldHealth);

        #endregion Health Methods


        #region - - - - - - Weapon Methods - - - - - -

        void IGameplayHUDController.SetWeaponCooldownValues(float currentCooldown, float maxCooldown)
        {
            // this.m_View.UpdateWeaponCooldown();
        }

        #endregion Weapon Methods
  
  
        #region - - - - - - HUD Methods - - - - - -
        
        void IScreenStateController.HideScreen() 
            => this.m_View.HideHUD();

        void IScreenStateController.ShowScreen()
            => this.m_View.ShowHUD();

        private void BindMethodsToView() 
            => this.m_View.PauseButton.onClick.AddListener(this.PauseGame);

        private void PauseGame()
        {
            this.m_UserInterfaceController.OpenScreen(UIScreenType.PauseScreen);
            this.m_PauseController.Pause();
        }

        #endregion HUD Methods

    }

}