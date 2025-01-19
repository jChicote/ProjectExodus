using System.Linq;
using MBT;
using ProjectExodus.Common.Services;
using UnityEngine;

namespace ProjectExodus
{
    
    public class ZetoPawnCommandInitializer : MonoBehaviour, ICommand
    {

        #region - - - - - - Fields - - - - - -

        [RequiredField] 
        [SerializeField] 
        private Blackboard m_Blackboard;

        private EnemyAssetObject m_EnemySpawnData;

        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        // Must resolve dependencies without a context object, to ensure the spawner does not include
        //  specific logic to setup dependencies for each enemy type.;
        private void Awake()
        {
            EnemyManager _EnemyManager = EnemyManager.Instance;
            this.m_EnemySpawnData = _EnemyManager.EnemySettings.Enemies.Single(eao => eao.Name == EnemyType.ZetoPawn);
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

            // Set Prefab Template values to the blackboard
            this.m_Blackboard.GetVariable<GameObjectVariable>(EnemyHealthSystemKeys.DeathEffect);
        }

        public bool CanExecute()
            => true;

        #endregion Methods

    }

}