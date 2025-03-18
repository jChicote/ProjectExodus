using System;
using ProjectExodus.GameLogic.Pause.PauseController;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.StateManagement.ScreenStates;
using ProjectExodus.UserInterface.GameplayHUD;
using ProjectExodus.UserInterface.OptionsMenu;
using ProjectExodus.UserInterface.PauseScreen;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectExodus.UserInterface.Controllers
{

    public class GameplayMenuInterfacesController : MonoBehaviour, IUserInterfaceController
    {

        #region - - - - - - Fields - - - - - -

        [Header("Switchable Screens")]
        [SerializeField] private GameObject m_LoadingScreen;
        [SerializeField] private GameObject m_GameplayHUDScreen;
        [SerializeField] private GameObject m_PauseScreen;
        [SerializeField] private GameObject m_OptionsScreen;
        
        [Header("Sub screens")]
        [SerializeField] private GameObject m_TargetingHUDScreen;
        [SerializeField] private GameObject m_WeaponLocatorHUDScreen;
        
        [Header("Scene-Persistence Views")]
        [SerializeField] private WeaponTargetWeaponTrackingHUDController weaponTargetWeaponTrackingHUDController;

        private GameplaySceneGUIControllers m_GUIControllers;

        private IScreenState m_LoadingBarScreenState;
        private IScreenState m_GameplayHudScreenState;
        private IScreenState m_PauseScreenState;
        private IScreenState m_OptionsScreenState;
        
        private IScreenState m_CurrentScreenState;
        private IScreenState m_PreviousScreenState;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        void IUserInterfaceController.InitialiseUserInterfaceController()
        {
            GameManager _GameManager = GameManager.Instance;
            IPauseController _PauseController = _GameManager.SceneManager.GetActiveSceneController().PauseController;
            
            this.m_LoadingBarScreenState = this.m_LoadingScreen.GetComponent<IScreenState>();
            this.m_GameplayHudScreenState = this.m_GameplayHUDScreen.GetComponent<IScreenState>();
            this.m_PauseScreenState = this.m_PauseScreen.GetComponent<IScreenState>();
            this.m_OptionsScreenState = this.m_OptionsScreen.GetComponent<IScreenState>();
            
            this.m_GameplayHudScreenState.Initialize();
            this.m_PauseScreenState.Initialize();
            this.m_OptionsScreenState.Initialize();
            
            // Initialize screens
            IGameplayHUDController _GamplayHUDController =
                this.m_GameplayHUDScreen.GetComponent<IGameplayHUDController>();
            _GamplayHUDController.Initialize(_PauseController, this);
            this.m_PauseScreen.GetComponent<IPauseScreenPresenter>().Initialize(
                _GameManager.GameStateManager, 
                _PauseController, 
                this);
            this.m_OptionsScreen.GetComponent<IOptionsMenuController>()
                .InitialiseOptionsMenu(
                    _GameManager.DataContext,
                    _GameManager.GameSettings.GameOptionsModel,
                    _GameManager.GameOptionsFacade,
                    _GameManager.Mapper,
                    this);
            this.m_WeaponLocatorHUDScreen.GetComponent<IInitialize>().Initialize();

            this.m_GUIControllers = new GameplaySceneGUIControllers(
                _GamplayHUDController,
                this.m_TargetingHUDScreen.GetComponent<IPlayerTargetingHUDController>(),
                this.m_TargetingHUDScreen.GetComponent<ITractorBeamTrackingHUDController>(),
                this.m_TargetingHUDScreen.GetComponent<IWeaponTrackingHUDController>());
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
                UIScreenType.OptionsMenu => this.m_OptionsScreenState,
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
        private IPlayerTargetingHUDController m_PlayerTargetingHUDController;
        private ITractorBeamTrackingHUDController m_TractorBeamTrackingHUDController;
        private IWeaponTrackingHUDController m_WeaponTrackingHUDController;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameplaySceneGUIControllers(
            IGameplayHUDController gameplayHUDController,
            IPlayerTargetingHUDController playerTargetingHUDController,
            ITractorBeamTrackingHUDController tractorBeamTrackingHUDController,
            IWeaponTrackingHUDController weaponTrackingHUDController)
        {
            this.m_GameplayHUDController =
                gameplayHUDController ?? throw new ArgumentNullException(nameof(gameplayHUDController));
            this.m_PlayerTargetingHUDController =
                playerTargetingHUDController ?? throw new ArgumentNullException(nameof(playerTargetingHUDController));
            this.m_TractorBeamTrackingHUDController =
                tractorBeamTrackingHUDController ?? throw new ArgumentNullException(nameof(tractorBeamTrackingHUDController));
            this.m_WeaponTrackingHUDController =
                weaponTrackingHUDController ?? throw new ArgumentNullException(nameof(weaponTrackingHUDController));
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public IPlayerTargetingHUDController PlayerTargetingHUDController
        {
            get
            {
                if (this.m_PlayerTargetingHUDController != null)
                    return this.m_PlayerTargetingHUDController;

                if (!Object.FindAnyObjectByType<PlayerTargetingHUDController>())
                    throw new NullReferenceException($"[ERROR]: No '{nameof(IPlayerTargetingHUDController)}' has been found");
            
                this.m_PlayerTargetingHUDController = Object.FindFirstObjectByType<PlayerTargetingHUDController>();
                return this.m_PlayerTargetingHUDController;
            }
        }

        public ITractorBeamTrackingHUDController TractorBeamTrackingHUDController
        {
            get
            {
                if (this.m_TractorBeamTrackingHUDController != null)
                    return this.m_TractorBeamTrackingHUDController;

                if (!Object.FindAnyObjectByType<TractorBeamTrackingHUDController>())
                    throw new NullReferenceException($"[ERROR]: No '{nameof(ITractorBeamTrackingHUDController)}' has been found");
            
                this.m_TractorBeamTrackingHUDController = Object.FindFirstObjectByType<TractorBeamTrackingHUDController>();
                return this.m_TractorBeamTrackingHUDController;
            }
        }
        
        public IWeaponTrackingHUDController WeaponTrackingHUDController
        {
            get
            {
                if (this.m_WeaponTrackingHUDController != null)
                    return this.m_WeaponTrackingHUDController;

                if (!Object.FindAnyObjectByType<WeaponTargetWeaponTrackingHUDController>())
                    throw new NullReferenceException($"[ERROR]: No '{nameof(IWeaponTrackingHUDController)}' has been found");
            
                this.m_WeaponTrackingHUDController = Object.FindFirstObjectByType<WeaponTargetWeaponTrackingHUDController>();
                return this.m_WeaponTrackingHUDController;
            }
        }

        #endregion Properties
  
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