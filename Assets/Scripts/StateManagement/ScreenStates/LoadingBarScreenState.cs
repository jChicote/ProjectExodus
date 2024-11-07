using ProjectExodus.UserInterface.LoadingScreen;
using UnityEngine;

namespace ProjectExodus.StateManagement.ScreenStates
{

    public class LoadingBarScreenState : MonoBehaviour, IScreenState
    {

        #region - - - - - - Fields - - - - - -

        private ILoadingScreenController m_LoadingScreenController;

        #endregion Fields

        void IScreenState.Initialize()
            => this.m_LoadingScreenController = this.GetComponent<ILoadingScreenController>();
  
        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState() 
            => this.m_LoadingScreenController.ShowScreen();

        void IScreenState.EndState() 
            => this.m_LoadingScreenController.HideScreen();
        
        object IScreenState.GetInterfaceController()
            => this.GetComponent<ILoadingScreenController>();

        #endregion Methods
  
    }

}