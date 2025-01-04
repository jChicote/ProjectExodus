using System;
using ProjectExodus.GameLogic.Enumeration;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.UserInterface.TrackingSystemHUD.TargetTrackingHUD;
using ProjectExodus.Utility.GameLogging;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerTargetingSystem
{

    public class PlayerTargetingSystem : PausableMonoBehavior, IPlayerTargetingSystem
    {

        #region - - - - - - Fields - - - - - -
        
        // This should be handled by settings or player data
        [SerializeField] private float m_PointerRange = 2f;

        private UnityEngine.Camera m_Camera;
        
        // Targeting Handlers
        private WeaponTargetingHandler m_WeaponTargetingHandler;
        private TractorBeamTrackingHandler m_TractorBeamTargetingHandler;

        // Target Object Fields
        private GameObject m_PossibleTarget;
        private Vector3 m_MouseWorldPosition;
        private Vector2 m_MouseWorldPosition2D;
        
        private bool m_IsTrackingEnabled;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IPlayerTargetingSystem.Initialize(UnityEngine.Camera camera)
        {
            this.m_Camera = camera ?? throw new ArgumentNullException(nameof(camera));
            
            this.m_WeaponTargetingHandler = 
                new WeaponTargetingHandler(
                    this.m_PointerRange, 
                    this.m_Camera,
                    FindFirstObjectByType<TargetTrackingHUDController>());
            this.m_TractorBeamTargetingHandler = new TractorBeamTrackingHandler(this.transform);
        }

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        private void Update()
        {
            if (this.m_IsPaused 
                || this.m_WeaponTargetingHandler == null
                || this.m_TractorBeamTargetingHandler == null)
                // || !GameValidator.NotNull(this.m_WeaponTargetingHandler, "", false)
                // || !GameValidator.NotNull(this.m_TractorBeamTargetingHandler, "", false)) 
                return;
            
            this.m_WeaponTargetingHandler.TrackTarget();
            // this.m_TractorBeamTargetingHandler.TrackTarget();
        }

        #endregion Methods
  
        #region - - - - - - Methods - - - - - -

        void IPlayerTargetingSystem.ConfirmTargetLock()
        {
            if (!this.m_IsTrackingEnabled || this.m_PossibleTarget == null)
            {
                this.m_WeaponTargetingHandler.EndWeaponTargeting();
                // this.m_TractorBeamTargetingHandler.EndWeaponTargeting();
                return;
            }
            
            // TODO: Move to WeaponTargeting            
            if (this.m_PossibleTarget.tag == GameTag.Enemy && this.m_WeaponTargetingHandler.CurrentTargetEnemy != null)
            {
                bool _IsTargetReselected = this.m_PossibleTarget.GetInstanceID() ==
                                           this.m_WeaponTargetingHandler.CurrentTargetEnemy.gameObject.GetInstanceID();
                GameLogger.Log((nameof(_IsTargetReselected), _IsTargetReselected));
                if (_IsTargetReselected)
                {
                    this.m_WeaponTargetingHandler.EndWeaponTargeting();
                    return;
                    
                }
            }
            else if (this.m_PossibleTarget.tag == GameTag.Interactable)
            {
                // No behavior implemented yet.
            }
            
            this.StartTargetingAction(this.m_PossibleTarget);
        }

        void IPlayerTargetingSystem.ActivateTargeting() 
            => this.m_IsTrackingEnabled = true;

        void IPlayerTargetingSystem.DeactivateTargeting() 
            => this.m_IsTrackingEnabled = false;

        void IPlayerTargetingSystem.SearchForTarget(Vector2 screenPosition)
        {
            if (!this.m_IsTrackingEnabled) return;
            
            // Prevent targeting from disengaging if mouse exits the targeting area.
            if (this.m_WeaponTargetingHandler.CanTrack || this.m_TractorBeamTargetingHandler.CanTrack) return;
            
            this.m_MouseWorldPosition = this.m_Camera.ScreenToWorldPoint(
                new Vector3(screenPosition.x, screenPosition.y, 0));
            this.m_MouseWorldPosition2D = new Vector2(m_MouseWorldPosition.x, m_MouseWorldPosition.y);
            this.m_WeaponTargetingHandler.SetPointerPosition(this.m_MouseWorldPosition2D);
            
            
            // Trace through scene
            RaycastHit2D _RaycastHit = Physics2D.Raycast(this.m_MouseWorldPosition2D, Vector2.zero, 0);
            if (!_RaycastHit)
            {
                this.m_PossibleTarget = null;
                return;
            }
            
            GameLogger.Log(
                (nameof(m_IsTrackingEnabled), m_IsTrackingEnabled),
                (nameof(_RaycastHit), _RaycastHit));
            
            this.m_PossibleTarget = _RaycastHit.collider.gameObject;
        }

        private void StartTargetingAction(GameObject hitObject)
        {
            Debug.Log(hitObject.tag);
            // TODO: Move to WeaponTargeting
            if (hitObject.tag == GameTag.Enemy)
            {
                this.m_WeaponTargetingHandler.SetNewTarget(hitObject);
                this.m_WeaponTargetingHandler.StartWeaponTargeting();
            }
            // TODO: Move to TractorBeamTracking
            else if (hitObject.tag == GameTag.Interactable)
            {
                this.m_TractorBeamTargetingHandler.SetNewTarget(hitObject);
                this.StartCoroutine(this.m_TractorBeamTargetingHandler.StartTargeting());
            }
        }

        #endregion Methods

        #region - - - - - - Gizmos - - - - - -

        private void OnDrawGizmos()
        {
            this.m_WeaponTargetingHandler.CalculateDrawGizmos(out Vector2 _TransformPosition, out float _Magnitude);

            if (this.m_WeaponTargetingHandler.CanTrack)
            {
                // Draw circular bounds
                Gizmos.color = new Color(255, 0, 0, 1);
                Gizmos.DrawWireSphere(_TransformPosition, _Magnitude);
                
                // Draw circular distance
                Gizmos.color = new Color(0, 255, 255, 1);
                Gizmos.DrawWireSphere(_TransformPosition, _Magnitude);
            }
        }

        #endregion Gizmos
  
    }

}