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

            this.BindLogicToCommands();
            this.RegisterMediatorActions();
            this.m_GameSaveSelectionView.BindToViewModel(this);
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
        // Setup Methods
        // -----------------------------------------

        private void BindLogicToCommands()
        {
            this.m_CreateNewGameCommand = new RelayCommand(this.NewGameSaveSlot);
            this.m_ClearGameSaveSlotCommand = new RelayCommand(this.ClearGameSaveSlot);
            this.m_EditGameSaveSlotCommand = new RelayCommand(this.EditGameSaveSlot);
            this.m_QuitGameCommand = new RelayCommand(this.QuiteGame);
        }

        private void RegisterMediatorActions()
        {
            this.m_Mediator.Register(GameSaveMenuEventType.OnEmptySlotSelection, this.DisplayEmptySaveSlotMenuButtons);
            this.m_Mediator.Register(GameSaveMenuEventType.OnGameSaveSlotSelection, this.DisplayEditGameSaveSlotMenuButtons);
            this.m_Mediator.Register(GameSaveMenuEventType.ShowGameSaveSlotSelectionMenu, this.ShowGameSaveSelectionMenu);
        }
        
        // -----------------------------------------
        // Command Actions
        // -----------------------------------------
        
        private void NewGameSaveSlot()
        {
            this.OnDisableViewInteraction?.Invoke();
            this.m_Mediator.Invoke(GameSaveMenuEventType.StartCreatingNewGameSlot);
        }

        private void ClearGameSaveSlot() 
            => Debug.LogWarning("[WARNING]: Behavior is not implemented.");

        private void EditGameSaveSlot()
        {
            this.OnDisableViewInteraction?.Invoke();
            this.m_Mediator.Invoke(GameSaveMenuEventType.StartEditingGameSlot);
        }

        private void QuiteGame() 
            => Debug.LogWarning("[WARNING]: Behavior is not implemented.");
        
        // -----------------------------------------
        // View Model Actions
        // -----------------------------------------

        private void DisplayEmptySaveSlotMenuButtons()
            => this.OnShowEmptySlotButtonOptions?.Invoke();

        private void DisplayEditGameSaveSlotMenuButtons()
            => this.OnShowEditSlotButtonOptions?.Invoke();

        private void ShowGameSaveSelectionMenu() 
            => this.OnEnableViewInteraction?.Invoke();

        #endregion Methods

    }

}