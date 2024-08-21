using System;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.InputManager;
using ProjectExodus.Management.UserInterfaceScreenStatesManager;
using UnityEngine;

namespace ProjectExodus.StateManagement.GameStates.MainMenuState
{

    public class MainMenuState : IGameState
    {
        
        #region - - - - - - Fields - - - - - -

        private readonly IInputManager m_InputManager;
        private readonly IUserInterfaceScreenStateManager m_UserInterfaceScreenStateManager;

        #endregion Fields
  
        #region - - - - - - Constructor - - - - - -

        public MainMenuState(
            IInputManager inputManager, 
            IUserInterfaceScreenStateManager userInterfaceScreenStateManager)
        {
            this.m_InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
            this.m_UserInterfaceScreenStateManager = userInterfaceScreenStateManager ??
                                                     throw new ArgumentNullException(
                                                         nameof(userInterfaceScreenStateManager));
        }

        #endregion Constructor
        
        #region - - - - - - Methods - - - - - -

        void IGameState.StartState()
        {
            this.m_InputManager.SwitchToUserInterfaceInputControls();
            this.m_UserInterfaceScreenStateManager.OpenScreen(UIScreenType.MainMenu);
        }

        void IGameState.EndState() 
            => Debug.LogWarning("[NOT IMPLEMENTED] No MainMenu.EndState is configured");

        #endregion Methods

    }

}