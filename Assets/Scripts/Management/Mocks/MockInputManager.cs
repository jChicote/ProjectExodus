using ProjectExodus.Management.InputManager;
using ProjectExodus.Management.SceneManager;
using UnityEngine;

namespace ProjectExodus.Management.Mocks
{

    public class MocksInputManager : InputManager.InputManager
    {

        #region - - - - - - Fields - - - - - -

        public GameObject PlayerProvider;

        #endregion Fields
  
        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start()
        {
            Debug.Log($"[IMPORTANT]: You are using the mock object {nameof(MocksInputManager)}");
            
            this.m_PlayerProvider = PlayerProvider.GetComponent<IPlayerProvider>();

            ((IInputManager)this).PossesGameplayInputControls();
            ((IInputManager)this).EnableActiveInputControl();
        }

        #endregion Unity Lifecycle Methods
  
    }

}