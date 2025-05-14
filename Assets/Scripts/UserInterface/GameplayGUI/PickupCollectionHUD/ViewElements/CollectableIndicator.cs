using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectableIndicator : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    [SerializeField] private Image m_IndicatorImage;
    [SerializeField] private GameObject m_IndicatorCountGroup;
    [SerializeField] private TMP_Text m_IndicatorCount;
    [SerializeField] private Color m_ActiveColor;
    [SerializeField] private Color m_InactiveColor;

    #endregion Fields

    #region - - - - - - Methods - - - - - -

    public void DeactivateIndicator() 
        => this.m_IndicatorCountGroup.SetActive(false);

    public void SetCount(int count)
        => this.m_IndicatorCount.text = count.ToString();

    public void SetImage(Sprite sprite)
        => this.m_IndicatorImage.sprite = sprite;

    #endregion Methods

}
