using ProjectExodus.GameLogic.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectExodus
{
    
    public class DebugInputControl : 
        MonoBehaviour, 
        IInitialize<DebugInputControlInitializerData>, 
        IInputControl
    {

        #region - - - - - - Fields - - - - - -

        private IDebugHandler m_DebugHandler;
        
        private bool m_IsInputActive;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        public void Initialize(DebugInputControlInitializerData initializationData)
        {
            this.m_DebugHandler = initializationData.DebugHandler;
            this.m_IsInputActive = true;
        }

        #endregion Initializers

        #region - - - - - - Event Handlers - - - - - -

        public void OnDebugConsole(InputAction.CallbackContext context)
        {
            if (!this.m_IsInputActive)
                return;

            this.m_DebugHandler.ToggleDebugMenu();
        }

        public void OnSubmitCommand(InputAction.CallbackContext context)
        {
            if (!this.m_IsInputActive)
                return;
            
            this.m_DebugHandler.SubmitDebugCommand();
        }

        #endregion Event Handlers

        #region - - - - - - Methods - - - - - -

        public void BindInputControls(PlayerInput playerInput)
        {
            playerInput.actions[DebugInputActionConstants.DEBUGCONSOLE].performed +=
                this.OnDebugConsole;
            playerInput.actions[DebugInputActionConstants.SUBMITCOMMAND].performed +=
                this.OnSubmitCommand;
        }

        public void UnbindInputControls(PlayerInput playerInput)
        {
            playerInput.actions[DebugInputActionConstants.DEBUGCONSOLE].performed -=
                this.OnDebugConsole;
            playerInput.actions[DebugInputActionConstants.SUBMITCOMMAND].performed -=
                this.OnSubmitCommand;
        }

        public bool IsInputControlIsActive() 
            => this.m_IsInputActive;

        public void DisableInputControl() 
            => this.m_IsInputActive = false;

        public void EnableInputControl() 
            => this.m_IsInputActive = true;

        #endregion Methods
  
    }

    public class DebugInputControlInitializerData
    {

        #region - - - - - - Properties - - - - - -

        public IDebugHandler DebugHandler { get; set; }

        #endregion Properties
  
    }

}