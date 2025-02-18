using System.Linq;
using MBT;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Enumeration;
using UnityEngine;

namespace ProjectExodus
{

    public class Debug_SpawnZetoKnight : IDebugCommandRegistrater
    {

        #region - - - - - - Methods - - - - - -

        public void RegisterCommand(IDebugCommandSystem debugCommandSystem)
        {
            DebugCommand _SpawnSingleZetoFighter = new DebugCommand(
                "spawn_zetoknight",
                "Spawn single Zeto Knight enemy",
                "spawn_zetoknight",
                this.SpawnSingleZetoKnight);
            
            debugCommandSystem.RegisterCommand(_SpawnSingleZetoFighter);
        }
        
        private void SpawnSingleZetoKnight()
        {
            EnemyAssetObject _ZetoKnightAsset = EnemyManager.Instance.EnemySettings.Enemies
                .Single(eao => eao.Name == EnemyType.ZetoKnight);
            
            GameObject _SpawnedEnemy = Object.Instantiate(
                _ZetoKnightAsset.SpawnTemplate, 
                this.GenerateRandomPositionWithinView(),
                Quaternion.identity);
            ICommand _CommandInitializer = _SpawnedEnemy.GetComponent<ICommand>();
            _CommandInitializer.Execute();
            
            // Temporary set the target transform
            var _SpawnedEnemyBT = _SpawnedEnemy.GetComponentInChildren<Blackboard>();
            var _TransformVariable = _SpawnedEnemyBT.GetVariable<TransformVariable>("PlayerTargetTransform");
            _TransformVariable.Value = Object
                .FindObjectsByType<GameObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .FirstOrDefault(x => x.tag == GameTag.Player)
                ?.transform;
        }

        // TODO: Please move this to its own class and possibly as an extension.
        private Vector2 GenerateRandomPositionWithinView()
        {
            Camera _MainCamera = Camera.main;
            float _Width = 20f;
            float _Height = 20f;

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