namespace ProjectExodus.UserInterface.LoadingScreen
{

    public interface ILoadingScreenController : IScreenStateController
    {

        #region - - - - - - Methods - - - - - -

        void InitialiseLoadingScreenController();

        void ResetLoadingScreen();

        void UpdateLoadProgress(float progress);

        #endregion Methods

    }

}