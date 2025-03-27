using System;
using ProjectExodus.GameLogic.Common.Timers;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.GameLogic.Pause.PauseController;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.UserInterfaceManager;
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
            
            this.RegisterActionsToMediator();
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

        private void SetMaxHealthValues(HealthDto health)
            => this.m_View.SetMaxHealthValues(health.MaxPlating, health.MaxShield);

        private void SetHealthValues(HealthDto health)
            => this.m_View.UpdateHealthBars(health.CurrentPlating, health.CurrentShield);

        #endregion Health Methods

        #region - - - - - - Weapon Methods - - - - - -
        
        private void SetWeaponCooldownValues(AfterburnCooldownDto cooldown) 
            => this.m_View.UpdateWeaponCooldown(cooldown.CurrentCooldown, cooldown.MaxCooldown);

        private void AddWeaponIndicator(int weaponInstanceID, WeaponType weaponType) 
            => this.m_View.AddWeaponIndicator(weaponInstanceID);

        private void UpdateIndicator(int id, int currentAmmo, int maxAmmo) 
            => this.m_View.UpdateIndicator(id, (float)currentAmmo / maxAmmo);

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
        
        public void HideScreen() 
            => this.m_View.HideHUD();

        public void ShowScreen()
            => this.m_View.ShowHUD();

        private void RegisterActionsToMediator()
        {
            // Register Gameplay HUD events
            IUIEventCollection _EventCollection = UserInterfaceManager.Instance.EventCollectionRegistry;
            _EventCollection.RegisterEvent(
                GameplayHUDEvents.UpdateAfterburn.ToString(), 
                eventObject => this.SetAfterburnFill(eventObject as AfterburnDto));
            _EventCollection.RegisterEvent(GameplayHUDEvents.FadeOutAfterburn.ToString(), this.FadeOutAfterburn);
            _EventCollection.RegisterEvent(
                GameplayHUDEvents.UpdateCooldown.ToString(),
                eventObject => this.SetWeaponCooldownValues(eventObject as AfterburnCooldownDto));
            
            _EventCollection.RegisterEvent(
                GameplayHUDEvents.SetupHealthHUD.ToString(), 
                eventObject => this.SetMaxHealthValues(eventObject as HealthDto));
            _EventCollection.RegisterEvent(
                GameplayHUDEvents.UpdateHealth.ToString(),
                eventObject => this.SetHealthValues(eventObject as HealthDto));
            
            _EventCollection.RegisterEvent(
                GameplayHUDEvents.AddWeaponIndicator.ToString(),
                weapon =>
                {
                    WeaponInfo _Weapon = weapon as WeaponInfo;
                    this.AddWeaponIndicator(_Weapon.ID, WeaponType.Turrent);
                });
            _EventCollection.RegisterEvent(
                GameplayHUDEvents.AddWeaponIndicator.ToString(),
                weapon =>
                {
                    WeaponInfo _Weapon = weapon as WeaponInfo;
                    this.UpdateIndicator(_Weapon.ID, _Weapon.CurrentAmmo, _Weapon.MaxAmmo);
                });
            
            _EventCollection.RegisterEvent(GameplayHUDEvents.ShowHUD.ToString(), this.ShowScreen);
            _EventCollection.RegisterEvent(GameplayHUDEvents.HideHUD.ToString(), this.HideScreen);
        }

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

public class AfterburnCooldownDto
{

    #region - - - - - - Properties - - - - - -

    public float CurrentCooldown { get; set; }

    public float MaxCooldown { get; set; }

    #endregion Properties
  
}

public class HealthDto
{

    #region - - - - - - Properties - - - - - -

    public float MaxPlating { get; set; }

    public float MaxShield { get; set; }
    
    public float CurrentPlating { get; set; }

    public float CurrentShield { get; set; }

    #endregion Properties
  
}

public class WeaponInfo
{

    #region - - - - - - Propertis - - - - - -

    public int ID { get; set; }
    
    public int CurrentAmmo { get; set; }

    public int MaxAmmo { get; set; }

    #endregion Propertis
  
}