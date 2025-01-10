using ProjectExodus.GameLogic.Enumeration;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus
{

    public class PlayerTargetingHUDView : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -
        
        public Camera m_Camera;

        [SerializeField] private GameObject m_ContentGroup;
        [SerializeField] private Image m_HoverTargetingRecticle;
        [SerializeField] private RectTransform m_HoverTargetingRecticleTransform;

        [Space] [SerializeField] private Color m_DefaultColor;
        [SerializeField] private Color m_EnemyColor;
        [SerializeField] private Color m_InteractableColor;
        [SerializeField] private Color m_InvalidColor;

        private RectTransform m_ContentGroupRectTransform;

        #endregion Fields

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