using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.Models;
using ProjectExodus.StateManagement.ScreenStates;
using UnityEngine;

namespace ProjectExodus.Management.UserInterfaceScreenStatesManager
{

    public class UserInterfaceScreenStateManager : MonoBehaviour, IUserInterfaceScreenStateManager
    {

        #region - - - - - - Fields - - - - - -

        // UI Screen States
        private MainMenuScreenState m_MainMenuScreenState;
        private OptionsMenuScreenState m_OptionsMenuScreenState;
        private LoadingBarScreenState m_LoadingBarScreenState;
        private GameplayHUDScreenState m_GameplayHUDScreenState;

        private IScreenState m_CurrentScreenState;
        private IScreenState m_PreviousScreenState;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IUserInterfaceScreenStateManager.InitialiseUserInterfaceScreenStatesManager(GameScreens gameScreens)
        {
            this.m_MainMenuScreenState = new MainMenuScreenState(gameScreens.MainMenuController);
            this.m_OptionsMenuScreenState = new OptionsMenuScreenState(gameScreens.OptionsMenuController);
            this.m_LoadingBarScreenState = new LoadingBarScreenState(gameScreens.LoadingScreenController);
            this.m_GameplayHUDScreenState = new GameplayHUDScreenState(gameScreens.GameplayHUDController);
            
            // Default opening game screen
            ((IUserInterfaceScreenStateManager)this).OpenScreen(UIScreenType.MainMenu);
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void IUserInterfaceScreenStateManager.OpenScreen(UIScreenType uiScreenType)
        {
            this.m_PreviousScreenState = this.m_CurrentScreenState;
            this.m_CurrentScreenState?.EndState();
            
            switch (uiScreenType)
            {
                case UIScreenType.MainMenu:
                    this.m_CurrentScreenState = this.m_MainMenuScreenState;
                    break;
                case UIScreenType.OptionsMenu:
                    this.m_CurrentScreenState = this.m_OptionsMenuScreenState;
                    break;
                case UIScreenType.LoadingScreen:
                    this.m_CurrentScreenState = this.m_LoadingBarScreenState;
                    break;
                case UIScreenType.GameplayHUD:
                    this.m_CurrentScreenState = this.m_GameplayHUDScreenState;
                    break;
            }
            
            this.m_CurrentScreenState.StartState();
        }

        void IUserInterfaceScreenStateManager.OpenPreviousScreen()
        {
            this.m_CurrentScreenState?.EndState();
            this.m_CurrentScreenState = this.m_PreviousScreenState;
            this.m_CurrentScreenState.StartState();
        }

        #endregion Methods
  
    }
    
}