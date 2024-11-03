using ProjectExodus.Management.Enumeration;
using ProjectExodus.StateManagement.ScreenStates;
using ProjectExodus.UserInterface.GameSaveSelectionMenu;
using ProjectExodus.UserInterface.MainMenu;
using ProjectExodus.UserInterface.OptionsMenu;
using UnityEngine;

namespace ProjectExodus.UserInterface.Controllers
{

    public class MainMenuInterfacesController : MonoBehaviour, IUserInterfaceController
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_LoadingBarScreen;
        [SerializeField] private GameObject m_MainMenuScreen;
        [SerializeField] private GameObject m_OptionsMenuScreen;
        [SerializeField] private GameObject m_GameSaveMenuScreen;

        private IScreenState m_LoadingBarScreenState;
        private IScreenState m_MainMenuScreenState;
        private IScreenState m_OptionsMenuScreenState;
        private IScreenState m_GameSaveMenuScreenState;

        private IScreenState m_CurrentScreenState;
        private IScreenState m_PreviousScreenState;
        
        #endregion Fields

        #region - - - - - - Methods - - - - - -

        void IUserInterfaceController.InitialiseUserInterfaceController()
        {
            GameManager _GameManager = GameManager.Instance;

            this.m_LoadingBarScreenState = this.m_LoadingBarScreen.GetComponent<IScreenState>();
            this.m_GameSaveMenuScreenState = this.m_GameSaveMenuScreen.GetComponent<IScreenState>();
            this.m_MainMenuScreenState = this.m_MainMenuScreen.GetComponent<IScreenState>();
            this.m_OptionsMenuScreenState = this.m_OptionsMenuScreen.GetComponent<IScreenState>();
            
            this.m_GameSaveMenuScreen.GetComponent<IGameSaveSelectionMenuController>()
                .InitializeGameSelectionMenuController(
                    _GameManager.DataContext,
                    _GameManager.GameSaveFacade,
                    _GameManager.Mapper);
            this.m_MainMenuScreen.GetComponent<IMainMenuController>()
                .InitialiseMainMenuController(_GameManager.GameStateManager, this);
            this.m_OptionsMenuScreen.GetComponent<IOptionsMenuController>()
                .InitialiseOptionsMenu(
                    _GameManager.DataContext,
                    _GameManager.GameSettings.GameOptionsModel,
                    _GameManager.GameOptionsFacade,
                    _GameManager.Mapper,
                    this);
        }

        void IUserInterfaceController.OpenScreen(UIScreenType uiScreenType)
        {
            var _PreviousScreen = this.m_CurrentScreenState;
            this.m_CurrentScreenState?.EndState();

            this.m_CurrentScreenState = uiScreenType switch
            {
                UIScreenType.LoadingScreen => this.m_LoadingBarScreenState,
                UIScreenType.GameSaveMenu => this.m_GameSaveMenuScreenState,
                UIScreenType.MainMenu => this.m_MainMenuScreenState,
                UIScreenType.OptionsMenu => this.m_OptionsMenuScreenState,
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
        
        bool IUserInterfaceController.TryGetInterfaceController(out object interfaceController)
        {
            interfaceController = this.m_CurrentScreenState?.GetInterfaceController();
            return interfaceController == null;
        }

        #endregion Methods
  
    }

}