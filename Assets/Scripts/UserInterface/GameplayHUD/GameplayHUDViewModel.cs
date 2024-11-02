using System;
using ProjectExodus.Common.Infrastructure;
using ProjectExodus.Common.Services;
using ProjectExodus.UserInterface.GameplayHUD.Mediator;
using UnityEngine;

namespace ProjectExodus.UserInterface.GameplayHUD
{

    public class GameplayHUDViewModel : MonoBehaviour, IGameplayHUDNotifyEvents
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

        public event Action<float, float> OnShipHealthUpdate;

        public event Action<int> OnAmmoCountUpdate;

        #endregion Events

        #region - - - - - - Methods - - - - - -

        private void BindLogicToCommands() 
            => this.m_PauseGameCommand = new RelayCommand(this.PauseGame);

        private void RegisterMediatorActions()
        {
            // this.m_Mediator.Register<(
            //     GameplayHUDMediatorEvent.HealthBars_Update,
            //     this.OnShipHealthUpdate.Invoke);
            
            this.m_Mediator.Register<int>(
                    GameplayHUDMediatorEvent.AmmoCountBar_Update,
                    this.OnAmmoCountUpdate.Invoke);
        }

        private void PauseGame()
        {
            
        }

        #endregion Methods
  
    }

}