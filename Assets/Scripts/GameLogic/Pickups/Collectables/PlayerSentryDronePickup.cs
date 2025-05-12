using GameLogic.Pickups;
using ProjectExodus.GameLogic.Enumeration;
using UnityEngine;

public class PlayerSentryDronePickup : BasePickup
{

    #region - - - - - - Fields - - - - - -

    

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start() 
        => this.StartCoroutine(this.DestroyPickup());
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != GameTag.Player) return;
        
        Destroy(this.gameObject);
    }

    #endregion Unity Methods
  
  
}
