using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/Is Target Within Radius")]
    public class CheckIsWithinDistanceCondition : Condition
    {

        #region - - - - - - Fields - - - - - -

        public TransformReference SourceTransform = new();
        public TransformReference TargetTransform = new();
        public FloatReference DetectionDistance = new();

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        public override bool Check()
        {
            if (this.TargetTransform.Value == null) return false;

            // TODO: Needs to be optimised to use sqrmagnitude
            return Vector3.Distance(
                this.SourceTransform.Value.position, 
                this.TargetTransform.Value.position) < this.DetectionDistance.Value;
        }

        #endregion Methods

        #region - - - - - - Gizmos - - - - - -

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying
                || this.SourceTransform.Value == null
                || this.TargetTransform.Value == null) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.SourceTransform.Value.position, this.DetectionDistance.Value);
        }

        #endregion Gizmos
  
    }

}