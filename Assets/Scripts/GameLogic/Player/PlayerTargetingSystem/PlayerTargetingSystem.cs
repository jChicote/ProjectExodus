using System;
using System.Collections;
using Codice.CM.Common;
using ProjectExodus.GameLogic.Enumeration;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.UserInterface.TrackingSystemHUD.TargetTrackingHUD;
using ProjectExodus.UserInterface.TrackingSystemHUD.TractorBeamTrackingHUD;
using UnityEngine;
using Object = System.Object;

namespace ProjectExodus.GameLogic.Player.PlayerTargetingSystem
{

    public interface IPlayerTargetingSystem
    {

        #region - - - - - - Initializers - - - - - -

        void Initialize(UnityEngine.Camera camera);

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void SearchForTarget(Vector2 screenPosition);

        #endregion Methods

    }
    
    public class PlayerTargetingSystem : PausableMonoBehavior, IPlayerTargetingSystem
    {

        #region - - - - - - Fields - - - - - -
        
        // This should be handled by settings or player data
        [SerializeField] private float m_TargetingLockOnTimeLength = 8f;
        [SerializeField] private float m_PointerRange = 2f;

        private UnityEngine.Camera m_Camera;
        private TargetTrackingHUDController m_TargetTrackingHUDController;
        private TractorBeamTrackingHUDController m_TractorBeamTrackingHUDController;

        // Target Object Fields
        private Transform m_TargetTransform;
        private int m_TargetObjectID;
        private float m_TargetSqrMagnitudeDistance;
        private Vector3 m_MouseWorldPosition;
        private Vector2 m_MouseWorldPosition2D;

        private bool m_CanTargetTrack;
        private bool m_CanTractorTrack;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IPlayerTargetingSystem.Initialize(UnityEngine.Camera camera)
        {
            this.m_Camera = camera ?? throw new ArgumentNullException(nameof(camera));

            this.m_TargetTrackingHUDController = FindFirstObjectByType<TargetTrackingHUDController>();
            // this.m_TractorBeamTrackingHUDController = FindFirstObjectByType<TractorBeamTrackingHUDController>();
        }

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        private void Update()
        {
            if (this.m_IsPaused) return;
            
            this.TrackTarget();
            this.TrackTractoredObject();
        }

        #endregion Methods
  
        #region - - - - - - Methods - - - - - -

        void IPlayerTargetingSystem.SearchForTarget(Vector2 screenPosition)
        {
            this.m_MouseWorldPosition = this.m_Camera.ScreenToWorldPoint(
                new Vector3(screenPosition.x, screenPosition.y, 0));
            this.m_MouseWorldPosition2D = new Vector2(m_MouseWorldPosition.x, m_MouseWorldPosition.y);

            // Track the hit
            if (this.m_TargetTransform)
            {
                this.m_TargetSqrMagnitudeDistance = 
                    (new Vector2(
                        x: this.m_TargetTransform.position.x, 
                        y: this.m_TargetTransform.position.y) - this.m_MouseWorldPosition2D)
                    .sqrMagnitude;

                if (!(this.m_TargetSqrMagnitudeDistance > this.m_PointerRange * this.m_PointerRange)) 
                    return;
                
                this.ResetTargetingSystem();
            }
            
            // Trace through scene
            RaycastHit2D _RaycastHit = Physics2D.Raycast(this.m_MouseWorldPosition2D, Vector2.zero, 0);
            if (!_RaycastHit)
                this.ResetTargetingSystem();
            
            else if (_RaycastHit.collider.gameObject.GetInstanceID() != this.m_TargetObjectID) 
                this.RunTargetingAction(_RaycastHit.collider.gameObject);
        }

        private void RunTargetingSystem()
        {
            Debug.Log("RunTractorLocking is started.");
            this.m_CanTargetTrack = true;
        }

        private IEnumerator RunTractorLockingSystem()
        {
            yield return new WaitForSeconds(this.m_TargetingLockOnTimeLength);
            Debug.Log("RunTractorLocking is started.");
            this.m_CanTractorTrack = true;
        }

        private void TrackTarget()
        {
            if (!this.m_CanTargetTrack) return;
            
            // Check that the target has not been lost
            
            this.m_TargetTrackingHUDController.SetTargetCrosshairPosition(this.m_MouseWorldPosition2D);
        }

        private void TrackTractoredObject()
        {
            if (!this.m_CanTractorTrack) return;
            
            // Check that the tractored object is not lost
            this.m_CanTractorTrack = false;
        }

        private void SetNewTarget(GameObject newTarget)
        {
            this.m_TargetObjectID = newTarget.GetInstanceID();
            this.m_TargetTransform = newTarget.transform;
        }

        private void RunTargetingAction(GameObject hitObject)
        {
            if (hitObject.tag == GameTag.Enemy)
            {
                this.SetNewTarget(hitObject);
                this.RunTargetingSystem();
            }
            else if (hitObject.tag == GameTag.Interactable)
            {
                this.SetNewTarget(hitObject);
                this.StartCoroutine(this.RunTractorLockingSystem());
            }
        }

        private void ResetTargetingSystem()
        {
            this.m_MouseWorldPosition2D = Vector2.zero;
            this.m_TargetObjectID = 0;
            this.m_TargetTransform = null;
            
            this.StopCoroutine(this.RunTractorLockingSystem());
        }

        #endregion Methods

        #region - - - - - - Gizmos - - - - - -

        private void OnDrawGizmos()
        {
            if (!this.m_TargetTransform) return;
            
            float _SqrMagnitude = 
                ((new Vector2(
                    x: this.m_TargetTransform.position.x, 
                    y: this.m_TargetTransform.position.y) - this.m_MouseWorldPosition2D))
                .magnitude;
            
            // Draw circular bounds
            Gizmos.color = new Color(255, 0, 0, 1);
            Gizmos.DrawWireSphere(this.m_TargetTransform.position, this.m_PointerRange);
            
            // Draw circular distance
            Gizmos.color = new Color(0, 255, 255, 1);
            Gizmos.DrawWireSphere(this.m_TargetTransform.position, _SqrMagnitude);
        }

        #endregion Gizmos
  
    }

}