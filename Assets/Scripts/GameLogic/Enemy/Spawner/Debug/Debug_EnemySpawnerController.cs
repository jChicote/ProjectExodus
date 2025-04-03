using ProjectExodus;
using UnityEngine;

public class Debug_EnemySpawnerController : IDebugCommandRegistrater
{

    #region - - - - - - Methods - - - - - -

    public void RegisterCommand(IDebugCommandSystem debugCommandSystem)
    {
        DebugCommand _SpawnSmallEasyGroup = new DebugCommand(
            "spawn_smalleasygroup",
            "Spawn small easy enemy group",
            "spawn_smalleasygroup",
            this.SpawnSmallEasyGroup);
        DebugCommand _SpawnSmallNormalGroup = new DebugCommand(
            "spawn_zetodrone",
            "Spawn single Zeto Drone enemy",
            "spawn_zetodrone",
            this.SpawnSmallNormalGroup);
        DebugCommand _SpawnSmallHardGroup = new DebugCommand(
            "spawn_zetodrone",
            "Spawn single Zeto Drone enemy",
            "spawn_zetodrone",
            this.SpawnSmallHardGroup);
        DebugCommand _SpawnLargeEasyGroup = new DebugCommand(
            "spawn_zetodrone",
            "Spawn single Zeto Drone enemy",
            "spawn_zetodrone",
            this.SpawnLargeEasyGroup);
        DebugCommand _SpawnLargeNormalGroup = new DebugCommand(
            "spawn_zetodrone",
            "Spawn single Zeto Drone enemy",
            "spawn_zetodrone",
            this.SpawnLargeNormalGroup);
        DebugCommand _SpawnLargeHardGroup = new DebugCommand(
            "spawn_zetodrone",
            "Spawn single Zeto Drone enemy",
            "spawn_zetodrone",
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
        IEnemySpawner _EnemySpawner = EnemyManager.Instance.EnemySpawner;
        
        // TODO: DIfficulty is set to hard by default
        _EnemySpawner.Spawn(
            new SpawnerRequest()
            {
                SpawnCenterPosition = _SpawnCenterPosition,
                SpawnCount = 10,
                SpawnDirection = Vector2.zero, // TODO: Change to player direction
                SpawnRadius = 5f
            },
            EnemySpawnFilter.Drone & EnemySpawnFilter.Fighter & EnemySpawnFilter.Knight & EnemySpawnFilter.Pawn);
    }

    private void SpawnSmallNormalGroup()
    {
        
    }

    private void SpawnSmallHardGroup()
    {
        
    }

    private void SpawnLargeEasyGroup()
    {
        
    }

    private void SpawnLargeNormalGroup()
    {
        
    }

    private void SpawnLargeHardGroup()
    {
        
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
