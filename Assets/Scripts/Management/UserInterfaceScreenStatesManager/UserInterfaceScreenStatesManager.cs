﻿using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.Models;
using ProjectExodus.UserInterface.ScreenStates;
using UnityEngine;

namespace ProjectExodus.Management.UserInterfaceScreenStatesManager
{

    public class UserInterfaceScreenStateManager : MonoBehaviour, IUserInterfaceScreenStateManager
    {

        #region - - - - - - Fields - - - - - -

        // UI Screen States
        private MainMenuScreenState m_MainMenuScreenState;

        private IScreenState m_CurrentScreenState;

        #endregion Fields
        
        #region - - - - - - Methods - - - - - -

        void IUserInterfaceScreenStateManager.InitialiseUserInterfaceScreenStatesManager(GameScreens gameScreens)
        {
            this.m_MainMenuScreenState = new MainMenuScreenState(gameScreens.MainMenuController);
            
            // Default opening game screen
            ((IUserInterfaceScreenStateManager)this).OpenMenu(UIScreenType.MainMenu);
        }
        
        void IUserInterfaceScreenStateManager.OpenMenu(UIScreenType uiScreenType)
        {
            this.m_CurrentScreenState?.EndState();
            
            switch (uiScreenType)
            {
                case UIScreenType.GameplayHUD:
                    Debug.LogWarning("[WARNING] - No behavior implemented.");
                    break;
                case UIScreenType.MainMenu:
                    this.m_CurrentScreenState = this.m_MainMenuScreenState;
                    break;
            }
            
            this.m_CurrentScreenState.StartState();
        }

        #endregion Methods
  
    }
    
}