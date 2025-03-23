using UnityEngine;

public class PointsGUIView : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    public GameObject m_PointsMarkerPrefab;
    public GameObject m_ContentGroup;

    #endregion Fields

    #region - - - - - - Methods - - - - - -

    public void AddPointsMarker(Vector2 position, int points)
    {
        PointsHoverMarker _PointsMarker = Instantiate(this.m_PointsMarkerPrefab, this.m_ContentGroup.transform)
            .GetComponent<PointsHoverMarker>();
        _PointsMarker.SetPosition(position);
        _PointsMarker.SetPoints(points);
    }
    
    #endregion Methods
  
}
