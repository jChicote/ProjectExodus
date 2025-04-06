using ProjectExodus;
using ProjectExodus.GameLogic.Player.PlayerProvider;
using UnityEngine;
using SceneManager = ProjectExodus.Management.SceneManager.SceneManager;

public class Debug_EnemySpawnerController : IDebugCommandRegistrater
{

    #region - - - - - - Methods - - - - - -

    public void RegisterCommand(IDebugCommandSystem debugCommandSystem)
    {
        DebugCommand _SpawnSmallEasyGroup = new DebugCommand(
            "spawn_smalleasygroup",
            "Spawn small EASY enemy group",
            "spawn_smalleasygroup",
            this.SpawnSmallEasyGroup);
        DebugCommand _SpawnSmallNormalGroup = new DebugCommand(
            "spawn_smallnormalgroup",
            "Spawn small NORMAL enemy group",
            "spawn_smallnormalgroup",
            this.SpawnSmallNormalGroup);
        DebugCommand _SpawnSmallHardGroup = new DebugCommand(
            "spawn_smallhardgroup",
            "Spawn small HARD enemy group",
            "spawn_smallhardgroup",
            this.SpawnSmallHardGroup);
        DebugCommand _SpawnLargeEasyGroup = new DebugCommand(
            "spawn_largeeasygroup",
            "Spawn small EASY enemy group",
            "spawn_largeeasygroup",
            this.SpawnLargeEasyGroup);
        DebugCommand _SpawnLargeNormalGroup = new DebugCommand(
            "spawn_largenormalgroup",
            "Spawn small NORMAL enemy group",
            "spawn_largenormalgroup",
            this.SpawnLargeNormalGroup);
        DebugCommand _SpawnLargeHardGroup = new DebugCommand(
            "spawn_largehardgroup",
            "Spawn small HARD enemy group",
            "spawn_largehardgroup",
            this.SpawnLargeHardGroup);
            
        debugCommandSystem.RegisterCommand(_SpawnSmallEasyGroup);
        debugCommandSystem.RegisterCommand(_SpawnSmallNormalGroup);
        debugCommandSystem.RegisterCommand(_SpawnSmallHardGroup);
        debugCommandSystem.RegisterCommand(_SpawnLargeEasyGroup);
        debugCommandSystem.RegisterCommand(_SpawnLargeNormalGroup);
        debugCommandSystem.RegisterCommand(_SpawnLargeHardGroup);
    }

    private void SpawnSmallEasyGroup()
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

    private void SpawnSmallNormalGroup()
    {
        Vector2 _SpawnCenterPosition = this.GenerateRandomPositionWithinView();
        // TODO: DIfficulty is set to hard by default
        this.SendSpawnRequest(
            new SpawnerRequest()
            {
                SpawnCenterPosition = _SpawnCenterPosition,
                SpawnCount = 5,
                SpawnDirection = this.GetPlayerDirection(_SpawnCenterPosition),
                SpawnRadius = 5f
            });

        Debug.Log("Spawned small normal group");
    }

    private void SpawnSmallHardGroup()
    {
        Vector2 _SpawnCenterPosition = this.GenerateRandomPositionWithinView();
        // TODO: DIfficulty is set to hard by default
        this.SendSpawnRequest(
            new SpawnerRequest()
            {
                SpawnCenterPosition = _SpawnCenterPosition,
                SpawnCount = 5,
                SpawnDirection = this.GetPlayerDirection(_SpawnCenterPosition),
                SpawnRadius = 10f
            });

        Debug.Log("Spawned small normal group");
    } 

    private void SpawnLargeEasyGroup()
    {
        Vector2 _SpawnCenterPosition = this.GenerateRandomPositionWithinView();
        // TODO: DIfficulty is set to hard by default
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

    private void SpawnLargeNormalGroup()
    {
        Vector2 _SpawnCenterPosition = this.GenerateRandomPositionWithinView();
        // TODO: DIfficulty is set to hard by default
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

    private void SpawnLargeHardGroup()
    {
        Vector2 _SpawnCenterPosition = this.GenerateRandomPositionWithinView();
        // TODO: DIfficulty is set to hard by default
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
