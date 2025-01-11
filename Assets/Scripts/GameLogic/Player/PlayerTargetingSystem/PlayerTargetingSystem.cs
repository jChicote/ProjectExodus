using System;
using ProjectExodus.GameLogic.Enumeration;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerTargetingSystem
{

    public class PlayerTargetingSystem : 
        PausableMonoBehavior, 
        IDebuggingDataProvider<PlayerTargetingSystemDebuggingData>,
        IPlayerTargetingSystem
    {

        #region - - - - - - Fields - - - - - -

        // Dependent components
        private UnityEngine.Camera m_Camera;
        private IPlayerTargetingHUDController m_HUDController;
        
        // Targeting Handlers
        private WeaponTargetingHandler m_WeaponTargetingHandler;
        private TractorBeamTrackingHandler m_TractorBeamTargetingHandler;

        // Target Object Fields
        public GameObject m_PossibleTarget;
        private Vector2 m_MouseScreenPosition;
        private Vector3 m_MouseWorldPosition;
        private Vector2 m_MouseWorldPosition2D;
        
        private bool m_IsTrackingEnabled;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IPlayerTargetingSystem.Initialize(
            UnityEngine.Camera camera, 
            IPlayerTargetingHUDController playerTargetingHUDController)
        {
            this.m_Camera = camera ?? throw new ArgumentNullException(nameof(camera));
            this.m_HUDController = playerTargetingHUDController ??
                                   throw new ArgumentNullException(nameof(playerTargetingHUDController));

            this.m_WeaponTargetingHandler = this.GetComponent<WeaponTargetingHandler>();
            this.m_TractorBeamTargetingHandler = this.GetComponent<TractorBeamTrackingHandler>();
        }

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        private void Update()
        {
            if (this.m_IsPaused 
                || this.m_WeaponTargetingHandler == null
                || this.m_TractorBeamTargetingHandler == null)
                return;
            
            // Recalculating at runtime to prevent indicator from being stuck on camera move.
            if (this.m_IsTrackingEnabled)
            {
                Vector3 _MouseRuntimeWorldPosition = this.m_Camera.ScreenToWorldPoint(this.m_MouseScreenPosition);
                Vector3 _RelativeOffset = _MouseRuntimeWorldPosition - this.m_Camera.transform.position;
                Vector3 _UpdateWorldPosition = this.m_Camera.transform.position + _RelativeOffset;
                this.m_HUDController.UpdateHoverTargetingIndicatorPosition(
                    this.m_Camera.WorldToScreenPoint(_UpdateWorldPosition));
                
                this.m_HUDController.UpdateIndicatorPresentation(this.m_PossibleTarget 
                    ? this.m_PossibleTarget.tag 
                    : string.Empty);
            }
            
            this.m_WeaponTargetingHandler.TrackTarget();
            this.m_TractorBeamTargetingHandler.TrackCurrentTarget();
        }

        #endregion Methods
  
        #region - - - - - - Methods - - - - - -

        PlayerTargetingSystemDebuggingData IDebuggingDataProvider<PlayerTargetingSystemDebuggingData>.GetData() 
            => new()
            {
                PossibleTarget = this.m_PossibleTarget
            };

        void IPlayerTargetingSystem.ConfirmTargetLock()
        {
            if (!this.m_IsTrackingEnabled || this.m_PossibleTarget == null)
            {
                this.m_WeaponTargetingHandler.EndTargeting();
                return;
            }
            
            if (this.m_PossibleTarget.tag == GameTag.Interactable)
                this.m_TractorBeamTargetingHandler.StartHoverTargetLock(this.m_PossibleTarget);
            else if (this.m_PossibleTarget.tag == GameTag.Enemy)
                this.m_WeaponTargetingHandler.StartTargeting(this.m_PossibleTarget);
        }

        void IPlayerTargetingSystem.ActivateTargeting()
        {
            this.m_IsTrackingEnabled = true;
            this.m_HUDController.ShowHoverTargetingIndicator();
        }

        void IPlayerTargetingSystem.DeactivateTargeting()
        {
            this.m_IsTrackingEnabled = false;
            this.m_HUDController.HideHoverTargetingIndicator();
        }

        // Major limitation is this only is performed when the mouse moves. Avoid running calculations that required runtime.
        void IPlayerTargetingSystem.SearchForTarget(Vector2 screenPosition)
        {
            if (!this.m_IsTrackingEnabled) return;

            this.m_MouseScreenPosition = screenPosition;
            this.m_MouseWorldPosition = this.m_Camera.ScreenToWorldPoint(
                new Vector3(screenPosition.x, screenPosition.y, 0));
            this.m_MouseWorldPosition2D = new Vector2(m_MouseWorldPosition.x, m_MouseWorldPosition.y);
            
            // Trace through scene
            RaycastHit2D _RaycastHit = Physics2D.Raycast(this.m_MouseWorldPosition2D, Vector2.zero, 0);
            if (!_RaycastHit)
                this.m_PossibleTarget = null;
            else if (this.m_PossibleTarget == null
                || _RaycastHit.collider.gameObject.GetInstanceID() != this.m_PossibleTarget.GetInstanceID()) 
                this.m_PossibleTarget = _RaycastHit.collider.gameObject;
        }

        #endregion Methods
  
    }

    public class PlayerTargetingSystemDebuggingData
    {

        #region - - - - - - Properties - - - - - -

        public GameObject PossibleTarget { get; set; }

        #endregion Properties

    }

}