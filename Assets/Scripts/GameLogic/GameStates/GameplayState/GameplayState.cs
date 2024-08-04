using ProjectExodus.Management.InputManager;
using UnityEngine;

namespace ProjectExodus.GameLogic.GameStates.GameplayState
{

    public class GameplayState : IGameState
    {

        #region - - - - - - Fields - - - - - -

        private readonly IInputManager m_InputManager;

        #endregion Fields
  
        #region - - - - - - Constructor - - - - - -

        public GameplayState(IInputManager inputManager)
        {
            this.m_InputManager = inputManager;
        }        

        #endregion Constructor
  
        #region - - - - - - Methods - - - - - -

        void IGameState.StartState()
        {
            /* Expected behavior:
             *  - Presents the gameplay related UI screens
             *  - Switches behavior to run game
             *  - Switches the acting input to 'Gameplay'
             */
            
            this.m_InputManager.PossesGameplayInputControls();
            this.m_InputManager.SwitchToGameplayInputControls();
        }

        void IGameState.EndState()
        {
            this.m_InputManager.UnpossesGameplayInputControls();
        }

        #endregion Methods
  
    }

}