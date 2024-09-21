using System;
using System.Collections;
using System.Threading.Tasks;
using ProjectExodus.GameLogic.Coroutines;
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

        private readonly ICoroutineManager m_CoroutineManager;
        private readonly IInputManager m_InputManager;
        private readonly ISceneLoader m_SceneLoader;
        private readonly ISceneManager m_SceneManager;
        private readonly IUserInterfaceScreenStateManager m_UserInterfaceStateManager;

        #endregion Fields
  
        #region - - - - - - Constructor - - - - - -

        public GameplayState(
            ICoroutineManager coroutineManager,
            IInputManager inputManager, 
            ISceneLoader sceneLoader,
            ISceneManager sceneManager,
            IUserInterfaceScreenStateManager userInterfaceScreenStateManager)
        {
            this.m_CoroutineManager = coroutineManager ?? throw new ArgumentNullException(nameof(coroutineManager));
            this.m_InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
            this.m_SceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
            this.m_SceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
            this.m_UserInterfaceStateManager = userInterfaceScreenStateManager ??
                                                throw new ArgumentNullException(
                                                    nameof(userInterfaceScreenStateManager));
        }

        #endregion Constructor
  
        #region - - - - - - Methods - - - - - -

        IEnumerator IGameState.StartState()
        {
            // Load scene
            // Temporarily will be a DebugScene1 invoker for now but later will need to handle different scenes.
            yield return this.m_CoroutineManager.StartNewCoroutine(
                this.m_SceneLoader.LoadScene(GameScenes.DebugScene1));
            
            // Startup the scene
            ISceneController _ActiveSceneController = this.m_SceneManager.GetActiveSceneController();
            _ActiveSceneController.InitialiseSceneController();
            _ActiveSceneController.RunSceneStartup();
            
            this.m_UserInterfaceStateManager.OpenScreen(UIScreenType.GameplayHUD);

            yield return null;
        }
        
        IEnumerator IGameState.EndState()
        {
            this.m_InputManager.UnpossesGameplayInputControls();
            yield return null;
        }

        #endregion Methods
  
    }

}