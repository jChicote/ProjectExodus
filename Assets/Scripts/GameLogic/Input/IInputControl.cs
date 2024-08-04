using UnityEngine.InputSystem;

namespace ProjectExodus.GameLogic.Input
{

    public interface IInputControl
    {

        #region - - - - - - Methods - - - - - -

        void BindInputControls(PlayerInput playerInput);

        void UnbindInputControls(PlayerInput playerInput);

        bool IsInputControlIsActive();

        void DisableInputControl();
        
        void EnableInputControl();

        #endregion Methods

    }

}