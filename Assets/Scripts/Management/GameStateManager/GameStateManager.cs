using ProjectExodus.GameLogic.GameStates;
using ProjectExodus.GameLogic.GameStates.GameplayState;
using ProjectExodus.GameLogic.GameStates.MainMenuState;
using ProjectExodus.Management.InputManager;
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
            this.m_GameplayState = new GameplayState(_InputManager);
            this.m_MainMenuState = new MainMenuState(
                                    _InputManager,
                                    GameManager.Instance.UserInterfaceManager.MainMenuController);
            
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

    public enum GameState
    {
        MainMenu,
        Gameplay
    }

}