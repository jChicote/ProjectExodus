using System;
using ProjectExodus.Management.InputManager;
using UnityEngine;

namespace ProjectExodus.Management.GameStateManager
{

    public class GameStateManager : MonoBehaviour, IGameStateManager
    {

        #region - - - - - - Fields - - - - - -

        private IInputManager m_InputManager;
        
        private GameState m_CurrentGameState;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        GameState IGameStateManager.GameState
            => this.m_CurrentGameState;

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        void IGameStateManager.InitialiseGameStateManager()
        {
            this.m_InputManager = GameManager.Instance.InputManager;
            
            // Set the starting game state
            ((IGameStateManager)this).ChangeGameState(GameState.MainMenu);
        }

        void IGameStateManager.ChangeGameState(GameState gameState)
        {
            this.m_CurrentGameState = gameState;

            switch (gameState)
            {
                case GameState.MainMenu:
                    this.RunMainMenuState();
                    break;
                case GameState.Gameplay:
                    this.RunGameplayState();
                    break;
                default:
                    Debug.LogError($"The game state: '{gameState.ToString()}' is not found");
                    break;
            }
        }

        private void RunMainMenuState()
        {
            /* Expected behavior:
             *  - Presents the MainMenu screen
             *  - Ensures that no gameplay related behavior is running
             *  - Switches the acting input to 'UI'
             */
            
            this.m_InputManager.SwitchToUserInterfaceInputControls();
        }

        private void RunGameplayState()
        {
            /* Expected behavior:
             *  - Presents the gameplay related UI screens
             *  - Switches behavior to run game
             *  - Switches the acting input to 'Gameplay'
             */
            
            this.m_InputManager.PossesGameplayInputControls();
            this.m_InputManager.SwitchToGameplayInputControls();
        }

        #endregion Methods
  
    }

    [Serializable]
    public enum GameState
    {
        MainMenu,
        Gameplay
    }

}