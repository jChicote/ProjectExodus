using ProjectExodus;
using ProjectExodus.Common.Services;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZetoEnemySpawner : MonoBehaviour, IInitialize<ZetoSpawnerInitializerData>
{

    #region - - - - - - Fields - - - - - -

    [SerializeField] private GameObject m_EnemyTemplate;
    private float m_DifficultyMultiplier;
    private SpawnerInfo m_SpawnerInfo;
    
    private bool m_IsSpawnerActive;

    #endregion Fields

    #region - - - - - - Initializers - - - - - -

    public void Initialize(ZetoSpawnerInitializerData initializerData)
    {
        this.m_DifficultyMultiplier = initializerData.DifficultyMultiplier;
        this.m_SpawnerInfo = initializerData.Info;
    }

    #endregion Initializers

    #region - - - - - - Methods - - - - - -

    public void SpawnEnemies(SpawnerRequest spawnRequest)
    {
        int _MinSpawnCount = (int)(spawnRequest.SpawnCount * this.m_DifficultyMultiplier 
                                                           * this.m_SpawnerInfo.SpawnRatio 
                                                           * (1 - this.m_SpawnerInfo.MinDeviation));
        int _MaxSpawnCount = (int)(spawnRequest.SpawnCount * this.m_DifficultyMultiplier
                                                           * this.m_SpawnerInfo.SpawnRatio
                                                           * (1 + this.m_SpawnerInfo.MaxDeviation));

        for (int i = 0; i < Random.Range(_MinSpawnCount, _MaxSpawnCount); i++)
        {
            Vector2 _RandomizedSpawnPoint = new Vector2(
                Random.Range(
                    spawnRequest.SpawnCenterPosition.x - spawnRequest.SpawnRadius, 
                    this.transform.position.x + spawnRequest.SpawnRadius),
                Random.Range(
                    this.transform.position.y - spawnRequest.SpawnRadius, 
                    this.transform.position.y + spawnRequest.SpawnRadius));
                
            GameObject _SpawnedEnemy = Instantiate(this.m_EnemyTemplate, _RandomizedSpawnPoint, Quaternion.identity);
            ICommand _CommandInitializer = _SpawnedEnemy.GetComponent<ICommand>();
            _CommandInitializer.Execute();
        }
    }
    
    #endregion Methods
  
}

public class ZetoSpawnerInitializerData
{

    #region - - - - - - Properties - - - - - -
    
    public float DifficultyMultiplier { get; set; }

    public SpawnerInfo Info { get; set; }

    #endregion Properties
  
}

public class SpawnerRequest
{

    #region - - - - - - Properties - - - - - -
    
    public Vector2 SpawnCenterPosition { get; set; }

    public int SpawnCount { get; set; }
    
    public Vector2 SpawnDirection { get; set; }

    public float SpawnRadius { get; set; }
    

    #endregion Properties
  
}
