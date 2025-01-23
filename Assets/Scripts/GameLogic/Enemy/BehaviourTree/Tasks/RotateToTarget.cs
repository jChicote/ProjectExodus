using System;
using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/Rotate To Target")]
    public class RotateToTarget : Leaf
    {
        public TransformReference SourceTransform = new();
        public TransformReference TargetTransform = new();
        public FloatReference TurnSpeed = new();

        private Vector3 m_Direction;
        private float m_Angle;

        private void Update()
        {
            if (m_Direction != Vector3.zero)
            {
                Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * m_Direction;
                
                // Calculate the desired rotation
                // Quaternion _TargetRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
                // this.SourceTransform.Value.rotation = Quaternion.RotateTowards(
                //     this.SourceTransform.Value.rotation,
                //     _TargetRotation,
                //     this.TurnSpeed.Value * Time.deltaTime
                // );
                
                this.SourceTransform.Value.rotation = Quaternion.Euler(Vector3.forward * (m_Angle + 90f));
            }
        }
        
        public override NodeResult Execute()
        {
            if (this.TargetTransform.Value == null) return NodeResult.failure;
            
            // Calculate the direction to target
            Vector3 _Direction = this.TargetTransform.Value.position - this.SourceTransform.Value.position;
            this.m_Direction = _Direction;
            this.m_Angle = Mathf.Atan2(this.m_Direction.y, this.m_Direction.x) * Mathf.Rad2Deg;
            
            return NodeResult.success;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(this.SourceTransform.Value.position,   m_Direction);
        }
    }

}