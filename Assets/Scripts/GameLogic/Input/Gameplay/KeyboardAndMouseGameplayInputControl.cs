using System;
using ProjectExodus.Common.Services;
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

        private GameplayInputControlServiceContainer m_ServiceControllers;

        private Vector2 m_ScreenCenter;
        private bool m_IsInputActive;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IGameplayInputControl.InitializeGameplayInputControl(ICommand initializerCommand)
        {
            // Executes custom resolution of input control's dependencies.
            if (initializerCommand.CanExecute())
                initializerCommand.Execute();
        }

        #endregion Initializers

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Awake()
            => this.m_ScreenCenter = new Vector2(
                Screen.width / 2f,
                Screen.height / 2f);

        #endregion Unity Lifecycle Methods
  
        #region - - - - - - Events - - - - - -

        void IGameplayInputControl.OnAttack(InputAction.CallbackContext callback)
        {
            if (this.m_IsPaused || !this.m_IsInputActive)
                return;
            
            this.m_ServiceControllers.PlayerWeaponSystems.ToggleWeaponFire(true);
        }

        void IGameplayInputControl.OnAttackRelease(InputAction.CallbackContext callback)
        {
            if (this.m_IsPaused || !this.m_IsInputActive)
                return;
            
            this.m_ServiceControllers.PlayerWeaponSystems.ToggleWeaponFire(false);
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
            
            // Calculate look direction assuming screen center as origin point
            this.m_ServiceControllers.PlayerMovement.SetLookDirection(
                callback.ReadValue<Vector2>() - this.m_ScreenCenter);
            
            this.m_ServiceControllers.PlayerTargetingSystem.SearchForTarget(callback.ReadValue<Vector2>());
        }

        void IGameplayInputControl.OnMove(InputAction.CallbackContext callback)
        {
            if (this.m_IsPaused || !this.m_IsInputActive)
                return;
            
            this.m_ServiceControllers.PlayerMovement.SetMoveDirection(callback.ReadValue<Vector2>()); // default for now
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

            this.m_ServiceControllers.PlayerMovement.ToggleAfterburn();
        }

        #endregion Events
        
        #region - - - - - - Methods - - - - - -

        void IInputControl.BindInputControls(PlayerInput playerInput)
        {
            playerInput.actions[GameplayInputActionConstants.ATTACK].performed +=
                ((IGameplayInputControl)this).OnAttack;
            playerInput.actions[GameplayInputActionConstants.ATTACK].canceled +=
                ((IGameplayInputControl)this).OnAttackRelease;
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
            playerInput.actions[GameplayInputActionConstants.ATTACK].canceled -=
                ((IGameplayInputControl)this).OnAttackRelease;
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

        void IGameplayInputControl.SetServiceContainer(GameplayInputControlServiceContainer serviceContainer)
            => this.m_ServiceControllers = serviceContainer ?? throw new ArgumentNullException(nameof(serviceContainer));

        #endregion Methods

    }

}
