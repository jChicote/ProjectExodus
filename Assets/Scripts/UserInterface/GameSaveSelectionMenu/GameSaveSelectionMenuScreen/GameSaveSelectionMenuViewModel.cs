using System;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.UserInterface.GameSaveSelectionMenu.GameSaveSelectionMenuScreen;
using UnityEngine;
using UserInterface.GameSaveSelectionMenu.Mediator;

namespace UserInterface.GameSaveSelectionMenu.GameSaveSelectionMenuScreen
{

    public class GameSaveSelectionMenuViewModel
    {

        #region - - - - - - Fields - - - - - -

        private readonly GameSaveSelectionMenuView m_GameSaveSelectionMenuView;
        private readonly IObjectMapper m_Mapper;
        private readonly IGameSaveSelectionMenuMediator m_Mediator;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameSaveSelectionMenuViewModel(
            IGameSaveSelectionMenuMediator gameSaveSelectionMenuMediator,
            GameSaveSelectionMenuView gameSaveSelectionMenuView)
        {
            this.m_GameSaveSelectionMenuView = gameSaveSelectionMenuView ??
                                                throw new ArgumentNullException(nameof(gameSaveSelectionMenuView));
            this.m_Mediator = gameSaveSelectionMenuMediator ??
                                throw new ArgumentNullException(nameof(gameSaveSelectionMenuMediator));

            this.BindViewEvents();
            this.RegisterMediatorActions();
        }

        #endregion Constructors

        #region - - - - - - Events - - - - - -

        private void OnNewGameSlot() 
            => this.m_Mediator.Invoke(GameSaveMenuEventType.StartCreatingNewGameSlot);

        private void OnEditGameSlot() 
            => this.m_Mediator.Invoke(GameSaveMenuEventType.StartEditingGameSlot);

        private void OnClearGame() 
            => Debug.LogWarning("[WARNING]: Behavior is not implemented.");

        private void OnQuiteGame() 
            => Debug.LogWarning("[WARNING]: Behavior is not implemented.");

        #endregion Events
  
        #region - - - - - - Methods - - - - - -
        
        // -----------------------------------------
        // Initialization methods
        // -----------------------------------------

        private void BindViewEvents()
        {
            GameSaveSelectionMenuButtonsView _MenuButtons = this.m_GameSaveSelectionMenuView.MenuButtons;
            _MenuButtons.NewGameButton.onClick.AddListener(this.OnNewGameSlot);
            _MenuButtons.EditButton.onClick.AddListener(this.OnEditGameSlot);
            _MenuButtons.ClearButton.onClick.AddListener(this.OnClearGame);
            _MenuButtons.QuitButton.onClick.AddListener(this.OnQuiteGame);
        }

        private void RegisterMediatorActions()
        {
            this.m_Mediator.Register(GameSaveMenuEventType.OnEmptySlotSelection, this.RevealEmptySlotMenuActionButtons);
            this.m_Mediator.Register(GameSaveMenuEventType.OnGameSaveSlotSelection, this.RevealGameSaveSlotMenuActionButtons);
        }
        
        // -----------------------------------------
        // Action specific methods
        // -----------------------------------------

        private void RevealEmptySlotMenuActionButtons()
        {
            GameSaveSelectionMenuButtonsView _MenuButtons = this.m_GameSaveSelectionMenuView.MenuButtons;
            _MenuButtons.ClearButton.interactable = false;
            _MenuButtons.EditButton.interactable = false;
            _MenuButtons.NewGameButton.interactable = true;
        }

        private void RevealGameSaveSlotMenuActionButtons()
        {
            GameSaveSelectionMenuButtonsView _MenuButtons = this.m_GameSaveSelectionMenuView.MenuButtons;
            _MenuButtons.ClearButton.interactable = true;
            _MenuButtons.EditButton.interactable = true;
            _MenuButtons.NewGameButton.interactable = false;
        }

        #endregion Methods
  
    }

}