using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.InputManager;
using ProjectExodus.Management.SceneManager;
using ProjectExodus.Management.UserInterfaceScreenStatesManager;
using ProjectExodus.StateManagement.GameStates;
using ProjectExodus.StateManagement.GameStates.GameplayState;
using ProjectExodus.StateManagement.GameStates.MainMenuState;
using UnityEngine;

namespace ProjectExodus.Management.GameStateManager
{

    public class GameStateManager : MonoBehaviour, IGameStateManager
    {

        #region - - - - - - Fields - - - - - -

        // Game States
        private GameplayState m_GameplayState;
        private MainMenuState m_MainMenuState;
        
        private IGameState m_CurrentGameState;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        void IGameStateManager.InitialiseGameStateManager()
        {
            IInputManager _InputManager = GameManager.Instance.InputManager;
            ISceneManager _SceneManager = GameManager.Instance.SceneManager;
            IUserInterfaceScreenStateManager _UserInterfaceScreenStateManager =
                GameManager.Instance.UserInterfaceManager.UserInterfaceScreenStateManager;
            
            this.m_GameplayState = new GameplayState(_InputManager, _SceneManager, _UserInterfaceScreenStateManager);
            this.m_MainMenuState = new MainMenuState(_InputManager, _UserInterfaceScreenStateManager);
            
            // Set the starting game state
            ((IGameStateManager)this).ChangeGameState(GameState.MainMenu);
        }

        void IGameStateManager.ChangeGameState(GameState gameState)
        {
            // Close previous state
            this.m_CurrentGameState?.EndState();
            
            switch (gameState)
            {
                case GameState.MainMenu:
                    this.m_CurrentGameState = this.m_MainMenuState;
                    break;
                case GameState.Gameplay:
                    this.m_CurrentGameState = this.m_GameplayState;
                    break;
                default:
                    Debug.LogError($"The game state: '{gameState.ToString()}' is not found");
                    break;
            }
            
            this.m_CurrentGameState.StartState();
        }

        #endregion Methods
  
    }

}