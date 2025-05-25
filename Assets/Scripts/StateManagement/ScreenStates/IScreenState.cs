namespace ProjectExodus.StateManagement.ScreenStates
{

    /// <remarks>
    /// Usage through implementations, represent the state lifecycle of the UI screen.
    /// </remarks>
    public interface IScreenState
    {

        #region - - - - - - Methods - - - - - -

        void Initialize();

        void StartState();

        void EndState();

        object GetInterfaceController();

        #endregion Methods

    }

}