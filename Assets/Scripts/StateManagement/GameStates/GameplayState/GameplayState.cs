using System;
using ProjectExodus.GameLogic.Scene;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.InputManager;
using ProjectExodus.Management.SceneManager;
using ProjectExodus.Management.UserInterfaceScreenStatesManager;
using UnityEngine;

namespace ProjectExodus.StateManagement.GameStates.GameplayState
{

    public class GameplayState : IGameState
    {

        #region - - - - - - Fields - - - - - -

        private readonly IInputManager m_InputManager;
        private readonly ISceneManager m_SceneManager;
        private readonly IUserInterfaceScreenStateManager m_UserInterfaceStateManager;

        #endregion Fields
  
        #region - - - - - - Constructor - - - - - -

        public GameplayState(
            IInputManager inputManager, 
            ISceneManager sceneManager,
            IUserInterfaceScreenStateManager userInterfaceScreenStateManager)
        {
            this.m_InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
            this.m_SceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
            this.m_UserInterfaceStateManager = userInterfaceScreenStateManager ??
                                                throw new ArgumentNullException(
                                                    nameof(userInterfaceScreenStateManager));
        }

        #endregion Constructor
  
        #region - - - - - - Methods - - - - - -

        void IGameState.StartState()
        {
            // Startup the scene
            ISceneController _ActiveSceneController = this.m_SceneManager.GetActiveSceneController();
            _ActiveSceneController.InitialiseSceneController();
            _ActiveSceneController.RunSceneStartup();

            // Activate game input
            // this.m_InputManager.PossesGameplayInputControls();
            // this.m_InputManager.SwitchToGameplayInputControls();
            
            this.m_UserInterfaceStateManager.OpenScreen(UIScreenType.GameplayHUD);
            
            Debug.Log("Started Game State");
        }

        void IGameState.EndState() 
            => this.m_InputManager.UnpossesGameplayInputControls();

        #endregion Methods
  
    }

}