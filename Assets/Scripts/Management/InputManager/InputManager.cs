using System;
using ProjectExodus.GameLogic.Input;
using ProjectExodus.GameLogic.Input.UserInterface;
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

        [SerializeField] private GameObject m_SessionUser;
        [SerializeField] private InputControlSchema m_CurrentInputControlSchema = InputControlSchema.KeyboardAndMouse;
        [SerializeField] private PlayerInput m_PlayerInput;

        private IInputControl m_UserInterfaceInputControl;
        private IInputControl m_GameplayInputControl;

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
            ((IInputManager)this).PossesUIInputControls();
            
            Debug.Log("InputManager initialised."); // Temp debug only
        }

        void IInputManager.PossesUIInputControls()
        {
            switch (this.m_CurrentInputControlSchema)
            { 
                case InputControlSchema.KeyboardAndMouse:
                    this.m_UserInterfaceInputControl = this.m_SessionUser
                                                        .AddComponent<KeyboardAndMouseUserInterfaceInputControl>();
                    break;
                case InputControlSchema.Gamepad:
                    Debug.LogError("No Gamepad schema exists.");
                    break;
            }
            
            this.m_UserInterfaceInputControl.BindInputControls(this.m_PlayerInput);
        }

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

        #endregion Methods
  
    }

    [Serializable]
    public enum InputControlSchema
    {
        KeyboardAndMouse,
        Gamepad
    }

}