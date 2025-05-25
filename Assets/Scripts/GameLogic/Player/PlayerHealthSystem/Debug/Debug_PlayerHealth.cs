using ProjectExodus.GameLogic.Player.PlayerHealthSystem;
using ProjectExodus.GameLogic.Player.PlayerProvider;
using UnityEngine;
using SceneManager = ProjectExodus.Management.SceneManager.SceneManager;

namespace ProjectExodus
{

    public class Debug_PlayerHealth : IDebugCommandRegistrater
    {

        #region - - - - - - Methods - - - - - -

        public void RegisterCommand(IDebugCommandSystem debugCommandSystem)
        {
            DebugCommand _EnableInvincibility = new DebugCommand(
                "player_enableinvincibility",
                "Make the active player invincible",
                "player_enableinvincibility",
                this.EnableInvincibility);
            DebugCommand _DisableInvincibility = new DebugCommand(
                "player_disableinvincibility",
                "Disable the active player invincibility",
                "player_disableinvincibility",
                this.DisableInvincibility);
            
            debugCommandSystem.RegisterCommand(_EnableInvincibility);
            debugCommandSystem.RegisterCommand(_DisableInvincibility);
        }

        private void EnableInvincibility()
        {
            // TODO: Will need validation for which scene its run in.
            
            IPlayerProvider _PlayerProvider = SceneManager.Instance.SceneController.PlayerProvider;
            GameObject _PlayerObject = _PlayerProvider.GetActivePlayer();
            IPlayerHealthSystem _PlayerHealthSystem = _PlayerObject.GetComponent<IPlayerHealthSystem>();
            _PlayerHealthSystem.MakeInvincible(true);
        }

        private void DisableInvincibility()
        {
            // TODO: Will need validation for which scene its run in.
            
            IPlayerProvider _PlayerProvider = SceneManager.Instance.SceneController.PlayerProvider;
            GameObject _PlayerObject = _PlayerProvider.GetActivePlayer();
            IPlayerHealthSystem _PlayerHealthSystem = _PlayerObject.GetComponent<IPlayerHealthSystem>();
            _PlayerHealthSystem.MakeInvincible(false);
        }

        #endregion Methods
  
    }

}