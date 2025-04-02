using System;
using ProjectExodus.GameLogic.Enumeration;
using UnityEngine;

public class EnemyLocatorHUDShapes : SmartEnum
{

    #region - - - - - - Fields - - - - - -

    public static EnemyLocatorHUDShapes Circle = new("Circle", 0, shape =>
    {
        float _Angle = (shape.CurrentSegment / (float)shape.SegmentCount) * Mathf.PI * 2;
        float _X = shape.Dimensions.x * Mathf.Cos(_Angle);
        float _Y = shape.Dimensions.y * Mathf.Sin(_Angle);
                
        return shape.CurrentPosition + new Vector2(_X, _Y);
    });
    public static EnemyLocatorHUDShapes Ellipse = new("Ellipse", 1, shape =>
    {
        float _Angle = (shape.CurrentSegment / (float)shape.SegmentCount) * Mathf.PI * 2;
        float _X = (shape.Dimensions.x / 2) * Mathf.Cos(_Angle);
        float _Y = (shape.Dimensions.y / 2) * Mathf.Sin(_Angle);
                
        return shape.CurrentPosition + new Vector2(_X, _Y);
    });

    #endregion Fields

    #region - - - - - - Constructors - - - - - -

    private EnemyLocatorHUDShapes(string name, int value, Func<ShapeDetails, Vector2> function) : base(name, value)
        => this.Function = function;

    #endregion Constructors

    #region - - - - - - Properties - - - - - -

    public Func<ShapeDetails, Vector2> Function { get; private set; }

    #endregion Properties

    #region - - - - - - Methods - - - - - -

    public static EnemyLocatorHUDShapes GetShape(EnemyLocatorHUDShape shape)
    {
        if (shape.ToString() == Circle.ToString())
            return Circle;
        
        return Ellipse;
    }

    #endregion Methods
  
}

public class ShapeDetails
{

    #region - - - - - - Properties - - - - - -

    public int CurrentSegment { get; set; }

    public int SegmentCount { get; set; }

    public Vector2 Dimensions { get; set; }

    public Vector2 CurrentPosition { get; set; }

    #endregion Properties
  
}
