using System;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.GameSaveManager;
using ProjectExodus.StateManagement.ScreenStates;
using ProjectExodus.UserInterface.GameSaveSelectionMenu;
using ProjectExodus.UserInterface.MainMenu;
using ProjectExodus.UserInterface.OptionsMenu;
using ProjectExodus.UserInterface.ShipSelectionScreen;
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
        [SerializeField] private GameObject m_ShipSelectionScreen;

        private IScreenState m_LoadingBarScreenState;
        private IScreenState m_GameSaveMenuScreenState;
        private IScreenState m_MainMenuScreenState;
        private IScreenState m_OptionsMenuScreenState;
        private IScreenState m_ShipSelectionScreenState;

        private IScreenState m_CurrentScreenState;
        private IScreenState m_PreviousScreenState;
        
        #endregion Fields

        #region - - - - - - Methods - - - - - -

        void IUserInterfaceController.InitialiseUserInterfaceController()
        {
            GameManager _GameManager = GameManager.Instance;
            GameSaveManager _GameSaveManager = GameSaveManager.Instance;
            IServiceLocator _ServiceLocator = _GameManager.ServiceLocator;

            this.m_LoadingBarScreenState = this.m_LoadingBarScreen.GetComponent<IScreenState>();
            this.m_GameSaveMenuScreenState = this.m_GameSaveMenuScreen.GetComponent<IScreenState>();
            this.m_MainMenuScreenState = this.m_MainMenuScreen.GetComponent<IScreenState>();
            this.m_OptionsMenuScreenState = this.m_OptionsMenuScreen.GetComponent<IScreenState>();
            this.m_ShipSelectionScreenState = this.m_ShipSelectionScreen.GetComponent<IScreenState>();
            
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
            this.m_ShipSelectionScreen.GetComponent<IShipSelectionScreenPresenter>()
                .Initialize(_GameSaveManager.PlayerShips,
                _ServiceLocator.GetService<IShipAssetProvider>());
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
                UIScreenType.ShipSelectionScreen => this.m_ShipSelectionScreenState,
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
            => throw new NotImplementedException();

        #endregion Methods
  
    }

}