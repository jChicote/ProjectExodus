using System;

public interface IUIEventCollection
{

    #region - - - - - - Methods - - - - - -

    void RegisterEvent(string key, Action eventAction);

    void RegisterEvent(string key, Action<object> eventAction);

    #endregion Methods

}