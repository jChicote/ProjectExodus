using ProjectExodus.Common.Services;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZetoEnemySpawner_New : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    [SerializeField] private GameObject m_EnemyTemplate;
    [SerializeField] private Transform m_SpawnPoint;

    private bool m_IsSpawnerActive;

    #endregion Fields

    #region - - - - - - Methods - - - - - -

    public void SpawnEnemy(SpawnerRequest spawnRequest)
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
    
    #endregion Methods
  
}

public class SpawnerRequest
{

    #region - - - - - - Properties - - - - - -
    
    public Vector2 SpawnCenterPosition { get; set; }

    public float SpawnRadius { get; set; }
    

    #endregion Properties
  
}
