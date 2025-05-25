using System;

public class DebugCommand : DebugCommandBase
{

    #region - - - - - - Fields - - - - - -

    private Action m_Command;

    #endregion Fields
  
    #region - - - - - - Constructors - - - - - -

    public DebugCommand(
        string commandID,
        string commandDescription,
        string commandFormat,
        Action command)
        : base(commandID, commandDescription, commandFormat)
        => this.m_Command = command;

    #endregion Constructors
  
    #region - - - - - - Methods - - - - - -

    public void Invoke()
        => this.m_Command?.Invoke();

    #endregion Methods
  
}
