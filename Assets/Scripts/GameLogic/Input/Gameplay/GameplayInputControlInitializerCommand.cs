using System;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Player;
using UnityEngine;

namespace ProjectExodus.GameLogic.Input.Gameplay
{

    public class GameplayInputControlInitializerCommand : ICommand
    {

        #region - - - - - - Fields - - - - - -

        private readonly GameObject m_ActivePlayer;
        private readonly GameObject m_SessionUser;
        private readonly IGameplayInputControl m_GameplayInputControl;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public GameplayInputControlInitializerCommand(
            GameObject activePlayer, 
            GameObject sessionUser, 
            IGameplayInputControl gameplayInputControl)
        {
            this.m_ActivePlayer = activePlayer ?? throw new ArgumentNullException(nameof(activePlayer));
            this.m_SessionUser = sessionUser ?? throw new ArgumentNullException(nameof(sessionUser));
            this.m_GameplayInputControl =
                gameplayInputControl ?? throw new ArgumentNullException(nameof(gameplayInputControl));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void ICommand.Execute()
        {
            GameplayInputControlServiceContainer _ServiceContainer = new GameplayInputControlServiceContainer();
            _ServiceContainer.PlayerMovement = this.m_ActivePlayer.GetComponent<IPlayerMovement>();
            
            this.m_GameplayInputControl.SetServiceContainer(_ServiceContainer);
        }

        bool ICommand.CanExecute() => true; // No validation currently needed.

        #endregion Methods
  
    }

}