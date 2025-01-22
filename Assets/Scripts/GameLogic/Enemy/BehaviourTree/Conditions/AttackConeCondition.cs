using System;
using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/Is Within Attack Cone")]
    public class AttackConeCondition : Condition
    {
        public TransformReference AgentTransform = new();
        public TransformReference TargetTransform = new();
        public WeaponFireInfo WeaponFireInfo = new();
        
        private float FiringArc = 45f;
        
        public override bool Check()
        {
            throw new System.NotImplementedException();
        }

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
    }

}