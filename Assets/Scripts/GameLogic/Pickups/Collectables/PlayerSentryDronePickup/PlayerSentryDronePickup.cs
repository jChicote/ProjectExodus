using GameLogic.Pickups.Collectables;
using ProjectExodus.GameLogic.Enumeration;
using UnityEngine;

public class PlayerSentryDronePickup : CollectablePickup
{

    #region - - - - - - Unity Methods - - - - - -

    private void Start() 
        => this.StartCoroutine(this.DestroyPickup());
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != GameTag.Player) return;

        IPickupCollectionSystem _PickupCollectionSystem = other.GetComponent<IPickupCollectionSystem>();
        _PickupCollectionSystem.AddCollectable(this);
        
        Destroy(this.gameObject);
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    public override PickupEnum GetPickupType() 
        => PickupEnum.AutonomousSentry;

    #endregion Methods
  
}
