using ProjectExodus.Management.InputManager;
using ProjectExodus.UserInterface.MainMenu;
using UnityEngine;

namespace ProjectExodus.GameLogic.GameStates.MainMenuState
{

    public class MainMenuState : IGameState
    {
        
        #region - - - - - - Fields - - - - - -

        private readonly IInputManager m_InputManager;
        private readonly IMainMenuController m_MainMenuController;

        #endregion Fields
  
        #region - - - - - - Constructor - - - - - -

        public MainMenuState(IInputManager inputManager, IMainMenuController mainMenuController)
        {
            this.m_InputManager = inputManager;
            this.m_MainMenuController = mainMenuController;
        }

        #endregion Constructor
        
        #region - - - - - - Methods - - - - - -

        void IGameState.StartState()
        {
            this.m_InputManager.SwitchToUserInterfaceInputControls();
            this.m_MainMenuController.ShowMainMenu();
        }

        void IGameState.EndState()
        {
            Debug.LogWarning("[NOT IMPLEMENTED] No MainMenu.EndState is configured");
        }

        #endregion Methods

    }

}