using MBT;
using UnityEngine;

namespace ProjectExodus
{

    public static class EnemyHealthSystemKeys
    {

        #region - - - - - - Fields - - - - - -

        public const string IsDead = "IsDead";
        public const string Health = "Health";

        #endregion Fields

    }
    
    public class EnemyHealthSystem : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private Blackboard m_Blackboard;

        private FloatVariable m_HealthVariable;
        private BoolVariable m_IsDeadVariable;

        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Start()
        {
            this.m_IsDeadVariable = this.m_Blackboard.GetVariable<BoolVariable>(EnemyHealthSystemKeys.IsDead);
            this.m_HealthVariable = this.m_Blackboard.GetVariable<FloatVariable>(EnemyHealthSystemKeys.Health);

            this.m_HealthVariable.Value = 10f;
            this.m_IsDeadVariable.AddListener(this.DestroyEnemy);
        }

        #endregion Unity Methods

        #region - - - - - - Methods - - - - - -

        private void DestroyEnemy(bool oldValue, bool isDead)
        {
            if (isDead)
                Destroy(this.gameObject);
        }

        #endregion Methods
  
    }

}