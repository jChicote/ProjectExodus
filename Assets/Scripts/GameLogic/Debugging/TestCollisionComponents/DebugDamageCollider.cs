using ProjectExodus.GameLogic.Common.Health;
using UnityEngine;

namespace ProjectExodus.GameLogic.Debugging.TestCollisionComponents
{

    public class DebugDamageCollider : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private float m_CollisionDamage;

        #endregion Fields
  
        #region - - - - - - Unity Collision Methods - - - - - -

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IDamageable _DamageableObject = collision.gameObject.GetComponent<IDamageable>();
            if (_DamageableObject == null) return;
            
            _DamageableObject.SendDamage(this.m_CollisionDamage);
            Debug.Log("[LOG]: Collision hit detected");
        }

        #endregion Unity Collision Methods
  
    }

}