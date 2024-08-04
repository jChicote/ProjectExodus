using UnityEngine.InputSystem;

namespace ProjectExodus.GameLogic.Input
{

    public interface IInputControl
    {

        #region - - - - - - Methods - - - - - -

        void BindInputControls(PlayerInput playerInput);

        bool IsInputControlIsActive();

        void DisableInputControl();
        
        void EnableInputControl();

        #endregion Methods

    }

}