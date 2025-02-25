using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/Has Reached Screen Borders")]
    public class HasReachedScreenBorders : Condition
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private TransformReference m_SourceTransform;
        [SerializeField] 
        [Tooltip("Padding distance from the screen borders.")]
        private float m_BorderPadding = 1;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public override bool Check()
        {
            var _Condition = !this.IsWithinVerticalBound(this.m_SourceTransform.Value.position.y, this.m_BorderPadding)
                             || !this.IsWithinHorizontalBound(this.m_SourceTransform.Value.position.x, this.m_BorderPadding);
            Debug.Log("Has reached screen border is:" + _Condition);
            return _Condition;
        }

        // TODO: This needs to be optimised and refactored
        private bool IsWithinVerticalBound(float verticalPosition, float borderPadding)
        {
            Vector2 _UpperBound = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));
            Vector2 _LowerBound = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f));
            return verticalPosition < _UpperBound.y - borderPadding 
                   && verticalPosition > _LowerBound.y + borderPadding;
        }

        // TODO: This needs to be optimised and refactored
        private bool IsWithinHorizontalBound(float horizontalPosition, float borderPadding)
        {
            Vector2 _UpperBound = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));
            Vector2 _LowerBound = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f));
            return horizontalPosition < _UpperBound.x - borderPadding
                   && horizontalPosition > _LowerBound.x + borderPadding;
        }

        #endregion Methods

        #region - - - - - - Gizmos - - - - - -

        private void OnDrawGizmos()
        {
            Vector2 _UpperBound = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));
            Vector2 _CameraCenter = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
            
            Gizmos.DrawWireSphere(_UpperBound, 2f);
            Gizmos.DrawWireSphere(_CameraCenter, 2f);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(
                _CameraCenter, 
                new Vector2(
                    _UpperBound.x,// - this.m_BorderPadding, 
                    _UpperBound.y));// - this.m_BorderPadding));

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(
                _CameraCenter,
                new Vector2(Screen.width - this.m_BorderPadding * 2, Screen.height - this.m_BorderPadding * 2));
        }

        #endregion Gizmos
  
    }

}