using System;
using ProjectExodus.GameLogic.Common.Timers;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.GameLogic.Pause.PauseController;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.UserInterface.Controllers;
using UnityEngine;

namespace ProjectExodus.UserInterface.GameplayHUD
{

    public class GameplayHUDController : PausableMonoBehavior, IGameplayHUDController, IScreenStateController
    {

        #region - - - - - - Fields - - - - - -

        private GameplayHUDView m_View;
        private IUserInterfaceController m_UserInterfaceController;
        private IPauseController m_PauseController;
        private EventTimer m_AfterburnFadeTimer;

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
            
            // Temporary: Until the UI is fleshed out and with no parameters. The weapon UI is initialized here.
            this.m_View.SetDefaultWeaponValues();
            this.m_AfterburnFadeTimer = new EventTimer(2f, Time.deltaTime, this.FadeOutAfterburn, canRun: false);
            
            // Register game events
            IUIEventCollection _EventCollection = null;
            _EventCollection.RegisterEvent(
                GameplayHUDEvents.UpdateAfterburn.ToString(), 
                eventObject => this.SetAfterburnFill(eventObject as AfterburnDto));
        }

        #endregion Initializers

        #region - - - - - - Unity Methods - - - - - -

        private void Update()
        {
            if (this.m_IsPaused) return;

            this.m_AfterburnFadeTimer.TickTimer();
        }

        #endregion Unity Methods
  
        #region - - - - - - Health Methods - - - - - -

        void IGameplayHUDController.SetMaxHealthValues(float maxPlating, float maxShield)
            => this.m_View.SetMaxHealthValues(maxPlating, maxShield);

        void IGameplayHUDController.SetHealthValues(float platingHealth, float shieldHealth)
            => this.m_View.UpdateHealthBars(platingHealth, shieldHealth);

        #endregion Health Methods

        #region - - - - - - Weapon Methods - - - - - -
        
        void IGameplayHUDController.SetWeaponCooldownValues(float currentCooldown, float maxCooldown) 
            => this.m_View.UpdateWeaponCooldown(currentCooldown, maxCooldown);

        public void SetAfterburnFill(float currentFill, float maxFill)
        {
            throw new NotImplementedException();
        }

        #endregion Weapon Methods

        #region - - - - - - Movement Methods - - - - - -

        private void SetAfterburnFill(AfterburnDto afterburn)
        {
            this.m_View.UpdateAfterburnFill(afterburn.CurrentFill, afterburn.MaxFill);
            this.m_AfterburnFadeTimer.ResetTimer();
            this.m_AfterburnFadeTimer.EnableTimer();
        }

        private void FadeOutAfterburn()
        {
            this.m_AfterburnFadeTimer.ResetTimer();
            this.m_AfterburnFadeTimer.DisableTimer();
            this.m_View.HideAfterburn();
        }

        #endregion Movement Methods
  
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

public class AfterburnDto
{

    #region - - - - - - Properties - - - - - -

    public float CurrentFill { get; set; }
    
    public float MaxFill { get; set; }

    #endregion Properties
  
}