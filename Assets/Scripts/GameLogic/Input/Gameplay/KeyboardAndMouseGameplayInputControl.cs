using System;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.Management.SceneManager;
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
        private bool m_IsDebugConsoleEnabled;
        private bool m_IsPlayerControllable = true;

        private bool m_CtrlPressed;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IGameplayInputControl.InitializeGameplayInputControl(ICommand initializerCommand)
        {
            // Executes custom resolution of input control's dependencies.
            if (initializerCommand.CanExecute())
                initializerCommand.Execute();
            
            IPlayerObserver _PlayerObserver = SceneManager.Instance.PlayerObserver;
            _PlayerObserver.OnPlayerSpawned.AddListener( _ => this.m_IsPlayerControllable = true);
            _PlayerObserver.OnPlayerDeath.AddListener(() => this.m_IsPlayerControllable = false);
            _PlayerObserver.OnPlayerSpawned.AddListener(this.RebindInputControlToRespawnedPlayer);
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
            if (this.IsInputControlValid() || !this.m_IsPlayerControllable)
                return;
            
            this.m_ServiceControllers.PlayerWeaponSystems?.ToggleWeaponFire(true);
        }

        void IGameplayInputControl.OnAttackRelease(InputAction.CallbackContext callback)
        {
            if (this.IsInputControlValid() || !this.m_IsPlayerControllable)
                return;
            
            this.m_ServiceControllers.PlayerWeaponSystems?.ToggleWeaponFire(false);
        }

        void IGameplayInputControl.OnControlOptionPressed(InputAction.CallbackContext callback)
        {
            if (this.IsInputControlValid() || !this.m_IsPlayerControllable)
                return;

            this.m_CtrlPressed = true;
            this.m_ServiceControllers.PlayerTargetingSystem?.ActivateTargeting();
        }

        void IGameplayInputControl.OnControlOptionReleased(InputAction.CallbackContext callback)
        {
            if (this.IsInputControlValid() || !this.m_IsPlayerControllable)
                return;

            this.m_CtrlPressed = false;
            this.m_ServiceControllers.PlayerTargetingSystem?.DeactivateTargeting();
        }

        void IGameplayInputControl.OnInteract(InputAction.CallbackContext callbackContext)
        {
            if (this.IsInputControlValid())
                return;

            Debug.LogWarning("[NOT IMPLEMENTED] >> No implemented behavior for OnInteract.");
        }

        void IGameplayInputControl.OnLook(InputAction.CallbackContext callback)
        {
            if (this.IsInputControlValid() || !this.m_IsPlayerControllable)
                return;
            
            // Calculate look direction assuming screen center as origin point
            this.m_ServiceControllers.PlayerMovement.SetLookDirection(
                callback.ReadValue<Vector2>() - this.m_ScreenCenter);
            
            this.m_ServiceControllers.PlayerTargetingSystem?.SearchForTarget(callback.ReadValue<Vector2>());
        }

        void IGameplayInputControl.OnMove(InputAction.CallbackContext callback)
        {
            if (this.IsInputControlValid() || !this.m_IsPlayerControllable)
                return;
            
            this.m_ServiceControllers.PlayerMovement.SetMoveDirection(callback.ReadValue<Vector2>()); // default for now
        }

        void IGameplayInputControl.OnPause(InputAction.CallbackContext callback)
        {
            if (this.IsInputControlValid())
                return;

            Debug.LogWarning("[NOT IMPLEMENTED] >> No implemented behavior for OnPause.");
        }

        void IGameplayInputControl.OnSecondaryAction(InputAction.CallbackContext callback)
        {
            if (this.IsInputControlValid() || !this.m_IsPlayerControllable)
                return;
            
            if (this.m_CtrlPressed)
                this.m_ServiceControllers.PlayerTargetingSystem?.ConfirmTargetLock();
        }

        void IGameplayInputControl.OnSprint(InputAction.CallbackContext callbackContext)
        {
            if (this.IsInputControlValid() || !this.m_IsPlayerControllable)
                return;

            this.m_ServiceControllers.PlayerMovement?.ToggleAfterburn();
        }
        
        // -----------------------------------------------------
        // Debug related Events
        // -----------------------------------------------------

        void IGameplayInputControl.OnDebugConsole(InputAction.CallbackContext context)
        {
            if (!this.m_IsInputActive)
                return;
            
            this.m_ServiceControllers.DebugHandler.ToggleDebugMenu();
            this.m_IsDebugConsoleEnabled = !this.m_IsDebugConsoleEnabled;
        }

        void IGameplayInputControl.OnSubmitCommand(InputAction.CallbackContext context)
        {
            if (!this.m_IsInputActive)
                return;
            
            this.m_ServiceControllers.DebugHandler.SubmitDebugCommand();
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

            playerInput.actions[GameplayInputActionConstants.SECONDARY].performed += 
                ((IGameplayInputControl)this).OnSecondaryAction;
            playerInput.actions[GameplayInputActionConstants.CONTROL_OPTION].performed += 
                ((IGameplayInputControl)this).OnControlOptionPressed;
            playerInput.actions[GameplayInputActionConstants.CONTROL_OPTION].canceled +=
                ((IGameplayInputControl)this).OnControlOptionReleased;
            
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
            
            // Debug
            playerInput.actions[GameplayInputActionConstants.DEBUGCONSOLE].performed +=
                ((IGameplayInputControl)this).OnDebugConsole;
            playerInput.actions[GameplayInputActionConstants.SUBMITCOMMAND].performed +=
                ((IGameplayInputControl)this).OnSubmitCommand;

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
            
            playerInput.actions[GameplayInputActionConstants.SECONDARY].performed -= 
                ((IGameplayInputControl)this).OnSecondaryAction;
            playerInput.actions[GameplayInputActionConstants.CONTROL_OPTION].performed -= 
                ((IGameplayInputControl)this).OnControlOptionPressed;
            playerInput.actions[GameplayInputActionConstants.CONTROL_OPTION].canceled -=
                ((IGameplayInputControl)this).OnControlOptionReleased;
            
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
            
            // Debug
            playerInput.actions[GameplayInputActionConstants.DEBUGCONSOLE].performed -=
                ((IGameplayInputControl)this).OnDebugConsole;
            playerInput.actions[GameplayInputActionConstants.SUBMITCOMMAND].performed -=
                ((IGameplayInputControl)this).OnSubmitCommand;
        }

        bool IInputControl.IsInputControlIsActive()
            => this.m_IsInputActive;

        void IInputControl.DisableInputControl()
            => this.m_IsInputActive = false;
        
        void IInputControl.EnableInputControl()
            => this.m_IsInputActive = true;

        void IGameplayInputControl.SetServiceContainer(GameplayInputControlServiceContainer serviceContainer)
            => this.m_ServiceControllers = serviceContainer ?? throw new ArgumentNullException(nameof(serviceContainer));

        private bool IsInputControlValid()
            => this.m_IsPaused || !this.m_IsInputActive || this.m_IsDebugConsoleEnabled;

        private void RebindInputControlToRespawnedPlayer(GameObject player)
        {
            this.m_IsPlayerControllable = false;
            this.m_ServiceControllers.SetNewPlayerComponents(player);
            this.m_IsPlayerControllable = true;
        }

        #endregion Methods

    }

}
