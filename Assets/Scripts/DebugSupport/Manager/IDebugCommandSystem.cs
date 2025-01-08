public interface IDebugCommandSystem
{

    #region - - - - - - Methods - - - - - -

    void RegisterCommand(object command);

    void UnregisterCommand(string id);

    #endregion Methods

}
