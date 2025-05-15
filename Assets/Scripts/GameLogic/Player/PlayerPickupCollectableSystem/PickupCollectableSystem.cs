using System.Collections.Generic;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.Utility.GameValidation;
using UnityEngine;

public class PickupCollectableSystem : MonoBehaviour, IPickupCollectionSystem
{

    #region - - - - - - Fields - - - - - -

    private List<ICollectablePickup> m_CollectedPickups;
    private IUIEventMediator m_EventMediator;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_EventMediator = UserInterfaceManager.Instance.EventMediator;

        string _SourceObjectName = this.gameObject.name;
        GameValidator.NotNull(this.m_EventMediator, nameof(m_EventMediator), sourceObjectName: _SourceObjectName);
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    public void AddCollectable(ICollectablePickup collectablePickup) 
        => this.m_CollectedPickups.Add(collectablePickup);

    #endregion Methods
  
}
