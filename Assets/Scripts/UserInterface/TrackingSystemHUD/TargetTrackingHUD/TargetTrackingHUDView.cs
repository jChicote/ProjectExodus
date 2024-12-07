using UnityEngine;

namespace ProjectExodus.UserInterface.TrackingSystemHUD.TargetTrackingHUD
{

    public class TargetTrackingHUDView : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_ContentGroup;
        [SerializeField] private RectTransform m_CrosshairTransform;

        private RectTransform m_ContentGroupRectTransform;

        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Start()
        {
            this.m_ContentGroupRectTransform = this.m_ContentGroup.GetComponent<RectTransform>();
        }

        #endregion Unity Methods
  
        #region - - - - - - Methods - - - - - -

        public void HideGUI()
            => this.m_ContentGroup.SetActive(false);

        public void ShowGUI()
            => this.m_ContentGroup.SetActive(true);

        public void UpdateCrosshairPosition(Vector2 screenPosition)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    this.m_ContentGroupRectTransform,
                    screenPosition,
                    null, // Set to null as the Canvas is done in Screen Overlay
                    out Vector2 _CanvasPosition))
                return;
            
            this.m_CrosshairTransform.anchoredPosition = _CanvasPosition;
        }

        #endregion Methods

    }

}