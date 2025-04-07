using ProjectExodus;
using UnityEngine;
using SceneManager = ProjectExodus.Management.SceneManager.SceneManager;

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
        SceneDifficulty _Difficulty = SceneManager.Instance.SceneController.Difficulty;
        float _DifficultyMultiplier = _EnemySettings.SpawnerDifficulty.Normal;

        if (_Difficulty == SceneDifficulty.Easy)
            _DifficultyMultiplier = _EnemySettings.SpawnerDifficulty.Easy;
        else if (_Difficulty == SceneDifficulty.Hard)
            _DifficultyMultiplier = _EnemySettings.SpawnerDifficulty.Hard;
        
        this.m_PawnSpawner.Initialize(new()
        {
            Info = _EnemySettings.PawnSpawnerInfo, 
            DifficultyMultiplier = _DifficultyMultiplier
        });
        this.m_FighterSpawner.Initialize(new()
        {
            Info = _EnemySettings.FighterSpawnerInfo, 
            DifficultyMultiplier = _DifficultyMultiplier
        });
        this.m_DroneSpawner.Initialize(new()
        {
            Info = _EnemySettings.DroneSpawnerInfo, 
            DifficultyMultiplier = _DifficultyMultiplier
        });
        this.m_KnightSpawner.Initialize(new()
        {
            Info = _EnemySettings.KnightSpawnerInfo, 
            DifficultyMultiplier = _DifficultyMultiplier
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
