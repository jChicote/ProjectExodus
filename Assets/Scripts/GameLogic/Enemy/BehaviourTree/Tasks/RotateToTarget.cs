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
        
        public override NodeResult Execute()
        {
            if (this.TargetTransform.Value == null) return NodeResult.failure;
            
            // Calculate the direction to target
            Vector3 _Direction = this.SourceTransform.Value.position - this.TargetTransform.Value.position;
            if (_Direction != Vector3.zero)
            {
                // Calculate the desired rotation
                Quaternion _TargetRotation = Quaternion.LookRotation(_Direction);
                this.SourceTransform.Value.rotation = Quaternion.RotateTowards(
                    this.SourceTransform.Value.rotation,
                    _TargetRotation,
                    this.TurnSpeed.Value * Time.deltaTime
                );
            }
            
            return NodeResult.success;
        }
    }

}