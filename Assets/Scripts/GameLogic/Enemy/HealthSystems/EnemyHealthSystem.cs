using System;
using MBT;
using ProjectExodus.GameLogic.Common.Health;
using ProjectExodus.GameLogic.Enumeration;
using UnityEngine;

namespace ProjectExodus
{

    public static class EnemyHealthSystemKeys
    {

        #region - - - - - - Fields - - - - - -

        public const string CollisionHitCount = "CollisionHitCount";
        public const string IsDead = "IsDead";
        public const string Health = "Health";

        #endregion Fields

    }
    
    public class EnemyHealthSystem : MonoBehaviour, IDamageable
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private Blackboard m_Blackboard;
        [SerializeField] private int m_CurrentHealth;

        private IntVariable m_CollisionHitCountVariable;
        private FloatVariable m_HealthVariable;
        private BoolVariable m_IsDeadVariable;

        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Start()
        {
            this.m_CollisionHitCountVariable =
                this.m_Blackboard.GetVariable<IntVariable>(EnemyHealthSystemKeys.CollisionHitCount);
            this.m_IsDeadVariable = this.m_Blackboard.GetVariable<BoolVariable>(EnemyHealthSystemKeys.IsDead);
            this.m_HealthVariable = this.m_Blackboard.GetVariable<FloatVariable>(EnemyHealthSystemKeys.Health);

            this.m_HealthVariable.Value = this.m_CurrentHealth;
            this.m_IsDeadVariable.AddListener(this.DestroyEnemy);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == GameTag.Player)
                Debug.Log("Player is hit");
            else if (other.gameObject.tag == GameTag.Projectile)
                Debug.Log("Hit Projectile");
            else
                this.m_CollisionHitCountVariable.Value += 1;
        }

        #endregion Unity Methods

        #region - - - - - - Methods - - - - - -

        private void DestroyEnemy(bool oldValue, bool isDead)
        {
            Debug.Log("Invoked is dead");
            if (isDead)
                Destroy(this.gameObject);
        }

        public bool CanDamage() 
            => this.m_HealthVariable.Value > 0;

        public void SendDamage(float damage) 
            => this.m_HealthVariable.Value -= damage;

        #endregion Methods
        
    }

}