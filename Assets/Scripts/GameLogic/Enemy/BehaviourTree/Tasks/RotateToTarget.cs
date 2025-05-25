using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/Rotate To Target")]
    public class RotateToTarget : Leaf
    {

        #region - - - - - - Fields - - - - - -

        public TransformReference SourceTransform = new();
        public TransformReference TargetTransform = new();
        public FloatReference TurnSpeed = new();

        private Vector3 m_Direction;
        private float m_Angle;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public override NodeResult Execute()
        {
            if (this.TargetTransform.Value == null) return NodeResult.failure;
            
            // Calculate the direction to target
            Vector3 _Direction = this.TargetTransform.Value.position - this.SourceTransform.Value.position;
            this.m_Direction = _Direction * -1;
            this.m_Angle = Mathf.Atan2(this.m_Direction.y, this.m_Direction.x) * Mathf.Rad2Deg;

            if (this.m_Direction == Vector3.zero) return NodeResult.success;

            // Calculate the desired rotation
            var _PreferredEuler = Quaternion.Euler(Vector3.forward * (m_Angle + 90f));
            this.SourceTransform.Value.rotation = Quaternion.Lerp(
                this.SourceTransform.Value.rotation, 
                _PreferredEuler, 
                Time.deltaTime * this.TurnSpeed.Value);
            
            return NodeResult.success;
        }

        #endregion Methods
  
        #region - - - - - - Gizmos - - - - - -

        public void OnDrawGizmos()
        {
            if (!Application.isPlaying || this.SourceTransform.Value == null) return;
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(this.SourceTransform.Value.position, this.m_Direction * -1);
        }

        #endregion Gizmos
  
    }

}