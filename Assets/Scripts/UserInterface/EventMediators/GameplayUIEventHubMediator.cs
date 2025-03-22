using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for mediating events for the Gameplay UI.
/// </summary>
/// <remarks>Avoid using this within initializers. Instead, handle from Unity's lifecycle</remarks>
public class GameplayUIEventHubMediator : MonoBehaviour, IUIEventMediator, IUIEventCollection
{

    #region - - - - - - Fields - - - - - -

    private readonly Dictionary<string, Action> m_GameplayHUDActions = new();
    private readonly Dictionary<string, Action<object>> m_GameplayHUDActionsWithParameter = new();

    #endregion Fields
  
    #region - - - - - - Methods - - - - - -

    public void Dispatch(string key)
        => this.m_GameplayHUDActions[key].Invoke();

    public void Dispatch(string key, object eventObject)
        => this.m_GameplayHUDActionsWithParameter[key].Invoke(eventObject);

    void IUIEventCollection.RegisterEvent(string key, Action eventAction)
        => this.m_GameplayHUDActions.Add(key, eventAction);

    void IUIEventCollection.RegisterEvent(string key, Action<object> eventAction)
        => this.m_GameplayHUDActionsWithParameter.Add(key, eventAction);

    #endregion Methods

}
