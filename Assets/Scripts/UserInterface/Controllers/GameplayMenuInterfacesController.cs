using System;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.StateManagement.ScreenStates;
using ProjectExodus.UserInterface.GameplayHUD;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectExodus.UserInterface.Controllers
{

    public class GameplayMenuInterfacesController : MonoBehaviour, IUserInterfaceController
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_LoadingScreen;
        [SerializeField] private GameObject m_GameplayHUDScreen;
        [SerializeField] private GameObject m_PauseScreen;

        private GameplaySceneGUIControllers m_GUIControllers;

        private IScreenState m_LoadingBarScreenState;
        private IScreenState m_GameplayHudScreenState;
        private IScreenState m_PauseScreenState;
        
        private IScreenState m_CurrentScreenState;
        private IScreenState m_PreviousScreenState;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        void IUserInterfaceController.InitialiseUserInterfaceController()
        {
            IGameplayHUDController _GamplayHUDController =
                this.m_GameplayHUDScreen.GetComponent<IGameplayHUDController>();
            
            this.m_LoadingBarScreenState = this.m_LoadingScreen.GetComponent<IScreenState>();
            this.m_GameplayHudScreenState = this.m_GameplayHUDScreen.GetComponent<IScreenState>();
            this.m_PauseScreenState = this.m_PauseScreen.GetComponent<IScreenState>();
            
            this.m_GameplayHudScreenState.Initialize();
            _GamplayHUDController.Initialize();
            
            // A bit unusual for the controller's initialisation to occur from retrieving a related components
            // Additionally the initialise method may be thrown around in different areas. Might be a concern.

            this.m_GUIControllers = new GameplaySceneGUIControllers(_GamplayHUDController);
        }

        void IUserInterfaceController.OpenScreen(UIScreenType uiScreenType)
        {
            var _PreviousScreen = this.m_CurrentScreenState;
            this.m_CurrentScreenState?.EndState();

            this.m_CurrentScreenState = uiScreenType switch
            {
                UIScreenType.LoadingScreen => this.m_LoadingBarScreenState,
                UIScreenType.GameplayHUD => this.m_GameplayHudScreenState,
                UIScreenType.PauseScreen => this.m_PauseScreenState,
                _ => this.m_CurrentScreenState
            };

            this.m_CurrentScreenState?.StartState();
            this.m_PreviousScreenState = _PreviousScreen;
        }

        void IUserInterfaceController.OpenPreviousScreen()
        {
            this.m_CurrentScreenState?.EndState();
            this.m_CurrentScreenState = this.m_PreviousScreenState;
            this.m_CurrentScreenState.StartState();
        }

        bool IUserInterfaceController.TryGetGUIControllers(out object _Controllers)
        {
            if (this.m_GUIControllers == null)
            {
                _Controllers = null;
                return false;
            }

            _Controllers = this.m_GUIControllers;
            return true;
        }

        #endregion Methods

    }
    
    public class GameplaySceneGUIControllers
    {

        #region - - - - - - Fields - - - - - -

        private IGameplayHUDController m_GameplayHUDController;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameplaySceneGUIControllers(IGameplayHUDController gameplayHUDController)
        {
            this.m_GameplayHUDController =
                gameplayHUDController ?? throw new ArgumentNullException(nameof(gameplayHUDController));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        public IGameplayHUDController GetGameplayHUDController()
        {
            if (this.m_GameplayHUDController != null)
                return this.m_GameplayHUDController;

            if (!Object.FindAnyObjectByType<GameplayHUDController>())
                throw new NullReferenceException($"[ERROR]: No '{nameof(IGameplayHUDController)}' has been found");
            
            this.m_GameplayHUDController = Object.FindFirstObjectByType<GameplayHUDController>();
            return this.m_GameplayHUDController;
        } 

        #endregion Methods
  
    }

}