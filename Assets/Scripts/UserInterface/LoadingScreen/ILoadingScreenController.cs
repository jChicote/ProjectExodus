namespace ProjectExodus.UserInterface.LoadingScreen
{

    public interface ILoadingScreenController : IScreenStateController
    {

        #region - - - - - - Methods - - - - - -

        void ResetLoadingScreen();

        void UpdateLoadProgress(float progress);

        #endregion Methods

    }

}