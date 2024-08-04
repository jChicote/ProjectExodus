using ProjectExodus.Management.GameStateManager;
using ProjectExodus.Management.InputManager;
using UnityEngine;

namespace ProjectExodus.GameLogic.GameStates.MainMenuState
{

    public class MainMenuState : IGameState
    {
        
        #region - - - - - - Fields - - - - - -

        private readonly IInputManager m_InputManager;

        #endregion Fields
  
        #region - - - - - - Constructor - - - - - -

        public MainMenuState(IInputManager inputManager)
        {
            this.m_InputManager = inputManager;
        }        

        #endregion Constructor
        
        #region - - - - - - Methods - - - - - -

        void IGameState.StartState()
        {
            /* Expected behavior:
             *  - Presents the MainMenu screen
             *  - Ensures that no gameplay related behavior is running
             *  - Switches the acting input to 'UI'
             */
            
            this.m_InputManager.SwitchToUserInterfaceInputControls();
        }

        void IGameState.EndState()
        {
            Debug.LogWarning("[NOT IMPLEMENTED] No MainMenu.EndState is configured");
        }

        #endregion Methods

    }

}