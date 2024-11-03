using ProjectExodus.UserInterface;
using ProjectExodus.UserInterface.GameplayHUD;
using UnityEngine;

namespace ProjectExodus.StateManagement.ScreenStates
{

    public class GameplayHUDScreenState : MonoBehaviour, IScreenState
    {

        #region - - - - - - Fields - - - - - -

        private IScreenStateController m_ScreenController;
        
        #endregion Fields
  
        #region - - - - - - Initializers - - - - - -

        void IScreenState.Initialize() 
            => this.m_ScreenController = this.GetComponent<IScreenStateController>();

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState() 
            => this.m_ScreenController.ShowScreen();

        void IScreenState.EndState()
            => this.m_ScreenController.HideScreen();

        object IScreenState.GetInterfaceController()
            => this.GetComponent<IGameplayHUDController>();

        #endregion Methods

    }

}