using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectExodus.GameLogic.Input.Gameplay
{

    public class KeyboardAndMouseGameplayInputControl : 
        PausableMonoBehavior,
        IGameplayInputControl
    {

        #region - - - - - - Fields - - - - - -

        private bool m_IsInputActive;

        #endregion Fields
  
        #region - - - - - - Events - - - - - -

        void IGameplayInputControl.OnAttack(InputAction.CallbackContext callback)
        {
            if (this.m_IsPaused || !this.m_IsInputActive)
                return;
            
            Debug.LogWarning("[NOT IMPLEMENTED] >> No implemented behavior for OnAttack.");
        }

        void IGameplayInputControl.OnInteract(InputAction.CallbackContext callbackContext)
        {
            if (this.m_IsPaused || !this.m_IsInputActive)
                return;

            Debug.LogWarning("[NOT IMPLEMENTED] >> No implemented behavior for OnInteract.");
        }

        void IGameplayInputControl.OnLook(InputAction.CallbackContext callback)
        {
            if (this.m_IsPaused || !this.m_IsInputActive)
                return;

            Debug.LogWarning("[NOT IMPLEMENTED] >> No implemented behavior for OnLook.");
        }

        void IGameplayInputControl.OnMove(InputAction.CallbackContext callback)
        {
            if (this.m_IsPaused || !this.m_IsInputActive)
                return;

            Debug.LogWarning("[NOT IMPLEMENTED] >> No implemented behavior for OnMove.");
        }

        void IGameplayInputControl.OnPause(InputAction.CallbackContext callback)
        {
            if (this.m_IsPaused || !this.m_IsInputActive)
                return;

            Debug.LogWarning("[NOT IMPLEMENTED] >> No implemented behavior for OnPause.");
        }

        void IGameplayInputControl.OnSprint(InputAction.CallbackContext callbackContext)
        {
            if (this.m_IsPaused || !this.m_IsInputActive)
                return;

            Debug.LogWarning("[NOT IMPLEMENTED] >> No implemented behavior for OnSprint.");
        }

        #endregion Events


        #region - - - - - - Methods - - - - - -

        void IInputControl.BindInputControls(PlayerInput playerInput)
        {
            playerInput.actions[GameplayInputActionConstants.ATTACK].performed +=
                ((IGameplayInputControl)this).OnAttack;
            playerInput.actions[GameplayInputActionConstants.INTERACT].performed +=
                ((IGameplayInputControl)this).OnInteract;
            playerInput.actions[GameplayInputActionConstants.PAUSE].performed += ((IGameplayInputControl)this).OnPause;
            
            // Look
            playerInput.actions[GameplayInputActionConstants.LOOK].performed += ((IGameplayInputControl)this).OnLook;
            playerInput.actions[GameplayInputActionConstants.LOOK].canceled += ((IGameplayInputControl)this).OnLook;

            // Move
            playerInput.actions[GameplayInputActionConstants.MOVE].performed += ((IGameplayInputControl)this).OnMove;
            playerInput.actions[GameplayInputActionConstants.MOVE].canceled += ((IGameplayInputControl)this).OnMove;

            // Sprint
            playerInput.actions[GameplayInputActionConstants.SPRINT].performed +=
                ((IGameplayInputControl)this).OnSprint;
            playerInput.actions[GameplayInputActionConstants.SPRINT].canceled +=
                ((IGameplayInputControl)this).OnSprint;
        }

        void IInputControl.UnbindInputControls(PlayerInput playerInput)
        {
            playerInput.actions[GameplayInputActionConstants.ATTACK].performed -=
                ((IGameplayInputControl)this).OnAttack;
            playerInput.actions[GameplayInputActionConstants.INTERACT].performed -=
                ((IGameplayInputControl)this).OnInteract;
            playerInput.actions[GameplayInputActionConstants.PAUSE].performed -= ((IGameplayInputControl)this).OnPause;
            
            // Look
            playerInput.actions[GameplayInputActionConstants.LOOK].performed -= ((IGameplayInputControl)this).OnLook;
            playerInput.actions[GameplayInputActionConstants.LOOK].canceled -= ((IGameplayInputControl)this).OnLook;

            // Move
            playerInput.actions[GameplayInputActionConstants.MOVE].performed -= ((IGameplayInputControl)this).OnMove;
            playerInput.actions[GameplayInputActionConstants.MOVE].canceled -= ((IGameplayInputControl)this).OnMove;

            // Sprint
            playerInput.actions[GameplayInputActionConstants.SPRINT].performed -=
                ((IGameplayInputControl)this).OnSprint;
            playerInput.actions[GameplayInputActionConstants.SPRINT].canceled -=
                ((IGameplayInputControl)this).OnSprint;
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
