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
        public const string DeathEffect = "DeathEffect";

        #endregion Fields

    }

    public class EnemyHealthSystemInitializerData
    {

        #region - - - - - - Properties - - - - - -

        public float Health { get; set; }

        #endregion Properties
  
    }
    
    public class EnemyHealthSystem : MonoBehaviour, IDamageable, IInitialize<EnemyHealthSystemInitializerData>
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private Blackboard m_Blackboard;
        
        private float m_CurrentHealth;
        private IntVariable m_CollisionHitCountVariable;
        private FloatVariable m_HealthVariable;
        private BoolVariable m_IsDeadVariable;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public float CurrentHealth
        {
            get => this.m_CurrentHealth;
            private set
            {
                this.m_CurrentHealth = value;
                this.m_HealthVariable.Value = value;
            }
        }

        #endregion Properties

        #region - - - - - - Initializers - - - - - -

        public void Initialize(EnemyHealthSystemInitializerData initializationData)
        {
            this.m_CollisionHitCountVariable =
                this.m_Blackboard.GetVariable<IntVariable>(EnemyHealthSystemKeys.CollisionHitCount);
            this.m_IsDeadVariable = this.m_Blackboard.GetVariable<BoolVariable>(EnemyHealthSystemKeys.IsDead);
            this.m_HealthVariable = this.m_Blackboard.GetVariable<FloatVariable>(EnemyHealthSystemKeys.Health);
            
            this.CurrentHealth = initializationData.Health;
            this.m_IsDeadVariable.AddListener(this.DestroyEnemy);
        }

        #endregion Initializers
  
        #region - - - - - - Unity Methods - - - - - -

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == GameTag.Player)
            {
                IDamageable _DamageablePlayer = other.gameObject.GetComponent<IDamageable>();
                _DamageablePlayer.SendDamage(5f);

                this.CurrentHealth -= this.CurrentHealth + 1; // Ensures to deplete enemy's health
            }
            
            // Ignore projectile collisions. Note: Damage is sent not handled
            else if (other.gameObject.tag == GameTag.Projectile)
                return;
            
            this.m_CollisionHitCountVariable.Value += 1;
        }

        #endregion Unity Methods

        #region - - - - - - Methods - - - - - -

        private void DestroyEnemy(bool oldValue, bool isDead)
        {
            if (!isDead) return;
            
            Destroy(this.gameObject);
        }

        public bool CanDamage() 
            => this.m_HealthVariable.Value > 0;

        public void SendDamage(float damage) 
            => this.CurrentHealth -= damage;

        #endregion Methods

    }

}