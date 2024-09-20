using ProjectExodus.GameLogic.Player.PlayerProvider;
using ProjectExodus.Management.InputManager;
using UnityEngine;

namespace ProjectExodus.Management.Mocks
{

    public class MocksInputManager : InputManager.InputManager
    {

        #region - - - - - - Fields - - - - - -

        public GameObject PlayerProvider;
        public bool PossesPlayerInput;

        #endregion Fields
  
        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Awake()
        {
            Debug.Log($"[IMPORTANT]: You are using the mock object {nameof(MocksInputManager)}");
            
            this.m_PlayerProvider = PlayerProvider.GetComponent<IPlayerProvider>();
            if (this.PossesPlayerInput)
            {
                ((IInputManager)this).PossesGameplayInputControls();
                ((IInputManager)this).EnableActiveInputControl();
            }
        }

        #endregion Unity Lifecycle Methods
  
    }

}