using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Movement/Move Agent in Straight Line")]
    public class MoveAgentStraight : Leaf
    {

        #region - - - - - - Fields - - - - - -

        public Vector2Reference AgentMoveVelocity = new();
        public Vector2Reference AgentMoveDirection = new();
        public FloatReference MovementSpeed = new();

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public override NodeResult Execute()
        {
            this.AgentMoveVelocity.Value = this.AgentMoveDirection.Value * this.MovementSpeed.Value;
            return NodeResult.running;
        }

        #endregion Methods
  
    }

}