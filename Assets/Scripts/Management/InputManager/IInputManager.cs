namespace ProjectExodus.Management.InputManager
{

    public interface IInputManager
    {

        #region - - - - - - Methods - - - - - -

        void InitialiseInputManager();

        void DisableActiveInputControl();

        void EnableActiveInputControl();

        void PossesGameplayInputControls();

        /// <remarks>This should only be invoked during the start of the Game/Application.</remarks>
        void PossesUserInterfaceInputControls();

        void SwitchToGameplayInputControls();

        void SwitchToUserInterfaceInputControls();
        
        void UnpossesGameplayInputControls();

        void UnpossesUserInterfaceInputControls();

        #endregion Methods

    }

}