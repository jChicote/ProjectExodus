using ProjectExodus.GameLogic.Input.Gameplay;
using ProjectExodus.GameLogic.Input.UserInterface;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.GameStateManager;
using UnityEngine;

namespace ProjectExodus.Tests.TestHarness.TestGameStateManager
{

    public class Test_GameStateManagerHarness : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public GameStateManager GameStateManager;
        public GameObject SessionUser;
        public GameObject ActivePlayer;

        private IUserInterfaceInputControl m_UserInterfaceInputControl;
        private IGameplayInputControl m_GameplayInputControl;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void ChangeGameState_SettingNextGameState_GameplayStateIsActiveWithCorrectInputControl()
        {
            // Arrange
            
            // Act
            ((IGameStateManager)this.GameStateManager).ChangeGameState(GameState.Gameplay);
            this.m_GameplayInputControl = this.ActivePlayer.GetComponent<IGameplayInputControl>();
            
            // Assert
            if (this.m_GameplayInputControl.IsInputControlIsActive())
                Debug.Log("[SUCCESS]: Now in GameplayState with correct Gameplay InputControl");
            else
                Debug.LogWarning("[FAILED]: Gameplay InputControl is not active.");
        }

        public void ChangeGameState_SettingMainMenuState_MainMenuStateIsActiveWithCorrectInputControl()
        {
            // Arrange
            this.m_UserInterfaceInputControl = this.SessionUser.GetComponent<IUserInterfaceInputControl>();
            
            // Act
            ((IGameStateManager)this.GameStateManager).ChangeGameState(GameState.MainMenu);

            IGameplayInputControl _InputControl =
                this.ActivePlayer.GetComponent<IGameplayInputControl>();
            Object.Destroy(_InputControl as PausableMonoBehavior);
            
            // Assert
            if (this.m_UserInterfaceInputControl.IsInputControlIsActive())
                Debug.Log("[SUCCESS]: Now in MainMenu with correct UserInterface InputControl");
            else
                Debug.LogWarning("[FAILED]: UserInterface InputControl is not active.");
        }

        #endregion Methods
  
    }

}