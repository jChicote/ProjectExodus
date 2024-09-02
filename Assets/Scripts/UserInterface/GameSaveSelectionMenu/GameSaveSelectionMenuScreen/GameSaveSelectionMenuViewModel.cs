using System;
using ProjectExodus.Common.Infrastructure;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Mappers;
using UnityEngine;
using UserInterface.GameSaveSelectionMenu.Mediator;

namespace UserInterface.GameSaveSelectionMenu.GameSaveSelectionMenuScreen
{

    public class GameSaveSelectionMenuViewModel : IGameSaveSelectionNotifier
    {

        #region - - - - - - Fields - - - - - -
        
        private ICommand m_CreateNewGameCommand;
        private ICommand m_ClearGameSaveSlotCommand;
        private ICommand m_EditGameSaveSlotCommand;
        private ICommand m_QuitGameCommand;
        
        private readonly IGameSaveSelectionView m_GameSaveSelectionView;
        private readonly IObjectMapper m_Mapper;
        private readonly IGameSaveSelectionMenuMediator m_Mediator;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameSaveSelectionMenuViewModel(
            IGameSaveSelectionMenuMediator gameSaveSelectionMenuMediator,
            IGameSaveSelectionView gameSaveSelectionView)
        {
            this.m_GameSaveSelectionView = gameSaveSelectionView ??
                                                throw new ArgumentNullException(nameof(gameSaveSelectionView));
            this.m_Mediator = gameSaveSelectionMenuMediator ??
                                throw new ArgumentNullException(nameof(gameSaveSelectionMenuMediator));

            this.BindViewEvents();
            this.RegisterMediatorActions();
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public ICommand CreateNewGameCommand => this.m_CreateNewGameCommand;

        public ICommand ClearGameSaveSlotCommand => this.m_ClearGameSaveSlotCommand;

        public ICommand EditGameSaveSlotCommand => this.m_EditGameSaveSlotCommand;

        public ICommand QuitGameCommand => this.m_QuitGameCommand;

        #endregion Properties
  
        #region - - - - - - Events - - - - - -

        public event Action OnDisableViewInteraction;

        public event Action OnEnableViewInteraction;

        public event Action OnShowEmptySlotButtonOptions;

        public event Action OnShowEditSlotButtonOptions;

        #endregion Events
  
        #region - - - - - - Methods - - - - - -
        
        // -----------------------------------------
        // Initialization methods
        // -----------------------------------------

        private void BindViewEvents()
        {
            this.m_CreateNewGameCommand = new RelayCommand(this.NewGameSlot);
            this.m_ClearGameSaveSlotCommand = new RelayCommand(this.ClearGameSaveSlot);
            this.m_EditGameSaveSlotCommand = new RelayCommand(this.EditGameSaveSlot);
            this.m_QuitGameCommand = new RelayCommand(this.QuiteGame);
            
            this.m_GameSaveSelectionView.BindToViewModel(this);
        }

        private void RegisterMediatorActions()
        {
            this.m_Mediator.Register(GameSaveMenuEventType.OnEmptySlotSelection, this.RevealEmptySlotMenuActionButtons);
            this.m_Mediator.Register(GameSaveMenuEventType.OnGameSaveSlotSelection, this.RevealGameSaveSlotMenuActionButtons);
        }
        
        // -----------------------------------------
        // Command Actions
        // -----------------------------------------
        
        private void NewGameSlot()
        {
            this.OnDisableViewInteraction?.Invoke();
            this.m_Mediator.Invoke(GameSaveMenuEventType.StartCreatingNewGameSlot);
        }

        private void ClearGameSaveSlot() 
            => Debug.LogWarning("[WARNING]: Behavior is not implemented.");

        private void EditGameSaveSlot()
        {this.OnDisableViewInteraction?.Invoke();
            this.m_Mediator.Invoke(GameSaveMenuEventType.StartEditingGameSlot);
        }

        private void QuiteGame() 
            => Debug.LogWarning("[WARNING]: Behavior is not implemented.");
        
        // -----------------------------------------
        // Event Invokers
        // -----------------------------------------

        private void RevealEmptySlotMenuActionButtons()
            => this.OnShowEmptySlotButtonOptions?.Invoke();

        private void RevealGameSaveSlotMenuActionButtons()
            => this.OnShowEditSlotButtonOptions?.Invoke();

        #endregion Methods

    }

}