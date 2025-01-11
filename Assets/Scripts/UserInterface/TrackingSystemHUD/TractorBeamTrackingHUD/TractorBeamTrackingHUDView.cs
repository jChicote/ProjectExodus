using System;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus
{

    public class TractorBeamTrackingHUDView : MonoBehaviour, IInitialize<TractorBeamTrackingHUDData>
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_ContentGroup;
        
        // Required Components
        [Space]
        [SerializeField] private RectTransform m_CanvasRectTransform;
        [SerializeField] private Image m_TractorCircle;
        [SerializeField] private RectTransform m_TractorCircleTransform;
        [SerializeField] private Image m_TractorRecticle;
        [SerializeField] private RectTransform m_TractorRecticleTransform;
        [SerializeField] private LineRenderer m_TractorBeamLine;
        [SerializeField] private RectTransform m_TractorMaxDistanceCircleTransform;
        [SerializeField] private GameObject m_OutOfRangeLabel;

        private Camera m_Camera;
        
        // Available colors
        private Color m_FullBeamStrengthColor;
        private Color m_WeakBeamStrengthColor;
        private Color m_CurrentBeamColor;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        public void Initialize(TractorBeamTrackingHUDData initializerData)
        {
            this.m_Camera = initializerData.Camera ?? throw new ArgumentNullException(nameof(initializerData.Camera));

            this.m_WeakBeamStrengthColor = initializerData.UserInterfaceSettings.WeakBeamStrengthColor;
            this.m_FullBeamStrengthColor = initializerData.UserInterfaceSettings.FullBeamStregnthColor;
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -
        
        // -------------------------------------
        // Searching and tracking related
        // -------------------------------------

        // Positions are handled in the scene's world space.
        public void SetLinePositions(Vector3 startPosition, Vector3 endPosition)
        {
            this.m_TractorBeamLine.SetPosition(0, startPosition);
            this.m_TractorBeamLine.SetPosition(1, endPosition);
        }

        // Position should be in Screen Position
        public void UpdateCirclePosition(Vector2 circlePosition)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    this.m_CanvasRectTransform,
                    circlePosition,
                    this.m_Camera, // Set to null as the Canvas is done in Screen Overlay
                    out Vector2 _CanvasPosition)) return;
            
            this.m_TractorCircleTransform.anchoredPosition = _CanvasPosition;
        }
        
        public void UpdateCircleSize(float size)
            => this.m_TractorCircleTransform.localScale = new Vector3(size, size, size);

        public void UpdateBeamStrengthColor(float strength)
        {
            this.m_CurrentBeamColor = Color.Lerp(this.m_FullBeamStrengthColor, this.m_WeakBeamStrengthColor, strength);
            
            this.m_TractorBeamLine.startColor = this.m_CurrentBeamColor;
            this.m_TractorBeamLine.endColor = this.m_CurrentBeamColor;

            this.m_TractorCircle.color = this.m_CurrentBeamColor;
        }

        public void ShowCircle() 
            => this.m_TractorCircleTransform.gameObject.SetActive(true);

        public void HideCircle() 
            => this.m_TractorCircleTransform.gameObject.SetActive(false);

        public void ShowLineBeam() 
            => this.m_TractorBeamLine.gameObject.SetActive(true);

        public void HideLineBeam() 
            => this.m_TractorBeamLine.gameObject.SetActive(false);
        
        // -------------------------------------
        // Locked On Related
        // -------------------------------------
        
        public void UpdateRecticle(Vector2 recticlePosition)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    this.m_CanvasRectTransform,
                    recticlePosition,
                    this.m_Camera, // Set to null as the Canvas is done in Screen Overlay
                    out Vector2 _CanvasPosition)) return;
            
            this.m_TractorRecticleTransform.anchoredPosition = _CanvasPosition;
        }

        public void ShowRecticle() 
            => this.m_TractorRecticleTransform.gameObject.SetActive(true);

        public void HideRecticle() 
            => this.m_TractorRecticleTransform.gameObject.SetActive(false);
        
        // -------------------------------------
        // Out Of Range Related
        // -------------------------------------

        // The distance is passed as sqrmagnitude. Alter this so that its approximate to the magnitude distance.
        public void UpdateOutOfRangeCircleSize(float size) 
            => this.m_TractorMaxDistanceCircleTransform.localScale = new Vector3(size, size, 0);
        
        public void UpdateOutOfRangeCirclePosition(Vector2 screenPosition)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    this.m_CanvasRectTransform,
                    screenPosition,
                    this.m_Camera, // Set to null as the Canvas is done in Screen Overlay
                    out Vector2 _CanvasPosition)) return;
            
            this.m_TractorMaxDistanceCircleTransform.anchoredPosition = _CanvasPosition;
        }

        public void ShowOutOfRangeElements()
        {
            this.m_TractorMaxDistanceCircleTransform.gameObject.SetActive(true);
            this.m_OutOfRangeLabel.gameObject.SetActive(true);
        }

        public void HideOutofRangeElements()
        {
            this.m_TractorMaxDistanceCircleTransform.gameObject.SetActive(false);
            this.m_OutOfRangeLabel.gameObject.SetActive(false);
        }

        public void ShowHUD() 
            => this.m_ContentGroup.SetActive(true);

        public void HideHUD() 
            => this.m_ContentGroup.SetActive(false);

        #endregion Methods
  
    }

}