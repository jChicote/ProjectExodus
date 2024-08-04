using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectExodus.GameLogic.Input.UserInterface
{

    /// <summary>
    /// Responsible for handling input transformation for Keyboard & Mouse UserInterface inputs.
    /// </summary>
    public class KeyboardAndMouseUserInterfaceInputControl : 
        MonoBehaviour, 
        IUserInterfaceInputControl
    {

        #region - - - - - - Fields - - - - - -

        private bool m_IsInputActive;

        #endregion Fields
  
        #region - - - - - - Events - - - - - -

        void IUserInterfaceInputControl.OnUnPause(InputAction.CallbackContext callback)
        {
            if (!this.m_IsInputActive)
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

        void IInputControl.UnbindInputControls(PlayerInput playerInput)
        {
            playerInput.actions[UserInterfaceInputActionConstants.UNPAUSE].performed 
                -= ((IUserInterfaceInputControl)this).OnUnPause;
        }
        
        bool IInputControl.IsInputControlIsActive()
            => this.m_IsInputActive;

        void IInputControl.DisableInputControl()
            => this.m_IsInputActive = false;

        void IInputControl.EnableInputControl()
            => this.m_IsInputActive = true;

        #endregion Methods

    }

}