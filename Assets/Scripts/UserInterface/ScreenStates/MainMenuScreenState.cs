﻿using ProjectExodus.UserInterface.MainMenu;

namespace ProjectExodus.UserInterface.ScreenStates
{

    public class MainMenuScreenState : IScreenState
    {

        #region - - - - - - Fields - - - - - -

        private IMainMenuController m_MainMenuController;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public MainMenuScreenState(IMainMenuController mainMenuStateController)
        {
            this.m_MainMenuController = mainMenuStateController;
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState() 
            => this.m_MainMenuController.ShowScreen();

        void IScreenState.EndState() 
            => this.m_MainMenuController.HideScreen();

        #endregion Methods
  
    }

}