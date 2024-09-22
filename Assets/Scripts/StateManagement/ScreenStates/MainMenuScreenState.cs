using System;
using ProjectExodus.UserInterface.MainMenu;
using UnityEngine;

namespace ProjectExodus.StateManagement.ScreenStates
{

    public class MainMenuScreenState : MonoBehaviour, IScreenState
    {

        #region - - - - - - Fields - - - - - -

        private IMainMenuController m_MainMenuController;

        #endregion Fields

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start()
            => this.m_MainMenuController = this.GetComponent<IMainMenuController>();

        #endregion Unity Lifecycle Methods
        //
        // #region - - - - - - Constructors - - - - - -
        //
        // public MainMenuScreenState(IMainMenuController mainMenuStateController)
        //     => this.m_MainMenuController = mainMenuStateController ??
        //                                     throw new ArgumentNullException(nameof(mainMenuStateController));
        //
        // #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState() 
            => this.m_MainMenuController.ShowScreen();

        void IScreenState.EndState() 
            => this.m_MainMenuController.HideScreen();

        #endregion Methods
  
    }

}