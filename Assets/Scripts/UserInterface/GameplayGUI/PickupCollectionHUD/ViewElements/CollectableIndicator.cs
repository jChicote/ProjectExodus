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
    {
        this.m_IndicatorImage.color = this.m_InactiveColor;
        this.m_IndicatorCountGroup.SetActive(false);
    }

    public void EnableIndicator()
    {
        this.m_IndicatorImage.color = this.m_ActiveColor;
        this.m_IndicatorCountGroup.SetActive(true);
    }

    public void SetCount(int count)
        => this.m_IndicatorCount.text = count.ToString();

    public void SetImage(Sprite sprite)
        => this.m_IndicatorImage.sprite = sprite;

    #endregion Methods

}
