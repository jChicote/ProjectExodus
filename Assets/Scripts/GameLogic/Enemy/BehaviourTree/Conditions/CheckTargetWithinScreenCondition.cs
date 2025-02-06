using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/Is Target Within Screen")]
    public class CheckTargetWithinScreenCondition : Condition   
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] 
        private TransformReference m_TargetTransform = new();

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        public override bool Check()
        {
            Vector2 _BottomLeftWorldPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 _TopRightWorldPosition = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            float _LeftViewportScreenLimit = _BottomLeftWorldPosition.x;
            float _RightViewportScreenLimit = _TopRightWorldPosition.x;
            float _TopViewportScreenLimit = _TopRightWorldPosition.y;
            float _BottomViewportScreenLimit = _BottomLeftWorldPosition.y;

            return this.m_TargetTransform.Value.position.x > _LeftViewportScreenLimit
                    && this.m_TargetTransform.Value.position.x < _RightViewportScreenLimit
                    && this.m_TargetTransform.Value.position.y > _BottomViewportScreenLimit 
                    && this.m_TargetTransform.Value.position.y < _TopViewportScreenLimit;
        }

        #endregion Methods
  
    }

}