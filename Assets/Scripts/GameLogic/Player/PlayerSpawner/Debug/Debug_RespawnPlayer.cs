
using System;
using System.Linq;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Common.Health;
using ProjectExodus.GameLogic.Enumeration;
using ProjectExodus.GameLogic.Infrastructure.DataLoading.LoadCommands;
using ProjectExodus.GameLogic.Player.PlayerSpawner;
using ProjectExodus.Management.GameDataManager;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectExodus.Debugging
{

    public class Debug_RespawnPlayer : IDebugCommandRegistrater
    {

        #region - - - - - - Fields - - - - - -

        private IPlayerSpawner m_PlayerSpawner;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public Debug_RespawnPlayer()
        {
            this.m_PlayerSpawner = Object.FindFirstObjectByType<PlayerSpawner>(FindObjectsInactive.Exclude)
                                   ?? throw new NullReferenceException(nameof(PlayerSpawner));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        public void RegisterCommand(IDebugCommandSystem debugCommandSystem)
        {
            DebugCommand _RespawnPlayer = new DebugCommand(
                "player_respawn",
                "Respawns the player. (Resets the debug console)",
                "player_respawn",
                this.RespawnPlayer);
            
            debugCommandSystem.RegisterCommand(_RespawnPlayer);
        }

        private void RespawnPlayer()
        {
            GameObject _ExistingPlayer = Object
                .FindObjectsByType<GameObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .FirstOrDefault(x => x.tag == GameTag.Player);

            if (_ExistingPlayer)
            {
                IDamageable _PlayerDamageHandle = _ExistingPlayer.GetComponent<IDamageable>();
                _PlayerDamageHandle.SendDamage(999);
                _PlayerDamageHandle.SendDamage(999); // Performed twice to also damage the plating.
            }
            else
                Debug.LogError("No player is found in scene. New player is spawned instead.");
            
            StartupDataContext _StartupData = new StartupDataContext
            {
                Player = GameDataManager.Instance.SelectedPlayer,
                SelectedShip = GameDataManager.Instance.SelectedShip
            };
            
            ShipModel _ShipToSpawn = _StartupData.Player.Ships
                .Single(sm => sm.ID == _StartupData.SelectedShip.ID);

            this.m_PlayerSpawner.SpawnPlayerShip(_ShipToSpawn);
        }

        #endregion Methods
  
    }

}