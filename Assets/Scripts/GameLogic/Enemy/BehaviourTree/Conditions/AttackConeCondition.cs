using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/Is Within Attack Cone")]
    public class AttackConeCondition : Condition
    {

        #region - - - - - - Fields - - - - - -

        public TransformReference AgentTransform = new();
        public TransformReference TargetTransform = new();
        public WeaponSystemsInfoReference WeaponSystemsInfo = new();
        
        private float FiringArc = 45f; // TODO: Use Weapon systems info

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public override bool Check()
        {
            Vector2 _DirectionToTarget = this.TargetTransform.Value.position - this.AgentTransform.Value.position;
            _DirectionToTarget.Normalize();

            float _DotProduct = Vector2.Dot(this.AgentTransform.Value.up, _DirectionToTarget);
            float _AngleToTarget = Mathf.Acos(_DotProduct) * Mathf.Rad2Deg;
                
            bool _IsWithinCone = _AngleToTarget <= this.FiringArc / 2;
            return _IsWithinCone;
        }

        #endregion Methods

        #region - - - - - - Gizmos - - - - - -

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            
            // Draw cone directions
            Vector2 enemyForward = AgentTransform.Value.up; // Assuming the player faces "up" in 2D space
            Vector2 leftBoundary = Quaternion.Euler(0, 0, FiringArc / 2f) * enemyForward;
            Vector2 rightBoundary = Quaternion.Euler(0, 0, -FiringArc / 2f) * enemyForward;

            Gizmos.DrawLine(transform.position, transform.position + (Vector3)leftBoundary * 10);
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)rightBoundary * 10);
        }

        #endregion Gizmos
  
    }

}