using ProjectExodus.Management.UserInterfaceManager;
using UnityEngine;

public class PointsHUDController : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    private PointsHUDView m_View;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_View = this.GetComponent<PointsHUDView>();
        
        IUIEventCollection _UIEventCollection = UserInterfaceManager.Instance.EventCollectionRegistry;
        _UIEventCollection.RegisterEvent(
            PointsHUDConstants.AddPoints,
            pointsInfo => this.AddPointsHoverMarker(pointsInfo as PointsInfo));
        _UIEventCollection.RegisterEvent(
            PointsHUDConstants.UpdatePoints,
            totalPoints => this.UpdateTotallPoints(totalPoints as int? ?? default));
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    private void AddPointsHoverMarker(PointsInfo pointsInfo) 
        => this.m_View.AddPointsMarker(pointsInfo.Position, pointsInfo.Points);

    private void UpdateTotallPoints(int totalPoints)
        => this.m_View.UpdateTotalPoints(totalPoints);

    #endregion Methods

}

public class PointsInfo
{

    #region - - - - - - Properties - - - - - -

    public int Points { get; set; }

    public Vector2 Position { get; set; }

    #endregion Properties
  
}
