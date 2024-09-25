using System;
using System.Collections;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.GameSaveManager;
using ProjectExodus.Management.InputManager;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.UserInterface.Controllers;
using UnityEngine;

namespace ProjectExodus.StateManagement.GameStates.MainMenuState
{

    public class MainMenuState : IGameState
    {
        
        #region - - - - - - Fields - - - - - -

        private readonly IInputManager m_InputManager;
        private readonly IGameSaveManager m_GameSaveManager;
        private readonly IUserInterfaceManager m_UserInterfaceManager;

        #endregion Fields
  
        #region - - - - - - Constructor - - - - - -

        public MainMenuState(
            IInputManager inputManager, 
            IGameSaveManager gameSaveManager,
            IUserInterfaceManager userInterfaceManager)
        {
            this.m_InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
            this.m_GameSaveManager = gameSaveManager ?? throw new ArgumentNullException(nameof(gameSaveManager));
            this.m_UserInterfaceManager = userInterfaceManager 
                                            ?? throw new ArgumentNullException(nameof(userInterfaceManager));
        }

        #endregion Constructor
        
        #region - - - - - - Methods - - - - - -

        IEnumerator IGameState.StartState()
        {
            this.m_InputManager.SwitchToUserInterfaceInputControls();

            IUserInterfaceController _UserInterfaceController = 
                this.m_UserInterfaceManager.GetTheActiveUserInterfaceController();
            _UserInterfaceController.OpenScreen(this.m_GameSaveManager.GameSaveModel == null
                ? UIScreenType.GameSaveMenu 
                : UIScreenType.MainMenu);

            yield return null;
        }

        IEnumerator IGameState.EndState()
        {
            Debug.LogWarning("[NOT IMPLEMENTED] No MainMenu.EndState is configured");
            yield return null;
        }

        #endregion Methods

    }

}