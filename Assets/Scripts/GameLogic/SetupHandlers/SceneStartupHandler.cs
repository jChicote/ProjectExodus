using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.SetupHandlers;
using GameLogic.SetupHandlers.SceneHandlers;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Infrastructure.DataLoading.LoadCommands;
using ProjectExodus.Management.GameDataManager;
using ProjectExodus.Management.InputManager;
using ProjectExodus.UserInterface.LoadingScreen;
using UnityEngine;

namespace ProjectExodus.GameLogic.Scene.SetupHandlers
{

    public class SceneStartupHandler : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private LoadingScreenController LoadingScreenController;
        [SerializeField] private List<GameObject> m_SetupHandlersObjects;

        private IInputManager m_InputManager;
        private ILoadingScreenController m_LoadingScreenController;
        private IServiceLocator m_ServiceLocator;

        #endregion Fields

        #region - - - - - - Initialisers - - - - - -

        public void InitialiseSceneStartupController(
            IInputManager inputManager,
            IServiceLocator serviceLocator)
        {
            this.m_InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
            this.m_LoadingScreenController = LoadingScreenController 
                                                 ?? throw new NullReferenceException(nameof(ILoadingScreenController));
            this.m_ServiceLocator = serviceLocator ?? throw new ArgumentNullException(nameof(serviceLocator));
        }

        #endregion Initialisers
          
        #region - - - - - - Methods - - - - - -

        public void RunSceneStartup()
        {
            this.m_LoadingScreenController.ShowScreen();

            if (this.m_SetupHandlersObjects == null 
                || !this.m_SetupHandlersObjects.FirstOrDefault()
                || this.m_SetupHandlersObjects.Count == 0)
            {
                Debug.LogError("[ERROR]: There are no handlers configured for this scene.");
                return;
            }
            
            StartupDataContext _StartupData = new StartupDataContext
            {
                Player = GameDataManager.Instance.SelectedPlayer,
                SelectedShip = GameDataManager.Instance.SelectedShip
            };

            SceneSetupInitializationContext _InitializationContext = this.GetComponent<SceneSetupInitializationContext>();
            _InitializationContext.InputManager = this.m_InputManager;
            _InitializationContext.LoadingScreenController = this.m_LoadingScreenController;
            _InitializationContext.ServiceLocator = this.m_ServiceLocator;
            _InitializationContext.StartupDataOptions = _StartupData;

            List<ISetupHandler> _SetupHandlers = this.m_SetupHandlersObjects
                .Select(sh => sh.GetComponent<ISetupHandler>())
                .ToList();
                
            // Configure the handler chain for sequential invocation.
            for (int i = 0; i < _SetupHandlers.Count - 1; i++)
            {
                ISetupHandler _Handler = _SetupHandlers[i];
                _Handler.SetNext(_SetupHandlers[i + 1]);
            }
            
            // Initiate the chain invocation.
            _SetupHandlers.First().Handle(_InitializationContext);
            
            Debug.Log("[LOG]: The scene is now prepared.");
        }
        
        #endregion Methods
  
    }

}

