public interface IUIEventMediator
{

    #region - - - - - - Methods - - - - - -

    void Dispatch(string key);

    void Dispatch(string key, object eventObject);

    #endregion Methods

}