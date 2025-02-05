using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/Is Target Within Screen")]
    public class CheckTargetWithinScreenCondition : Condition   
    {
        [SerializeField] 
        private TransformReference m_TargetTransform = new();

        public override bool Check()
        {
            throw new System.NotImplementedException();
        }
    }

}