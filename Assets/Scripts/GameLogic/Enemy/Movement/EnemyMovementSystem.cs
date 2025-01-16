using MBT;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using UnityEngine;

namespace ProjectExodus
{

    public static class EnemyMovementKeys
    {

        #region - - - - - - Fields - - - - - -

        public const string MoveDirection = "AgentMoveDirection";
        public const string MovementVelocity = "MovementVelocity";
        public const string MovementSpeed = "MovementSpeed";

        #endregion Fields

    }

    public class EnemyMovementSystem : PausableMonoBehavior
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private Rigidbody2D m_Rigidbody;
        [SerializeField] private Blackboard m_Blackboard;

        private Vector2Variable m_MovementVelocityReference;
        private Vector2Variable m_MoveDirectionReference;
        private FloatVariable m_MovementSpeedReference;
        
        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Start()
        {
            this.m_MovementVelocityReference =
                this.m_Blackboard.GetVariable<Vector2Variable>(EnemyMovementKeys.MovementVelocity);
            this.m_MoveDirectionReference =
                this.m_Blackboard.GetVariable<Vector2Variable>(EnemyMovementKeys.MoveDirection);
            this.m_MovementSpeedReference =
                this.m_Blackboard.GetVariable<FloatVariable>(EnemyMovementKeys.MovementSpeed);

            this.m_MoveDirectionReference.Value = this.transform.rotation * Vector2.up;
            this.m_MovementSpeedReference.Value = 5f;
        }

        private void Update()
        {
            if (this.m_IsPaused)
            {
                this.m_Rigidbody.linearVelocity = Vector2.zero;
                return;
            }

            this.m_Rigidbody.linearVelocity = this.m_MovementVelocityReference.Value;
        }

        #endregion Unity Methods

    }

}