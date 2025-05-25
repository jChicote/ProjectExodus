using System.Linq;
using MBT;
using ProjectExodus;
using ProjectExodus.Common.Services;
using UnityEngine;

public class ZetoSentinelDroneCommandInitializer : MonoBehaviour, ICommand
{
    
    #region - - - - - - Fields - - - - - -

    [SerializeField, RequiredField] private Blackboard m_Blackboard;

    private EnemyAssetObject m_EnemySpawnData;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    // Must resolve dependencies without a context object, to ensure the spawner does not include
    //  specific logic to setup dependencies for each enemy type.;
    private void Start()
    {
        EnemyManager _EnemyManager = EnemyManager.Instance;
        this.m_EnemySpawnData = _EnemyManager.EnemySettings.Enemies.Single(eao => eao.Name == EnemyType.ZetoSentinelDrone);
        
        this.Execute();
    }

    #endregion Unity Methods
    
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
            
        // Collect and run all generic initializers
        IInitialize[] _Initializers = this.GetComponents<IInitialize>();
        foreach (var _Initializer in _Initializers)
            _Initializer.Initialize();
    }

    public bool CanExecute()
        => true;
}
