using ProjectExodus.Management.UserInterfaceManager;
using UnityEngine;

public class PointsGUIController : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    private PointsGUIView m_View;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_View = this.GetComponent<PointsGUIView>();
        
        IUIEventCollection _UIEventCollection = UserInterfaceManager.Instance.EventCollectionRegistry;
        _UIEventCollection.RegisterEvent(
            PointsGUIConstants.AddPoints,
            pointsInfo => this.AddPointsHoverMarker(pointsInfo as PointsInfo));
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    private void AddPointsHoverMarker(PointsInfo pointsInfo) 
        => this.m_View.AddPointsMarker(pointsInfo.Position, pointsInfo.Points);

    #endregion Methods

}

public class PointsInfo
{

    #region - - - - - - Properties - - - - - -

    public int Points { get; set; }

    public Vector2 Position { get; set; }

    #endregion Properties
  
}
