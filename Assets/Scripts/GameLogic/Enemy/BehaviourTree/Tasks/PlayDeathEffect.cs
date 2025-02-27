using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Effects/Play Death Effect")]
    public class PlayDeathEffect : Leaf
    {

        #region - - - - - - Fields - - - - - -

        public GameObjectReference DeathEffect = new();
        public TransformReference AgentTransform = new();

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        public override NodeResult Execute()
        {
            Instantiate(this.DeathEffect.Value, this.AgentTransform.Value.position, Quaternion.identity);
            return NodeResult.success;
        }

        #endregion Methods
  
    }

}