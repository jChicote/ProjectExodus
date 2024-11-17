using System;
using ProjectExodus.UserInterface;
using UnityEngine;

namespace ProjectExodus.StateManagement.ScreenStates
{

    public class ShipSelectionScreenState : MonoBehaviour, IScreenState
    {
        
        #region - - - - - - Fields - - - - - -

        private IScreenStateController m_ScreenStateController;

        #endregion Fields
  
        #region - - - - - - Initializers - - - - - -

        void IScreenState.Initialize() 
            => this.m_ScreenStateController = this.GetComponent<IScreenStateController>();

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState() 
            => this.m_ScreenStateController.ShowScreen();

        void IScreenState.EndState() 
            => this.m_ScreenStateController.HideScreen();

        object IScreenState.GetInterfaceController()
        {
            throw new NotImplementedException();
        }

        #endregion Methods
  
    }

}