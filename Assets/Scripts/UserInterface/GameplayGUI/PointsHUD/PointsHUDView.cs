using TMPro;
using UnityEngine;

public class PointsHUDView : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    public TMP_Text m_TotalPointsText;
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

    public void UpdateTotalPoints(int totalPoints) 
        => this.m_TotalPointsText.text = $"Total Points: {totalPoints.ToString()}";

    #endregion Methods
  
}
