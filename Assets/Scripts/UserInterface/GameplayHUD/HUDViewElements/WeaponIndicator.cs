using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIndicator : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    [Header("Indication Colors")]
    [SerializeField] private Color m_NormalColor;
    [SerializeField] private Color m_DepletedColor;

    [Header("Color affected elements")]
    [SerializeField] private Image m_IndicatorIcon;
    [SerializeField] private Slider m_AmmoBarSlider;
    [SerializeField] private Image m_Background;
    
    [Header("Warning Fields")]
    [SerializeField] private GameObject m_WarningSign;
    [SerializeField] private int m_WarningFlashCount;

    private bool m_IsPlayingWarning;

    #endregion Fields

    #region - - - - - - Methods - - - - - -]

    public void UpdateWeaponState(float delta)
    {
        Color _Transition = Color.Lerp(this.m_NormalColor, this.m_DepletedColor, 1 - delta);
        this.m_IndicatorIcon.color = _Transition;
        this.m_AmmoBarSlider.targetGraphic.color = _Transition;
        this.m_Background.color = _Transition;
        
        this.m_AmmoBarSlider.value = delta;
    }

    public void ShowWarning()
    {
        this.StopAllCoroutines();
        this.StartCoroutine(this.FlashWarningIcon());

        this.m_IsPlayingWarning = true;
    }

    public void HideWarning()
    {
        if (!this.m_IsPlayingWarning) return;
        
        this.StopAllCoroutines();
        this.m_IsPlayingWarning = false;
        this.m_WarningSign.SetActive(false);
    }

    private IEnumerator FlashWarningIcon()
    {
        for (int i = 0; i < this.m_WarningFlashCount; i++)
        {
            this.m_WarningSign.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            
            this.m_WarningSign.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void ShowIndicator()
    {
        this.gameObject.SetActive(true);
    }

    public void HideIndicator()
    {
        this.gameObject.SetActive(false);
    }

    #endregion Methods

}
