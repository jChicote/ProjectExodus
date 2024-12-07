using UnityEngine;

namespace ProjectExodus.UserInterface.TrackingSystemHUD.TargetTrackingHUD
{

    public class TargetTrackingHUDController : MonoBehaviour, ITrackingHUDController, IScreenStateController
    {

        #region - - - - - - Fields - - - - - -

        private TargetTrackingHUDView m_View;

        #endregion Fields
  
        #region - - - - - - Initializers - - - - - -

        void ITrackingHUDController.Initialize() 
            => this.m_View = this.GetComponent<TargetTrackingHUDView>();

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        public void SetTargetCrosshairPosition(Vector2 screenPosition) 
            => this.m_View.UpdateCrosshairPosition(screenPosition);

        void IScreenStateController.HideScreen() 
            => this.m_View.HideGUI();

        void IScreenStateController.ShowScreen() 
            => this.m_View.ShowGUI();

        #endregion Methods
  
    }

}