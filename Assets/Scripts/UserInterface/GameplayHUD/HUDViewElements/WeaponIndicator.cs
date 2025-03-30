using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIndicator : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    public Image m_IndicatorIcon;
    public Color m_NormalColor;
    public Color m_DepletedColor;
    
    public GameObject m_WarningSign;
    public int m_WarningFlashCount;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -]

    public void UpdateWeaponState(float delta)
    {
        Color _Transition = Color.Lerp(this.m_NormalColor, this.m_DepletedColor, delta);
        this.m_IndicatorIcon.color = _Transition;
    }

    public void ShowWarning()
    {
        this.StopAllCoroutines();
        this.StartCoroutine(this.FlashWarningIcon());
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
