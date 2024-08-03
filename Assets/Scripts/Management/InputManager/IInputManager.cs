namespace ProjectExodus.Management.InputManager
{

    public interface IInputManager
    {

        #region - - - - - - Methods - - - - - -

        void InitialiseInputManager();

        void PossesUIInputControls();

        void SwitchToGameplayInputControls();

        void SwitchToUserInterfaceInputControls();

        #endregion Methods

    }

}