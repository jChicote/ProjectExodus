using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Movement/Move Agent in Straight Line")]
    public class MoveAgentForward : Leaf
    {

        #region - - - - - - Fields - - - - - -

        public TransformReference AgentTransform = new();
        public Vector2Reference AgentMoveVelocity = new();
        public FloatReference MovementSpeed = new();

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public override NodeResult Execute()
        {
            this.AgentMoveVelocity.Value = this.AgentTransform.Value.up * this.MovementSpeed.Value;
            return NodeResult.success;
        }

        #endregion Methods
  
    }

}