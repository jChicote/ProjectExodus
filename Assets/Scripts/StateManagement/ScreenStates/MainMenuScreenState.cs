using ProjectExodus.UserInterface.MainMenu;
using UnityEngine;

namespace ProjectExodus.StateManagement.ScreenStates
{

    public class MainMenuScreenState : MonoBehaviour, IScreenState
    {

        #region - - - - - - Fields - - - - - -

        private IMainMenuController m_MainMenuController;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IScreenState.Initialize()
            => this.m_MainMenuController = this.GetComponent<IMainMenuController>();

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState() 
            => this.m_MainMenuController.ShowScreen();

        void IScreenState.EndState() 
            => this.m_MainMenuController.HideScreen();

        #endregion Methods
  
    }

}