using System;
using System.Threading.Tasks;
using ProjectExodus.GameLogic.Enumeration;
using ProjectExodus.GameLogic.Scene;
using ProjectExodus.GameLogic.Scene.SceneLoader;
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
        private readonly ISceneLoader m_SceneLoader;
        private readonly ISceneManager m_SceneManager;
        private readonly IUserInterfaceScreenStateManager m_UserInterfaceStateManager;

        #endregion Fields
  
        #region - - - - - - Constructor - - - - - -

        public GameplayState(
            IInputManager inputManager, 
            ISceneLoader sceneLoader,
            ISceneManager sceneManager,
            IUserInterfaceScreenStateManager userInterfaceScreenStateManager)
        {
            this.m_InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
            this.m_SceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
            this.m_SceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
            this.m_UserInterfaceStateManager = userInterfaceScreenStateManager ??
                                                throw new ArgumentNullException(
                                                    nameof(userInterfaceScreenStateManager));
        }

        #endregion Constructor
  
        #region - - - - - - Methods - - - - - -

        async Task IGameState.StartState()
        {
            // Load scene
            // Temporarily will be a DebugScene1 invoker for now but later will need to handle different scenes.
            await GameScenes.DebugScene1.LoadScene.Invoke(this.m_SceneLoader);
            
            // Startup the scene
            ISceneController _ActiveSceneController = this.m_SceneManager.GetActiveSceneController();
            _ActiveSceneController.InitialiseSceneController();
            _ActiveSceneController.RunSceneStartup();
            
            this.m_UserInterfaceStateManager.OpenScreen(UIScreenType.GameplayHUD);
        }
        Task IGameState.EndState()
        {
            this.m_InputManager.UnpossesGameplayInputControls();
            return Task.CompletedTask;
        }

        #endregion Methods
  
    }

}