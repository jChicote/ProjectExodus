using ProjectExodus;
using ProjectExodus.Debugging;

public class DebugCommandConfigurator
{

    #region - - - - - - Fields - - - - - -

    private DebugManager m_DebugManager;

    #endregion Fields

    #region - - - - - - Initializers - - - - - -

    public DebugCommandConfigurator() 
        => this.m_DebugManager = DebugManager.Instance;

    #endregion Initializers
  
    #region - - - - - - Methods - - - - - -

    public void ConfigureCommands()
    {
        // UI Debuggers
        new Debug_DisplayTargetingSystemInfoCommand().RegisterCommand(this.m_DebugManager);
        
        // Spawning
        new Debug_SpawnZetoPawn().RegisterCommand(this.m_DebugManager);
        new Debug_SpawnZetoFighter().RegisterCommand(this.m_DebugManager);
        new Debug_SpawnZetoDrone().RegisterCommand(this.m_DebugManager);
        
        // Player
        new Debug_RespawnPlayer().RegisterCommand(this.m_DebugManager);
        new Debug_KillPlayer().RegisterCommand(this.m_DebugManager);
    }

    #endregion Methods
  
}
