using System;
using UnityEngine;

public class LineShapeGenerator : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    [SerializeField] private RectTransform m_CanvasRect;
    [SerializeField, RequiredField] private LineRenderer m_LineRenderer;
    [SerializeField] private int m_LineSegmentCount = 100;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start() 
        => this.m_LineRenderer = this.GetComponent<LineRenderer>();

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    /// <summary>
    /// Draws Shape
    /// </summary>
    /// <remarks>This is not intended for calculation during runtime</remarks>
    public void DrawShape(Vector2 shapeDimensions, Func<ShapeDetails, Vector2> shapeCalculation)
    {
        Camera _MainCamera = Camera.main;
        this.m_LineRenderer.positionCount = this.m_LineSegmentCount + 1;
        Vector3[] _Points = new Vector3[this.m_LineSegmentCount + 1];
        ShapeDetails _Shape = new ShapeDetails
        {
            CurrentSegment = 0,
            CurrentPosition = _MainCamera.transform.position,
            Dimensions = shapeDimensions,
            SegmentCount = this.m_LineSegmentCount + 1
        };

        for (int i = 0; i <= this.m_LineSegmentCount; i++)
        {
            _Shape.CurrentSegment = i;
            Vector2 _ViewportPosition = _MainCamera.WorldToScreenPoint(shapeCalculation.Invoke(_Shape));
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                this.m_CanvasRect, 
                _ViewportPosition, 
                _MainCamera, 
                out Vector2 _LocalPosition);
            
            _Points[i] = _LocalPosition;
        }

        this.m_LineRenderer.SetPositions(_Points);
    }

    #endregion Methods
  
}
