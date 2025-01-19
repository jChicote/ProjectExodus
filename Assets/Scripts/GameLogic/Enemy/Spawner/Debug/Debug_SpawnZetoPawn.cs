using System.Linq;
using ProjectExodus.Common.Services;
using UnityEngine;

namespace ProjectExodus
{

    public class Debug_SpawnZetoPawn : IDebugCommandRegistrater
    {

        #region - - - - - - Methods - - - - - -

        public void RegisterCommand(IDebugCommandSystem debugCommandSystem)
        {
            DebugCommand _SpawnSingleZetoPawn = new DebugCommand(
                "spawn_zetopawn",
                "Spawn single Zeto Pawn enemy",
                "spawn_zetopawn",
                this.SpawnSingleZetoPawn);
            
            debugCommandSystem.RegisterCommand(_SpawnSingleZetoPawn);
        }

        private void SpawnSingleZetoPawn()
        {
            Camera _MainCamera = Camera.main;
            EnemyAssetObject _ZetoPawnAsset = EnemyManager.Instance.EnemySettings.Enemies
                .Single(eao => eao.Name == EnemyType.ZetoPawn);
            
            GameObject _SpawnedEnemy = Object.Instantiate(
                _ZetoPawnAsset.SpawnTemplate, 
                new Vector3(_MainCamera.transform.position.x + 6, _MainCamera.transform.position.y - 6, 0), 
                Quaternion.identity);
            ICommand _CommandInitializer = _SpawnedEnemy.GetComponent<ICommand>();
            _CommandInitializer.Execute();
        }

        #endregion Methods
  
    }

}