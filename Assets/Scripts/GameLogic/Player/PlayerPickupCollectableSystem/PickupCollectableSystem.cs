using System.Collections.Generic;
using UnityEngine;

public class PickupCollectableSystem : MonoBehaviour, IPickupCollectionSystem
{

    #region - - - - - - Fields - - - - - -

    private List<ICollectablePickup> m_CollectedPickups;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    public void AddCollectable(ICollectablePickup collectablePickup) 
        => this.m_CollectedPickups.Add(collectablePickup);

    #endregion Methods
  
}
