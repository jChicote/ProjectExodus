using System.Collections;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using UnityEngine;

namespace GameLogic.Pickups
{

    public class BasePickup : PausableMonoBehavior, IPickup
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
  
    }

}