using ProjectExodus.GameLogic.Common.Health;
using ProjectExodus.GameLogic.Enumeration;
using UnityEngine;

namespace ProjectExodus.GameLogic.Projectiles
{

    public class DebugBulletProjectile : BaseProjectile
    {

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Update()
        {
            if (this.m_IsPaused) return;
            
            this.Move();
            this.m_LifespanTimer.TickTimer();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == GameLayer.Environment) return;
            
            IDamageable _DamageableObject = other.gameObject.GetComponent<IDamageable>();
            if (_DamageableObject == null) return;
            
            _DamageableObject.SendDamage(this.m_Damage);
            this.DestroyProjectile();
        }

        #endregion Unity Lifecycle Methods

    }

}