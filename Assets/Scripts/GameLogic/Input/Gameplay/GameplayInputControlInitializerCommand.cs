using System;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Player.Movement;
using ProjectExodus.GameLogic.Player.PlayerTargetingSystem;
using ProjectExodus.GameLogic.Player.Weapons;
using ProjectExodus.Management.InputManager;
using ProjectExodus.Management.SceneManager;
using ProjectExodus.Utility.GameValidation;
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
            GameplayInputControlServiceContainer _ServiceControllers = new GameplayInputControlServiceContainer();
            _ServiceControllers.PlayerMovement = this.m_ActivePlayer.GetComponent<IPlayerMovement>();
            _ServiceControllers.PlayerTargetingSystem = this.m_ActivePlayer.GetComponent<IPlayerTargetingSystem>();
            _ServiceControllers.PlayerWeaponSystems = this.m_ActivePlayer.GetComponent<IPlayerWeaponSystems>();
            
            // DebugHandler exists exclusively from the DebugManager object as it is not related to the player behavior.
            _ServiceControllers.DebugHandler = DebugManager.Instance.gameObject.GetComponent<DebugHandler>();
            this.BindInputPossessionToPlayerRespawnEvents();
            
            this.m_GameplayInputControl.SetServiceContainer(_ServiceControllers);
        }

        bool ICommand.CanExecute()
            => GameValidator.NotNull(this.m_ActivePlayer, nameof(this.m_ActivePlayer))
               && GameValidator.NotNull(this.m_SessionUser, nameof(this.m_SessionUser))
               && GameValidator.NotNull(this.m_GameplayInputControl, nameof(this.m_GameplayInputControl));

        private void BindInputPossessionToPlayerRespawnEvents()
        {
            IInputManager _InputManager = InputManager.Instance;
            IPlayerObserver _PlayerObserver = SceneManager.Instance.PlayerObserver;
            
            _PlayerObserver.OnPlayerDeath.AddListener(_InputManager.UnpossesGameplayInputControls);
            _PlayerObserver.OnPlayerSpawned.AddListener(_ =>
            {
                _InputManager.PossesGameplayInputControls();
                _InputManager.EnableActiveInputControl();
            });
        }

        #endregion Methods

    }

}