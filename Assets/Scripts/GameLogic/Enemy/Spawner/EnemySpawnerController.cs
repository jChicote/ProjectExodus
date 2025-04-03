using ProjectExodus;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour, IEnemySpawner
{

    #region - - - - - - Fields - - - - - -

    [Header("Spawner Fields")] 
    public ZetoEnemySpawner m_PawnSpawner;
    public ZetoEnemySpawner m_FighterSpawner;
    public ZetoEnemySpawner m_DroneSpawner;
    public ZetoEnemySpawner m_KnightSpawner;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        EnemySettings _EnemySettings = EnemyManager.Instance.EnemySettings;
        this.m_PawnSpawner.Initialize(new()
        {
            Info = _EnemySettings.PawnSpawnerInfo, 
            DifficultyMultiplier = _EnemySettings.SpawnerDifficulty.Normal //TODO: Change this to be set externally
        });
        this.m_FighterSpawner.Initialize(new()
        {
            Info = _EnemySettings.FighterSpawnerInfo, 
            DifficultyMultiplier = _EnemySettings.SpawnerDifficulty.Normal //TODO: Change this to be set externally
        });
        this.m_DroneSpawner.Initialize(new()
        {
            Info = _EnemySettings.DroneSpawnerInfo, 
            DifficultyMultiplier = _EnemySettings.SpawnerDifficulty.Normal //TODO: Change this to be set externally
        });
        this.m_KnightSpawner.Initialize(new()
        {
            Info = _EnemySettings.KnightSpawnerInfo, 
            DifficultyMultiplier = _EnemySettings.SpawnerDifficulty.Normal //TODO: Change this to be set externally
        });
    }

    #endregion Unity Methods
  
    #region - - - - - - Methods - - - - - -

    public void Spawn(SpawnerRequest spawnRequest, EnemySpawnFilter spawnFilter)
    {
        if ((spawnFilter & EnemySpawnFilter.Pawn) == EnemySpawnFilter.Pawn)
            this.m_PawnSpawner.SpawnEnemies(spawnRequest);
        if ((spawnFilter & EnemySpawnFilter.Fighter) == EnemySpawnFilter.Fighter)
            this.m_FighterSpawner.SpawnEnemies(spawnRequest);
        if ((spawnFilter & EnemySpawnFilter.Drone) == EnemySpawnFilter.Drone)
            this.m_DroneSpawner.SpawnEnemies(spawnRequest);
        if ((spawnFilter & EnemySpawnFilter.Knight) == EnemySpawnFilter.Knight)
            this.m_KnightSpawner.SpawnEnemies(spawnRequest);
    }

    #endregion Methods
  
}
