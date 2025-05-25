using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/Does Target Exist")]
    public class DoesTargetExistCondition : Condition
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private TransformReference m_TargetTransform = new();
        
        [SerializeField] private bool m_Invert;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public override bool Check() 
            => this.m_TargetTransform.Value != null ^ this.m_Invert;

        #endregion Methods
  
    }

}