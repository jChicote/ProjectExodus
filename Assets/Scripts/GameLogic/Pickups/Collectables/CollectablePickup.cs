using ProjectExodus.GameLogic.Enumeration;
using ProjectExodus.Utility.GameLogging;
using UnityEngine;

namespace GameLogic.Pickups.Collectables
{

    public class CollectablePickup : BasePickup
    {
        
        #region - - - - - - Unity Methods - - - - - -

        private void Start() 
            => this.StartCoroutine(this.DestroyPickup());

        #endregion Unity Methods

        #region - - - - - - Unity Events - - - - - -

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag != GameTag.Player) return;

            // TODO: Implement behavior on SceneController to hold the SceneStateContext, which holds values about the 
            // game without being tied to the Player's lifetime.
            GameLogger.Log("No collectable collection is implemented.");
            
            Destroy(this.gameObject);
        }

        #endregion Unity Events
        
    }

}