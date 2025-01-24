using System.Linq;
using MBT;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Enumeration;
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