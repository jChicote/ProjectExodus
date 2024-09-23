using System;
using ProjectExodus.UserInterface.LoadingScreen;
using UnityEngine;

namespace ProjectExodus.StateManagement.ScreenStates
{

    public class LoadingBarScreenState : MonoBehaviour, IScreenState
    {

        #region - - - - - - Fields - - - - - -

        private ILoadingScreenController m_LoadingScreenController;

        #endregion Fields

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start()
            => this.m_LoadingScreenController = this.GetComponent<ILoadingScreenController>();

        #endregion Unity Lifecycle Methods
        //
        // #region - - - - - - Constructors - - - - - -
        //
        // public LoadingBarScreenState(ILoadingScreenController loadingScreenController)
        //     => this.m_LoadingScreenController = loadingScreenController ??
        //                                             throw new ArgumentNullException(nameof(loadingScreenController));
        //
        // #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState() 
            => this.m_LoadingScreenController.ShowScreen();

        void IScreenState.EndState() 
            => this.m_LoadingScreenController.HideScreen();

        #endregion Methods
  
    }

}