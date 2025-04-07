using ProjectExodus;
using ProjectExodus.GameLogic.Player.PlayerProvider;
using UnityEngine;
using SceneManager = ProjectExodus.Management.SceneManager.SceneManager;

public class Debug_EnemySpawnerController : IDebugCommandRegistrater
{

    #region - - - - - - Methods - - - - - -

    public void RegisterCommand(IDebugCommandSystem debugCommandSystem)
    {
        DebugCommand _SpawnSmallGroup = new DebugCommand(
            "spawn_smallgroup",
            "Spawn SMALL enemy group",
            "spawn_smallgroup",
            this.SpawnSmallGroup);
        DebugCommand _SpawnLargeGroup = new DebugCommand(
            "spawn_largegroup",
            "Spawn LARGE enemy group",
            "spawn_largegroup",
            this.SpawnLargeGroup);
            
        debugCommandSystem.RegisterCommand(_SpawnSmallGroup);
        debugCommandSystem.RegisterCommand(_SpawnLargeGroup);
    }

    private void SpawnSmallGroup()
    {
        Vector2 _SpawnCenterPosition = this.GenerateRandomPositionWithinView();
        this.SendSpawnRequest(
            new SpawnerRequest()
            {
                SpawnCenterPosition = _SpawnCenterPosition,
                SpawnCount = 5,
                SpawnDirection = this.GetPlayerDirection(_SpawnCenterPosition),
                SpawnRadius = 2f
            });

        Debug.Log("Spawned small easy group");
    }

    private void SpawnLargeGroup()
    {
        Vector2 _SpawnCenterPosition = this.GenerateRandomPositionWithinView();
        this.SendSpawnRequest(
            new SpawnerRequest()
            {
                SpawnCenterPosition = _SpawnCenterPosition,
                SpawnCount = 30,
                SpawnDirection = this.GetPlayerDirection(_SpawnCenterPosition),
                SpawnRadius = 15f
            });

        Debug.Log("Spawned small normal group");
    }

    private Vector2 GetPlayerDirection(Vector3 sourcePosition)
    {
        IPlayerProvider _PlayerProvider = SceneManager.Instance.SceneController.PlayerProvider;
        GameObject _ActivePlayer = _PlayerProvider.GetActivePlayer();
        return sourcePosition - _ActivePlayer.transform.position;
    }

    private void SendSpawnRequest(SpawnerRequest spawnerRequest)
    {
        IEnemySpawner _EnemySpawner = EnemyManager.Instance.EnemySpawner;
        
        // TODO: DIfficulty is set to hard by default
        _EnemySpawner.Spawn(
            spawnerRequest,
            EnemySpawnFilter.Drone | EnemySpawnFilter.Fighter | EnemySpawnFilter.Knight | EnemySpawnFilter.Pawn);
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
