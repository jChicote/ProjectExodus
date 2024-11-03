using System;
using ProjectExodus.Common.Infrastructure;
using ProjectExodus.Common.Services;
using ProjectExodus.UserInterface.GameplayHUD.Mediator;
using UnityEngine;

namespace ProjectExodus.UserInterface.GameplayHUD.Legacy
{

    [Obsolete]
    public class GameplayHUDViewModel : IGameplayHUDNotifyEvents
    {
        
        #region - - - - - - Fields - - - - - -

        private readonly IGameplayHUDMediator m_Mediator;
        private readonly IGameplayHUDView m_View;

        private ICommand m_PauseGameCommand;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameplayHUDViewModel(IGameplayHUDMediator gameplayHUDMediator, IGameplayHUDView gameplayHUDView)
        {
            this.m_Mediator = gameplayHUDMediator ?? throw new ArgumentNullException(nameof(gameplayHUDMediator));
            this.m_View = gameplayHUDView ?? throw new ArgumentNullException(nameof(gameplayHUDView));
            
            this.BindLogicToCommands();
            this.RegisterMediatorActions();
            this.m_View.BindToViewModel(this);
        }

        #endregion Constructors
  
        #region - - - - - - Properties - - - - - -

        ICommand IGameplayHUDNotifyEvents.PauseGameCommand => m_PauseGameCommand;

        #endregion Properties
  
        #region - - - - - - Events - - - - - -

        public event Action<HealthBarsStatusDto> OnShipHealthUpdate;

        public event Action<int> OnAmmoCountUpdate;

        public event Action OnHideGui;

        public event Action OnShowGui;

        #endregion Events

        #region - - - - - - Methods - - - - - -

        private void BindLogicToCommands() 
            => this.m_PauseGameCommand = new RelayCommand(this.PauseGame);

        private void RegisterMediatorActions()
        {
            this.m_Mediator.Register<HealthBarsStatusDto>(
                GameplayHUDMediatorEvent.HealthBars_Update,
                dto => this.OnShipHealthUpdate?.Invoke(dto));
            this.m_Mediator.Register<int>(
                GameplayHUDMediatorEvent.AmmoCountBar_Update,
                ammoCount => this.OnAmmoCountUpdate?.Invoke(ammoCount));
            this.m_Mediator.Register(
                GameplayHUDMediatorEvent.GameplayHUD_Visible,
                this.OnShowGui);
            this.m_Mediator.Register(
                GameplayHUDMediatorEvent.GameplayHUD_InVisible,
                this.OnHideGui);
        }

        private void PauseGame()
            => Debug.LogWarning("[WARNING]: No pause game behavior has been implemented.");

        #endregion Methods
        
    }

    public class HealthBarsStatusDto
    {

        #region - - - - - - Properties - - - - - -

        public float PlatingHealth { get; set; }

        public float ShieldHealth { get; set; }

        #endregion Properties
  
    }

}