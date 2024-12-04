using System;
using System.Collections;
using ProjectExodus.GameLogic.Enumeration;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using UnityEngine;

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

        // Target Object Fields
        private Transform m_TargetTransform;
        private int m_TargetObjectID;
        private float m_TargetSqrMagnitudeDistance;
        private Vector2 m_MouseWorkPosition2D;
        private Vector3 m_MouseWorldPosition;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IPlayerTargetingSystem.Initialize(UnityEngine.Camera camera)
        {
            this.m_Camera = camera ?? throw new ArgumentNullException(nameof(camera));
        }

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        void IPlayerTargetingSystem.SearchForTarget(Vector2 screenPosition)
        {
            this.m_MouseWorldPosition = this.m_Camera.ScreenToWorldPoint(
                new Vector3(screenPosition.x, screenPosition.y, 0));
            this.m_MouseWorkPosition2D = new Vector2(m_MouseWorldPosition.x, m_MouseWorldPosition.y);

            // Track the hit
            if (this.m_TargetTransform)
            {
                this.m_TargetSqrMagnitudeDistance = 
                    (new Vector2(
                        x: this.m_TargetTransform.position.x, 
                        y: this.m_TargetTransform.position.y) - this.m_MouseWorkPosition2D)
                    .sqrMagnitude;

                if (!(this.m_TargetSqrMagnitudeDistance > this.m_PointerRange * this.m_PointerRange)) 
                    return;
                
                this.ResetTargetingSystem();
            }
            
            // Trace through scene
            RaycastHit2D _RaycastHit = Physics2D.Raycast(this.m_MouseWorkPosition2D, Vector2.zero, 0);
            if (!_RaycastHit)
                this.ResetTargetingSystem();
            
            else if (_RaycastHit.collider.gameObject.GetInstanceID() != this.m_TargetObjectID) 
                this.RunTargetingAction(_RaycastHit.collider.gameObject);
        }

        private IEnumerator RunTargetingSystem()
        {
            yield return new WaitForSeconds(this.m_TargetingLockOnTimeLength);
            Debug.Log("RunTractorLocking is started.");
        }

        private IEnumerator RunTractorLockingSystem()
        {
            yield return new WaitForSeconds(this.m_TargetingLockOnTimeLength);
            Debug.Log("RunTractorLocking is started.");
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
                this.StartCoroutine(this.RunTargetingSystem());
            }
            else if (hitObject.tag == GameTag.Interactable)
            {
                this.SetNewTarget(hitObject);
                this.StartCoroutine(this.RunTractorLockingSystem());
            }
        }

        private void ResetTargetingSystem()
        {
            this.m_MouseWorkPosition2D = Vector2.zero;
            this.m_TargetObjectID = 0;
            this.m_TargetTransform = null;
            
            this.StopCoroutine(this.RunTargetingSystem());
            this.StopCoroutine(this.RunTractorLockingSystem());
        }

        #endregion Methods

        #region - - - - - - Gizmos - - - - - -

        private void OnDrawGizmos()
        {
            if (!this.m_TargetTransform) return;
            
            float _SqrMagnitude = 
                ((new Vector2(this.m_TargetTransform.position.x, this.m_TargetTransform.position.y) - this.m_MouseWorkPosition2D))
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