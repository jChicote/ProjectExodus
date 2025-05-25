using System.Collections;
using UnityEngine;

namespace GameLogic.Pickups.Collectables
{

    public class CollectablePickup : MonoBehaviour, ICollectablePickup
    {
        
        #region - - - - - - Fields - - - - - -

        [SerializeField] protected float m_Lifetime;

        #endregion Fields
        
        #region - - - - - - Methods - - - - - -

        protected IEnumerator DestroyPickup()
        {
            yield return new WaitForSeconds(this.m_Lifetime);
            Destroy(this.gameObject);
        }

        #endregion Methods

        public void Activate()
        {
        }

        public virtual PickupEnum GetPickupType() 
            => PickupEnum.None;
    }

}