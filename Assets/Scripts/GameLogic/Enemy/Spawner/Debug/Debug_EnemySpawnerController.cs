public class Debug_EnemySpawnerController : IDebugCommandRegistrater
{

    #region - - - - - - Methods - - - - - -

    public void RegisterCommand(IDebugCommandSystem debugCommandSystem)
    {
        DebugCommand _SpawnSmallEasyGroup = new DebugCommand(
            "spawn_zetodrone",
            "Spawn single Zeto Drone enemy",
            "spawn_zetodrone",
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

    #endregion Methods
}
