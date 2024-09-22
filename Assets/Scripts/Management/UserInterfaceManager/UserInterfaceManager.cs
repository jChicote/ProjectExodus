using ProjectExodus.Management.Models;
using ProjectExodus.Management.UserInterfaceScreenStatesManager;
using ProjectExodus.UserInterface.GameplayHUD;
using ProjectExodus.UserInterface.GameSaveSelectionMenu;
using ProjectExodus.UserInterface.LoadingScreen;
using ProjectExodus.UserInterface.MainMenu;
using ProjectExodus.UserInterface.OptionsMenu;
using UnityEngine;

namespace ProjectExodus.Management.UserInterfaceManager
{

    /// <summary>
    /// Responsible for high-level coordination of different UI views and encapsulating UI related components.
    /// </summary>
    public class UserInterfaceManager : MonoBehaviour, IUserInterfaceManager
    {

        #region - - - - - - Fields - - - - - -

        [Header("GUI Controllers")]
        [SerializeField] private GameSaveSelectionMenuController m_GameSaveSelectionMenuController;
        [SerializeField] private MainMenuController m_MainMenuController;
        [SerializeField] private OptionsMenuController m_OptionsMenuController;
        [SerializeField] private LoadingScreenController m_LoadingScreenController;
        [SerializeField] private GameplayHUDController m_GameplayHUDController;

        [Header("Sub-Managers")]
        [SerializeField] private UserInterfaceScreenStateManager m_UserInterfaceScreenStateManager;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        ILoadingScreenController IUserInterfaceManager.LoadingScreenController
            => this.m_LoadingScreenController;
        
        IUserInterfaceScreenStateManager IUserInterfaceManager.UserInterfaceScreenStateManager
            => this.m_UserInterfaceScreenStateManager;
        
        #endregion Properties

        #region - - - - - - Initializers - - - - - -

        private void InitialiseUserIterfaces()
        {
            ((IGameSaveSelectionMenuController)this.m_GameSaveSelectionMenuController)
                .InitializeGameSelectionMenuController(
                    GameManager.Instance.DataContext,
                    GameManager.Instance.GameSaveFacade,
                    GameManager.Instance.Mapper);
            ((IMainMenuController)this.m_MainMenuController)
                .InitialiseMainMenuController();
            ((IOptionsMenuController)this.m_OptionsMenuController)
                .InitialiseOptionsMenu(
                    GameManager.Instance.DataContext,
                    GameManager.Instance.GameSettings.GameOptionsModel,
                    GameManager.Instance.GameOptionsFacade,
                    GameManager.Instance.Mapper,
                    this.m_UserInterfaceScreenStateManager);
        }

        #endregion Initializers
  
        #region - - - - - - Unity Methods - - - - - -

        private void Awake()
        {
            UserInterfaceManager[] _UserInterfaceManager = Object.FindObjectsByType<UserInterfaceManager>(FindObjectsSortMode.None);
            if (_UserInterfaceManager.Length > 1)
                Debug.LogError($"Multiple {nameof(UserInterfaceManager)}s detected. " +
                               $"Only one {nameof(UserInterfaceManager)} should exist, unexpected behaviour will occur.");
        }

        #endregion Unity Methods
  
        #region - - - - - - Methods - - - - - -

        void IUserInterfaceManager.InitialiseUserInterfaceManager()
        {
            this.InitialiseUserIterfaces();

            GameScreens _GameScreens = new GameScreens(
                this.m_GameSaveSelectionMenuController,
                this.m_MainMenuController,
                this.m_OptionsMenuController,
                this.m_LoadingScreenController,
                this.m_GameplayHUDController);
            
            ((IUserInterfaceManager)this).UserInterfaceScreenStateManager
                .InitialiseUserInterfaceScreenStatesManager(_GameScreens);
        }

        #endregion Methods
  
    }

}