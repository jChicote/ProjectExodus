using MBT;
using UnityEngine;

namespace ProjectExodus
{

    public class ThrustForward : Leaf
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private TransformReference m_SourceTransform;
        [SerializeField] private float m_TraversalSpeed;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        public override NodeResult Execute()
        {
            if (!this.IsWithinVerticalBound(this.m_SourceTransform.Value.position.y, 4)
                || !this.IsWithinHorizontalBound(this.m_SourceTransform.Value.position.x, 4)) 
                return NodeResult.success;
            
            this.m_SourceTransform.Value.position += this.m_SourceTransform.Value.forward * this.m_TraversalSpeed * Time.deltaTime;
            return NodeResult.running;
        }
        
        private bool IsWithinVerticalBound(float verticalPosition, float borderPadding)
        {
            Vector2 _UpperBound = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));
            Vector2 _LowerBound = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f));
            return verticalPosition < _UpperBound.y - borderPadding 
                   && verticalPosition > _LowerBound.y + borderPadding;
        }

        private bool IsWithinHorizontalBound(float horizontalPosition, float borderPadding)
        {
            Vector2 _UpperBound = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));
            Vector2 _LowerBound = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f));
            return horizontalPosition < _UpperBound.x - borderPadding
                   && horizontalPosition > _LowerBound.x + borderPadding;
        }

        #endregion Methods
  
    }

}