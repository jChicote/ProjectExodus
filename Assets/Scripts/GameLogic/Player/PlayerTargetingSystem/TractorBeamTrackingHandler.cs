using System.Collections;
using ProjectExodus.UserInterface.TrackingSystemHUD.TractorBeamTrackingHUD;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerTargetingSystem
{

    // Actions of this behaviour
    // 1. Await for ctrl to be pressed to activate the general track
    // 2. Await for hover to exceed set time over an interactable object type
    // 3. Start the track
    // 4. Draw the beam line and the outline to the object (of blue shade)
    // 5. Move the target object's position to inverse-smooth-damp move to player's position
    // 6. Once object within a 'near radius', end the track.
    public class TractorBeamTrackingHandler
    {

        #region - - - - - - Fields - - - - - -
        
        private UnityEngine.Camera m_Camera;
        private int m_TargetObjectID;
        private Transform m_TargetTransform;
        private TractorBeamTrackingHUDController m_TractorBeamTrackingHUDController;
        
            
        private float m_TargetingLockOnTimeLength = 8f; // TODO: Change to use the virtual weight of the object.
        private bool m_CanTractorTrack;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public TractorBeamTrackingHandler()
        {
            
        }

        #endregion Constructors
  
        #region - - - - - - Properties - - - - - -

        public bool CanTrack => this.m_CanTractorTrack;

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        public void TrackTarget()
        {
            
        }
        
        public void SetNewTarget(GameObject newTarget) 
            => this.m_TargetTransform = newTarget.transform;

        // Note: This time length should be changed to tractor based on the virtual weight of the object.
        public IEnumerator StartTargeting()
        {
            yield return new WaitForSeconds(this.m_TargetingLockOnTimeLength);
            
            Debug.Log("RunTractorLocking is started.");
            this.m_CanTractorTrack = true;
        }

        public void EndTargeting()
        {
            
        }
        
        #endregion Methods
  
    }

}