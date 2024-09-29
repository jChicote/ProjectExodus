using System;
using ProjectExodus.UserInterface.OptionsMenu;
using UnityEngine;

namespace ProjectExodus.StateManagement.ScreenStates
{

    public class OptionsMenuScreenState : MonoBehaviour, IScreenState
    {

        #region - - - - - - Fields - - - - - -

        private IOptionsMenuController m_OptionsMenuController;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IScreenState.Initialize()
            => this.m_OptionsMenuController = this.GetComponent<IOptionsMenuController>();

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState() 
            => this.m_OptionsMenuController.ShowScreen();

        void IScreenState.EndState() 
            => this.m_OptionsMenuController.HideScreen();

        #endregion Methods
  
    }

}