using System.Linq;
using ProjectExodus.Common.Services;
using UnityEngine;

namespace ProjectExodus
{

    public class Debug_SpawnZetoFighter : IDebugCommandRegistrater
    {

        #region - - - - - - Methods - - - - - -

        public void RegisterCommand(IDebugCommandSystem debugCommandSystem)
        {
            DebugCommand _SpawnSingleZetoFighter = new DebugCommand(
                "spawn_zetofighter",
                "Spawn single Zeto Fighter enemy",
                "spawn_zetofighter",
                this.SpawnSingleZetoFighter);
            
            debugCommandSystem.RegisterCommand(_SpawnSingleZetoFighter);
        }

        private void SpawnSingleZetoFighter()
        {
            EnemyAssetObject _ZetoFighterAsset = EnemyManager.Instance.EnemySettings.Enemies
                .Single(eao => eao.Name == EnemyType.ZetoFighter);
            
            GameObject _SpawnedEnemy = Object.Instantiate(
                _ZetoFighterAsset.SpawnTemplate, 
                this.GenerateRandomPositionWithinView(),
                Quaternion.identity);
            ICommand _CommandInitializer = _SpawnedEnemy.GetComponent<ICommand>();
            _CommandInitializer.Execute();
        }

        // TODO: Please move this to its own class and possibly as an extension.
        private Vector2 GenerateRandomPositionWithinView()
        {
            Camera _MainCamera = Camera.main;
            float _Width = 10f;
            float _Height = 10f;

            Vector2 _RandomPosition = new(
                x: Random.Range(_MainCamera.transform.position.x - _Width / 2,
                    _MainCamera.transform.position.y + _Width / 2),
                y: Random.Range(_MainCamera.transform.position.y - _Height / 2,
                    _MainCamera.transform.position.y + _Height / 2));
            return _RandomPosition;
        }

        #endregion Methods
  
    }

}