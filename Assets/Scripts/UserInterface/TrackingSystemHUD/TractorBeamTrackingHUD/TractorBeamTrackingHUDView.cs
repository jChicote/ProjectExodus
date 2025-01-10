using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus
{

    public class TractorBeamTrackingHUDView : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public GameObject m_ContentGroup;
        public Camera m_Camera;
        
        public Image m_TractorCircle;
        public RectTransform m_TractorCircleTransform;
        public Image m_TractorRecticle;
        public RectTransform m_TractorRecticleTransform;
        public LineRenderer m_TractorBeamLine;
        public RectTransform m_TractorMaxDistanceCircleTransform;
        public GameObject m_OutOfRangeLabel;

        public RectTransform m_CanvasRectTransform;

        public Color m_WeakBeamStrengthColor;
        public Color m_FullBeamStrengthColor;

        private Color m_CurrentBeamColor;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

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
        
        public void UpdateRecticle(Vector2 recticlePosition)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    this.m_CanvasRectTransform,
                    recticlePosition,
                    this.m_Camera, // Set to null as the Canvas is done in Screen Overlay
                    out Vector2 _CanvasPosition)) return;
            
            this.m_TractorRecticleTransform.anchoredPosition = _CanvasPosition;
        }

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

        public void ShowRecticle() 
            => this.m_TractorRecticleTransform.gameObject.SetActive(true);

        public void HideRecticle() 
            => this.m_TractorRecticleTransform.gameObject.SetActive(false);

        public void ShowLineBeam() 
            => this.m_TractorBeamLine.gameObject.SetActive(true);

        public void HideLineBeam() 
            => this.m_TractorBeamLine.gameObject.SetActive(false);

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