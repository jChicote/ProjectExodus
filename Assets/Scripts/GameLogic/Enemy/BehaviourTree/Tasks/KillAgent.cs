using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Object/Kill Agent")]
    public class KillAgent : Leaf
    {

        #region - - - - - - Fields - - - - - -

        public BoolReference IsDead = new();

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public override NodeResult Execute()
        {
            this.IsDead.Value = true;
            return NodeResult.success;
        }

        #endregion Methods
  
    }

}