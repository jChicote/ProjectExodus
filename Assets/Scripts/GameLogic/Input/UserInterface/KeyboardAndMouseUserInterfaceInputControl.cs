using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectExodus.GameLogic.Input.UserInterface
{

    /// <summary>
    /// Responsible for handling input transformation for Keyboard & Mouse UserInterface inputs.
    /// </summary>
    public class KeyboardAndMouseUserInterfaceInputControl : 
        PausableMonoBehavior, 
        IInputControl, 
        IUserInterfaceInputControl
    {

        #region - - - - - - Fields - - - - - -

        private bool m_IsInputActive;

        #endregion Fields
  
        #region - - - - - - Events - - - - - -

        void IUserInterfaceInputControl.OnUnPause(InputAction.CallbackContext callback)
        {
            if (this.m_IsPaused || !this.m_IsInputActive)
                return;

            Debug.Log("Unpause menu");
        }
        
        #endregion Events
          
        #region - - - - - - Methods - - - - - -

        void IInputControl.BindInputControls(PlayerInput playerInput)
        {
            playerInput.actions[UserInterfaceInputActionConstants.UNPAUSE].performed 
                += ((IUserInterfaceInputControl)this).OnUnPause;
        }

        void IInputControl.DisableInputControl()
            => this.m_IsInputActive = false;

        void IInputControl.EnableInputControl()
            => this.m_IsInputActive = true;

        #endregion Methods

    }

}