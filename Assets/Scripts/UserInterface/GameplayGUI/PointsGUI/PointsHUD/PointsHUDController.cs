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
            PointsGUIConstants.UpdatePoints,
            totalPoints => this.UpdateTotallPoints(totalPoints as int? ?? default));
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    private void UpdateTotallPoints(int totalPoints)
        => this.m_View.UpdateTotalPoints(totalPoints);

    #endregion Methods
    
}
