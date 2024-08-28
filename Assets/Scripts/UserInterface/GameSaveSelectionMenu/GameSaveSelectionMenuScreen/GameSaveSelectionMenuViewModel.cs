using System;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.UserInterface;
using ProjectExodus.UserInterface.GameSaveSelectionMenu;
using UserInterface.GameSaveSelectionMenu.Mediator;

namespace UserInterface.GameSaveSelectionMenu.GameSaveSelectionMenuScreen
{

    public class GameSaveSelectionMenuViewModel
    {

        #region - - - - - - Fields - - - - - -

        private readonly IGameSaveFacade m_GameSaveFacade;
        private readonly GameSaveSelectionMenuView m_GameSaveSelectionMenuView;
        private readonly IObjectMapper m_Mapper;
        private readonly IGameSaveSelectionMenuMediator m_Mediator;

        private int m_SelectedIndex = -1; // Set to negative so that no selection is valid.

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameSaveSelectionMenuViewModel(
            IGameSaveFacade gameSaveFacade,
            IGameSaveSelectionMenuMediator gameSaveSelectionMenuMediator,
            GameSaveSelectionMenuView gameSaveSelectionMenuView)
        {
            this.m_GameSaveFacade = gameSaveFacade ?? throw new ArgumentNullException(nameof(gameSaveFacade));
            this.m_GameSaveSelectionMenuView = gameSaveSelectionMenuView ??
                                                throw new ArgumentNullException(nameof(gameSaveSelectionMenuView));
            this.m_Mediator = gameSaveSelectionMenuMediator ??
                                throw new ArgumentNullException(nameof(gameSaveSelectionMenuMediator));
            
            // Register Handlers
            this.m_Mediator.Register(GameSaveMenuEventType.OnEmptySlotSelection, this.RevealEmptySlotMenuActionButtons);
            this.m_Mediator.Register(GameSaveMenuEventType.OnGameSaveSlotSelection, this.RevealGameSaveSlotMenuActionButtons);
            
            // Bind Events
            GameSaveSelectionMenuButtonsView _MenuButtons = this.m_GameSaveSelectionMenuView.MenuButtons;
            _MenuButtons.NewGameButton.onClick.AddListener(this.OnNewGameSlot);
            _MenuButtons.EditButton.onClick.AddListener(this.OnEditGameSlot);
            _MenuButtons.ClearButton.onClick.AddListener(this.OnClearGame);
            _MenuButtons.QuitButton.onClick.AddListener(this.OnQuiteGame);
        }

        #endregion Constructors

        #region - - - - - - Events - - - - - -

        private void OnNewGameSlot() 
            => this.m_Mediator.Invoke(GameSaveMenuEventType.StartCreatingNewGameSlot);

        private void OnEditGameSlot() 
            => this.m_Mediator.Invoke(GameSaveMenuEventType.StartEditingGameSlot);

        private void OnClearGame()
        {
            
        }

        private void OnQuiteGame()
        {
            
        }

        #endregion Events
  
        #region - - - - - - Methods - - - - - -

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