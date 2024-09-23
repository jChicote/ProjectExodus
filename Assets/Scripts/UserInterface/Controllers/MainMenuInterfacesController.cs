using System;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.StateManagement.ScreenStates;
using UnityEngine;

namespace ProjectExodus.UserInterface.Controllers
{

    public class MainMenuInterfacesController : MonoBehaviour, IUserInterfaceController
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private LoadingBarScreenState m_LoadingBarScreenState;
        [SerializeField] private MainMenuScreenState m_MainMenuScreenState;
        [SerializeField] private OptionsMenuScreenState m_OptionsMenuScreenState;
        [SerializeField] private GameSaveMenuScreenState m_GameSaveMenuScreenState;

        private IScreenState m_CurrentScreenState;
        private IScreenState m_PreviousScreenState;
        
        #endregion Fields

        #region - - - - - - Methods - - - - - -

        void IUserInterfaceController.InitialiseUserInterfaceController()
        {
            throw new NotImplementedException();
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

        #endregion Methods
  
    }

}