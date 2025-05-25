public class DebugCommandBase
{

    #region - - - - - - Fields - - - - - -

    private string m_CommandID;
    private string m_CommandDescription;
    private string m_CommandFormat;

    #endregion Fields

    #region - - - - - - Properties - - - - - -

    public string CommandID => m_CommandID;
    public string CommandDescription => this.m_CommandDescription;
    public string CommandFormat => this.m_CommandFormat;

    #endregion Properties

    #region - - - - - - Constructors - - - - - -

    public DebugCommandBase(string commandID, string commandDescription, string commandFormat)
    {
        this.m_CommandID = commandID;
        this.m_CommandDescription = commandDescription;
        this.m_CommandFormat = commandFormat;
    }

    #endregion Constructors
  
}
