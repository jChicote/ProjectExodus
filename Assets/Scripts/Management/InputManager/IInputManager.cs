namespace ProjectExodus.Management.InputManager
{

    public interface IInputManager
    {

        #region - - - - - - Methods - - - - - -

        void InitialiseInputManager();

        void PossesGameplayInputControls();

        void PossesUserInterfaceInputControls();

        void SwitchToGameplayInputControls();

        void SwitchToUserInterfaceInputControls();
        
        void UnpossesGameplayInputControls();

        void UnpossesUserInterfaceInputControls();

        #endregion Methods

    }

}