using TMPro;
using UnityEngine;

public class PointsHUDView : MonoBehaviour
{
    
    #region - - - - - - Fields - - - - - -

    public TMP_Text m_TotalPointsText;
    public GameObject m_ContentGroup;

    #endregion Fields

    #region - - - - - - Methods - - - - - -

    public void UpdateTotalPoints(int totalPoints) 
        => this.m_TotalPointsText.text = $"Total Points: {totalPoints.ToString()}";

    #endregion Methods

}
