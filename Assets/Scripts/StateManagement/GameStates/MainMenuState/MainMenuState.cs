using System;
using System.Collections;
using ProjectExodus.GameLogic.Coroutines;
using ProjectExodus.GameLogic.Enumeration;
using ProjectExodus.GameLogic.Scene.SceneLoader;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.GameDataManager;
using ProjectExodus.Management.InputManager;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.UserInterface.Controllers;
using UnityEngine;

namespace ProjectExodus.StateManagement.GameStates.MainMenuState
{

    public class MainMenuState : IGameState
    {
        
        #region - - - - - - Fields - - - - - -

        private readonly ICoroutineManager m_CoroutineManager;
        private readonly IInputManager m_InputManager;
        private readonly IGameDataManager m_GameSaveManager;
        private readonly ISceneLoader m_SceneLoader;
        private readonly IUserInterfaceManager m_UserInterfaceManager;

        #endregion Fields
  
        #region - - - - - - Constructor - - - - - -

        public MainMenuState(
            ICoroutineManager coroutineManager,
            IInputManager inputManager, 
            IGameDataManager gameSaveManager,
            ISceneLoader sceneLoader,
            IUserInterfaceManager userInterfaceManager)
        {
            this.m_CoroutineManager = coroutineManager ?? throw new ArgumentNullException(nameof(coroutineManager));
            this.m_InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
            this.m_GameSaveManager = gameSaveManager ?? throw new ArgumentNullException(nameof(gameSaveManager));
            this.m_SceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
            this.m_UserInterfaceManager = userInterfaceManager 
                                            ?? throw new ArgumentNullException(nameof(userInterfaceManager));
        }

        #endregion Constructor
        
        #region - - - - - - Methods - - - - - -

        IEnumerator IGameState.StartState()
        {
            yield return this.m_CoroutineManager.StartNewCoroutine(
                this.m_SceneLoader.LoadScene(GameScenes.MainMenu));
            
            this.m_InputManager.SwitchToUserInterfaceInputControls();

            // Reveal the opening menu for this scene.
            IUserInterfaceController _UserInterfaceController = 
                this.m_UserInterfaceManager.GetTheActiveUserInterfaceController();
            _UserInterfaceController.InitialiseUserInterfaceController(); 
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