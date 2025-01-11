using System;
using ProjectExodus.GameLogic.Input;
using ProjectExodus.GameLogic.Input.Gameplay;
using ProjectExodus.GameLogic.Input.UserInterface;
using ProjectExodus.GameLogic.Player.PlayerProvider;
using ProjectExodus.Management.SceneManager;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace ProjectExodus.Management.InputManager
{

    /// <summary>
    /// Responsible for coordinating input bindings and input control management for different hardware.
    /// </summary>
    public class InputManager : MonoBehaviour, IInputManager
    {

        #region - - - - - - Fields - - - - - -

        [Header("Target input destinations")]
        [SerializeField] private GameObject m_SessionUser;
        [SerializeField] private PlayerInput m_PlayerInput;
        
        [Header("Schema Control")]
        [SerializeField] private InputControlSchema m_CurrentInputControlSchema = InputControlSchema.KeyboardAndMouse;
        
        protected IPlayerProvider m_PlayerProvider;
        private IInputControl m_CurrentInputControl;
        private IInputControl m_GameplayInputControl;
        private IInputControl m_UserInterfaceInputControl;

        #endregion Fields
  
        #region - - - - - - Unity Methods - - - - - -

        private void Awake()
        {
            InputManager[] _InputManager = Object.FindObjectsByType<InputManager>(FindObjectsSortMode.None);
            if (_InputManager.Length > 1)
                Debug.LogError($"Multiple {nameof(InputManager)}s detected. " +
                               $"Only one {nameof(InputManager)} should exist, unexpected behaviour will occur.");
        }

        #endregion Unity Methods
  
        #region - - - - - - Methods - - - - - -

        void IInputManager.InitialiseInputManager()
        {
            // Note: Ensure all values exist and references are set. Avoid setting the active input control.
            ((IInputManager)this).PossesUserInterfaceInputControls();
            
            Debug.Log("InputManager initialised."); // Temp debug only
        }

        void IInputManager.DisableActiveInputControl()
            => this.m_CurrentInputControl?.DisableInputControl();

        void IInputManager.EnableActiveInputControl()
            => this.m_CurrentInputControl?.EnableInputControl();
        
        // --------------------------------------
        // InputControl Possession Behavior
        // --------------------------------------

        void IInputManager.PossesGameplayInputControls()
        {
            // Validate whether controls exist
            if (!this.TryGetScenePlayerProvider() 
                || this.m_PlayerProvider.GetActivePlayer().GetComponent<IGameplayInputControl>() != null)
                return;
            
            GameObject _ActivePlayer = this.m_PlayerProvider.GetActivePlayer();
            switch (this.m_CurrentInputControlSchema)
            {
                case InputControlSchema.KeyboardAndMouse:
                    this.CreateAndPossessGameplayKeyboardAndMouseControls(_ActivePlayer);
                    break;
                case InputControlSchema.Gamepad:
                    Debug.LogError("No gamepad input control exists");
                    break;
            }
            
            this.m_GameplayInputControl.BindInputControls(this.m_PlayerInput);
            this.m_CurrentInputControl = this.m_GameplayInputControl;
        }

        /// <remarks>This should only be invoked during the start of the Game/Application.</remarks>
        void IInputManager.PossesUserInterfaceInputControls()
        {
            // Validate whether controls exist
            if (this.m_SessionUser.GetComponent<IUserInterfaceInputControl>() != null)
                return;
            
            switch (this.m_CurrentInputControlSchema)
            { 
                case InputControlSchema.KeyboardAndMouse:
                    this.m_UserInterfaceInputControl = 
                        this.m_SessionUser
                            .AddComponent<KeyboardAndMouseUserInterfaceInputControl>();
                    break;
                case InputControlSchema.Gamepad:
                    Debug.LogError("No Gamepad input control exists.");
                    break;
            }
            
            this.m_UserInterfaceInputControl.BindInputControls(this.m_PlayerInput);
            this.m_CurrentInputControl = this.m_UserInterfaceInputControl;
        }

        void IInputManager.UnpossesGameplayInputControls() 
            => this.m_GameplayInputControl.UnbindInputControls(this.m_PlayerInput);

        void IInputManager.UnpossesUserInterfaceInputControls()
            => this.m_UserInterfaceInputControl.UnbindInputControls(this.m_PlayerInput);

        // --------------------------------------
        // InputControl Switching Behavior
        // --------------------------------------
        
        void IInputManager.SwitchToGameplayInputControls()
        {
            this.m_PlayerInput.SwitchCurrentActionMap("Gameplay");
            
            this.m_GameplayInputControl?.EnableInputControl();
            this.m_UserInterfaceInputControl?.DisableInputControl();
        }

        void IInputManager.SwitchToUserInterfaceInputControls()
        {
            this.m_PlayerInput.SwitchCurrentActionMap("UI");
            
            this.m_GameplayInputControl?.DisableInputControl();
            this.m_UserInterfaceInputControl?.EnableInputControl();
        }

        private void CreateAndPossessGameplayKeyboardAndMouseControls(GameObject activePlayer)
        {
            IGameplayInputControl _GameplayInputControl = 
                activePlayer.AddComponent<KeyboardAndMouseGameplayInputControl>();
            
            _GameplayInputControl.InitializeGameplayInputControl(
                new GameplayInputControlInitializerCommand(
                    activePlayer,
                    this.m_SessionUser, 
                    _GameplayInputControl));
                    
            this.m_GameplayInputControl = _GameplayInputControl;
        }

        private bool TryGetScenePlayerProvider()
        {
            this.m_PlayerProvider = Object.FindFirstObjectByType<PlayerProvider>(FindObjectsInactive.Exclude);
            return this.m_PlayerProvider != null;
        }

        #endregion Methods
  
    }

    [Serializable]
    public enum InputControlSchema
    {
        KeyboardAndMouse,
        Gamepad
    }

}