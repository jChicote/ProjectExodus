using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/Is Target Within Radius")]
    public class CheckIsWithinDistanceCondition : Condition
    {
        public TransformReference SourceTransform = new();
        public TransformReference TargetTransform = new();
        public FloatReference DetectionDistance = new();
            
        public override bool Check()
        {
            if (this.TargetTransform.Value == null) return false;

            return Vector3.Distance(
                this.SourceTransform.Value.position, 
                this.TargetTransform.Value.position) < this.DetectionDistance.Value;
        }
    }

}