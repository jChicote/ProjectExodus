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
        new Debug_DisplayTargetingSystemInfoCommand().RegisterCommand(this.m_DebugManager);
    }

    #endregion Methods
  
}
