using ProjectExodus.Management.UserInterfaceManager;
using UnityEngine;

/// <summary>
/// Responsible for being a marker reference for the Enemy Locator HUD UI.
/// </summary>
public class TrackingMarkerReference : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    private IUIEventMediator m_EventMediator;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_EventMediator = UserInterfaceManager.Instance.EventMediator;
        this.m_EventMediator.Dispatch(EnemyLocatorHUDEventConstants.AddEnemyMarker, this.gameObject.transform);
    }

    private void OnDestroy() 
        => this.m_EventMediator.Dispatch(EnemyLocatorHUDEventConstants.RemoveEnemyMarker, this.gameObject);

    #endregion Unity Methods
  
}
