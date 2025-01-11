using System;
using UnityEngine;

namespace ProjectExodus
{

    public class WeaponTargetTrackingHUDView : MonoBehaviour, IInitialize<WeaponTargetTrackingHUDData>
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_ContentGroup;
        [SerializeField] private RectTransform m_CrosshairTransform;

        private Camera m_Camera;
        private RectTransform m_ContentGroupRectTransform;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        public void Initialize(WeaponTargetTrackingHUDData initializerData)
        {
            this.m_Camera = initializerData.Camera ?? throw new ArgumentNullException(nameof(initializerData.Camera));
            this.m_ContentGroupRectTransform = this.m_ContentGroup.GetComponent<RectTransform>();
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        public void UpdateCrosshairPosition(Vector2 screenPosition)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    this.m_ContentGroupRectTransform,
                    screenPosition,
                    this.m_Camera, // Set to null as the Canvas is done in Screen Overlay
                    out Vector2 _CanvasPosition))
                return;

            this.m_CrosshairTransform.anchoredPosition = _CanvasPosition;
        }

        public void ShowTargetRecticle()
            => this.m_CrosshairTransform.gameObject.SetActive(true);

        public void HideTargetRecticle()
            => this.m_CrosshairTransform.gameObject.SetActive(false);

        #endregion Methods

    }


}