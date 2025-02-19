using System.Linq;
using ProjectExodus.Common.Services;
using UnityEngine;

namespace ProjectExodus
{

    public class ZetoKnightCommandInitializer : MonoBehaviour, ICommand
    {

        #region - - - - - - Fields - - - - - -

        private EnemyAssetObject m_EnemySpawnData;

        #endregion Fields
  
        #region - - - - - - Unity Methods - - - - - -

        private void Awake()
        {
            EnemyManager _EnemyManager = EnemyManager.Instance;
            this.m_EnemySpawnData = _EnemyManager.EnemySettings.Enemies.Single(eao => eao.Name == EnemyType.ZetoKnight);
        }

        #endregion Unity Methods
        
        #region - - - - - - Methods - - - - - -

        public void Execute()
        {
            IInitialize<EnemyMovementSystemInitializerData> _MovementSystemInitializer =
                this.GetComponent<IInitialize<EnemyMovementSystemInitializerData>>();
            _MovementSystemInitializer.Initialize(new()
            {
                MovementSpeed = this.m_EnemySpawnData.Speed
            });

            IInitialize<EnemyHealthSystemInitializerData> _HealthSystemInitializer =
                this.GetComponent<IInitialize<EnemyHealthSystemInitializerData>>();
            _HealthSystemInitializer.Initialize(new()
            {
                Health = this.m_EnemySpawnData.Health
            });
            
            IInitialize<EnemyWeaponSystemInitializerData> _WeaponSystemInitializer =
                this.GetComponent<IInitialize<EnemyWeaponSystemInitializerData>>();
            _WeaponSystemInitializer.Initialize(new());
            
            // Collect and run all generic initializers
            IInitialize[] _Initializers = this.GetComponents<IInitialize>();
            foreach (var _Initializer in _Initializers)
                _Initializer.Initialize();
        }

        public bool CanExecute() 
            => true;

        #endregion Methods
        
    }

}