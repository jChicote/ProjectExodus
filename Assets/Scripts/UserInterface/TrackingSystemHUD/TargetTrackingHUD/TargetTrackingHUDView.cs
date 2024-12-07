using UnityEngine;

namespace ProjectExodus.UserInterface.TrackingSystemHUD.TargetTrackingHUD
{

    public class TargetTrackingHUDView : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_ContentGroup;
        [SerializeField] private RectTransform m_CrosshairTransform;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void SetCrosshairPosition(Vector2 crosshairPosition) 
            => this.m_CrosshairTransform.anchoredPosition = crosshairPosition;

        public void HideGUI()
            => this.m_ContentGroup.SetActive(false);

        public void ShowGUI()
            => this.m_ContentGroup.SetActive(true);

        public void UpdateCrosshairPosition(Vector2 screenPosition)
            => this.m_CrosshairTransform.anchoredPosition = screenPosition;

        #endregion Methods

    }

}