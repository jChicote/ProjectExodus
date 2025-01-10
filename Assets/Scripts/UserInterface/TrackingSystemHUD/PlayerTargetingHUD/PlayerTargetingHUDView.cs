using System;
using ProjectExodus.GameLogic.Enumeration;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus
{

    public class PlayerTargetingHUDView : MonoBehaviour, IInitialize<PlayerTargetingHUDData>
    {

        #region - - - - - - Fields - - - - - -

        // Component Data
        [SerializeField] private GameObject m_ContentGroup;
        [SerializeField] private Image m_HoverTargetingRecticle;
        [SerializeField] private RectTransform m_HoverTargetingRecticleTransform;
        
        private Camera m_Camera;
        private RectTransform m_ContentGroupRectTransform;

        // Available Colors
        private Color m_DefaultColor;
        private Color m_EnemyColor;
        private Color m_InteractableColor;
        private Color m_InvalidColor;

        #endregion Fields

        #region - - - - - - Initialize - - - - - -

        public void Initialize(PlayerTargetingHUDData initializationData)
        {
            this.m_Camera = initializationData.Camera ??
                throw new ArgumentNullException(nameof(initializationData.Camera));

            this.m_DefaultColor = initializationData.UserInterfaceSettings.DefaultColor;
            this.m_EnemyColor = initializationData.UserInterfaceSettings.EnemyColor;
            this.m_InteractableColor = initializationData.UserInterfaceSettings.InteractableColor;
            this.m_InvalidColor = initializationData.UserInterfaceSettings.InvalidColor;
        }

        #endregion Initialize
  
        #region - - - - - - Unity Methods - - - - - -

        private void Start()
            => this.m_ContentGroupRectTransform = this.m_ContentGroup.GetComponent<RectTransform>();

        #endregion Unity Methods

        #region - - - - - - Methods - - - - - -

        public void UpdateHoverTargetingRecticlePosition(Vector2 screenPosition)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    this.m_ContentGroupRectTransform,
                    screenPosition,
                    this.m_Camera, // Set to null as the Canvas is done in Screen Overlay
                    out Vector2 _CanvasPosition))
                return;

            this.m_HoverTargetingRecticleTransform.anchoredPosition = _CanvasPosition;
        }

        public void UpdateHoverTargetingRecticleColor(GameTag hoverType)
        {
            if (hoverType == GameTag.Enemy)
                this.m_HoverTargetingRecticle.color = this.m_EnemyColor;
            else if (hoverType == GameTag.Interactable)
                this.m_HoverTargetingRecticle.color = this.m_InteractableColor;
            else if (hoverType == GameTag.Player)
                this.m_HoverTargetingRecticle.color = this.m_InvalidColor;
            else
                this.m_HoverTargetingRecticle.color = this.m_DefaultColor;
        }

        public void ShowHoverTargetingRecticle()
            => this.m_HoverTargetingRecticleTransform.gameObject.SetActive(true);

        public void HideHoverTargetingRecticle()
            => this.m_HoverTargetingRecticleTransform.gameObject.SetActive(false);

        #endregion Methods

    }

}