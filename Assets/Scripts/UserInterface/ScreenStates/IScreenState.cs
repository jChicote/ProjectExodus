namespace ProjectExodus.UserInterface.ScreenStates
{

    /// <remarks>
    /// Usage through implementations, represent the state lifecycle of the UI screen.
    /// </remarks>
    public interface IScreenState
    {

        #region - - - - - - Methods - - - - - -

        void StartState();

        void EndState();

        #endregion Methods

    }

}