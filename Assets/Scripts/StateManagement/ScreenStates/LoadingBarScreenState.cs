using ProjectExodus.UserInterface.LoadingScreen;

namespace ProjectExodus.StateManagement.ScreenStates
{

    public class LoadingBarScreenState : IScreenState
    {

        #region - - - - - - Fields - - - - - -

        private readonly ILoadingScreenController m_LoadingScreenController;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public LoadingBarScreenState(ILoadingScreenController loadingScreenController) 
            => this.m_LoadingScreenController = loadingScreenController;

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState() 
            => this.m_LoadingScreenController.ShowScreen();

        void IScreenState.EndState() 
            => this.m_LoadingScreenController.HideScreen();

        #endregion Methods
  
    }

}