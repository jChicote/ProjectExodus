using System;
using ProjectExodus.Common.Infrastructure;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.UserInterface.GameSaveSelectionMenu.Common;
using UnityEngine;
using UserInterface.GameSaveSelectionMenu.Mediator;

namespace UserInterface.GameSaveSelectionMenu.GameSaveSelectionMenuScreen
{

    public class GameSaveSelectionMenuViewModel : IGameSaveSelectionNotifier
    {

        #region - - - - - - Fields - - - - - -
        
        private readonly IObjectMapper m_Mapper;
        private readonly IGameSaveSelectionMenuMediator m_Mediator;
        
        private ICommand m_CreateNewGameCommand;
        private ICommand m_ClearGameSaveSlotCommand;
        private ICommand m_EditGameSaveSlotCommand;
        private ICommand m_QuitGameCommand;

        private Guid? m_CurrentlySelectedGameSaveID;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameSaveSelectionMenuViewModel(
            IGameSaveSelectionMenuMediator gameSaveSelectionMenuMediator,
            IGameSaveSelectionView gameSaveSelectionView)
        {
            this.m_Mediator = gameSaveSelectionMenuMediator ??
                                throw new ArgumentNullException(nameof(gameSaveSelectionMenuMediator));

            this.BindLogicToCommands();
            this.RegisterMediatorActions();
            gameSaveSelectionView.BindToViewModel(this);
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
            this.m_QuitGameCommand = new RelayCommand(this.QuitGame);
        }

        private void RegisterMediatorActions()
        {
            this.m_Mediator.Register(
                GameSaveMenuEventType.EmptySaveSlot_Selected, 
                this.DisplayEmptySaveSlotMenuButtons);
            this.m_Mediator.Register(
                GameSaveMenuEventType.GameSaveSlot_Selected, 
                this.DisplayEditGameSaveSlotMenuButtons);
            this.m_Mediator.Register<GameSaveSlotModelWrapper>(
                GameSaveMenuEventType.GameSaveSlot_Selected,
                this.SetCurrentSlotSelection);
            this.m_Mediator.Register(
                GameSaveMenuEventType.GameSaveMenuInteraction_Enabled, 
                this.ShowGameSaveSelectionMenu);
        }
        
        // -----------------------------------------
        // Command Actions
        // -----------------------------------------
        
        private void NewGameSaveSlot()
        {
            this.OnDisableViewInteraction?.Invoke();
            this.m_Mediator.Invoke(GameSaveMenuEventType.CreateNewGameSlot_Open);
        }

        private void ClearGameSaveSlot()
        {
        }

        private void EditGameSaveSlot()
        {
            this.OnDisableViewInteraction?.Invoke();
            this.m_Mediator.Invoke(GameSaveMenuEventType.EditGameSlot_Open);
        }

        private void QuitGame()
        {
            #if UNITY_STANDALONE
                Application.Quit();
            #endif
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

        // -----------------------------------------
        // View Model Actions
        // -----------------------------------------

        private void DisplayEmptySaveSlotMenuButtons()
            => this.OnShowEmptySlotButtonOptions?.Invoke();

        private void DisplayEditGameSaveSlotMenuButtons()
            => this.OnShowEditSlotButtonOptions?.Invoke();

        private void SetCurrentSlotSelection(GameSaveSlotModelWrapper gameSaveSlotDisplayWrapper) 
            => this.m_CurrentlySelectedGameSaveID = gameSaveSlotDisplayWrapper.GameSaveSlotDto.ID;

        private void ShowGameSaveSelectionMenu() 
            => this.OnEnableViewInteraction?.Invoke();

        #endregion Methods

    }

}