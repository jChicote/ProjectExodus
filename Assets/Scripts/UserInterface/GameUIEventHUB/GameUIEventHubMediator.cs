using System;
using System.Collections.Generic;
using UnityEngine;

public interface IUIEventCollection
{

    #region - - - - - - Methods - - - - - -

    void RegisterEvent(string key, Action eventAction);

    void RegisterEvent(string key, Action<object> eventAction);

    #endregion Methods

}

public interface IUIEventMediator
{

    #region - - - - - - Methods - - - - - -

    void Dispatch(string key);

    void Dispatch(string key, object eventObject);

    #endregion Methods

}

public class GameUIEventHubMediator : MonoBehaviour, IUIEventMediator, IUIEventCollection
{

    #region - - - - - - Fields - - - - - -

    public Dictionary<string, Action> GameplayHUDActions;
    public Dictionary<string, Action<object>> GameplayHUDActionsWithParameter;

    #endregion Fields
  
    #region - - - - - - Methods - - - - - -

    public void Dispatch(string key)
        => this.GameplayHUDActions[key].Invoke();

    public void Dispatch(string key, object eventObject)
        => this.GameplayHUDActionsWithParameter[key].Invoke(eventObject);

    void IUIEventCollection.RegisterEvent(string key, Action eventAction)
        => this.GameplayHUDActions.Add(key, eventAction);

    void IUIEventCollection.RegisterEvent(string key, Action<object> eventAction)
        => this.GameplayHUDActionsWithParameter.Add(key, eventAction);

    #endregion Methods

}
