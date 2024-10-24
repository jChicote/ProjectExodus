using ProjectExodus.GameLogic.Common.Timers;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using UnityEngine;

namespace ProjectExodus.GameLogic.Projectiles
{

    public class BaseProjectile : PausableMonoBehavior
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] protected float m_Speed;
        [SerializeField] protected float m_Damage;
        [SerializeField] protected float m_Lifespan;

        protected EventTimer m_LifespanTimer;
        private float m_DeltaTime;

        #endregion Fields

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start()
        {
            this.m_DeltaTime = Time.deltaTime;
            this.m_LifespanTimer = new EventTimer(this.m_Lifespan, this.m_DeltaTime, this.DestroyProjectile);
        }

        #endregion Unity Lifecycle Methods
  
        #region - - - - - - Methods - - - - - -

        protected virtual void Move() 
            => this.transform.position += this.transform.up * (this.m_Speed * this.m_DeltaTime);

        protected virtual void DestroyProjectile()
            => Destroy(this.gameObject);

        #endregion Methods

    }

}