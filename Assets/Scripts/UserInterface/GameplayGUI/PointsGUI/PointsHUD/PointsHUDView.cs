using TMPro;
using UnityEngine;

public class PointsHUDView : MonoBehaviour
{
    
    #region - - - - - - Fields - - - - - -

    [SerializeField] private TMP_Text m_TotalPointsText;
    [SerializeField] private GameObject m_ContentGroup;

    #endregion Fields

    #region - - - - - - Methods - - - - - -

    public void UpdateTotalPoints(int totalPoints) 
        => this.m_TotalPointsText.text = $"Total Points: {totalPoints.ToString()}";

    #endregion Methods

}
