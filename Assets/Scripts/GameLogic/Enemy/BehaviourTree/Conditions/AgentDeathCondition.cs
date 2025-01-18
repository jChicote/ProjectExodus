using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/Is Dead")]
    public class AgentDeathCondition : Condition
    {

        #region - - - - - - Fields - - - - - -

        public FloatReference Health = new();

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public override bool Check() => this.Health.Value < 0;

        #endregion Methods
  
    }

}