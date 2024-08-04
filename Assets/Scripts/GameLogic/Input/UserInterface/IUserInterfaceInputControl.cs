using UnityEngine.InputSystem;

namespace ProjectExodus.GameLogic.Input.UserInterface
{

    public interface IUserInterfaceInputControl : IInputControl
    {

        #region - - - - - - Events - - - - - -

        void OnUnPause(InputAction.CallbackContext callback);

        #endregion Events

    }

}