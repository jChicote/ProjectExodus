using ProjectExodus.GameLogic.Common.Health;
using ProjectExodus.GameLogic.Player.PlayerProvider;
using ProjectExodus.Management.SceneManager;
using UnityEngine;

namespace ProjectExodus
{

    public class Debug_KillPlayer : IDebugCommandRegistrater
    {

        #region - - - - - - Methods - - - - - -

        public void RegisterCommand(IDebugCommandSystem debugCommandSystem)
        {
            DebugCommand _KillPlayerCommand = new DebugCommand(
                "player_killplayer",
                "Kill active player with damage.",
                "player_killplayer",
                this.KillPlayer);
            
            debugCommandSystem.RegisterCommand(_KillPlayerCommand);
        }

        private void KillPlayer()
        {
            IPlayerProvider _PlayerProvider = SceneManager.Instance.SceneController.PlayerProvider;
            GameObject _Player = _PlayerProvider.GetActivePlayer();

            IDamageable _PlayerDamageable = _Player.GetComponent<IDamageable>();
            _PlayerDamageable.SendDamage(999);
            _PlayerDamageable.SendDamage(999);
        }

        #endregion Methods
  
    }

}