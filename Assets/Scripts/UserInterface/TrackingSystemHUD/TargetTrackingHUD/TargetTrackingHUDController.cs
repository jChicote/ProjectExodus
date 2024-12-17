using UnityEngine;

namespace ProjectExodus.UserInterface.TrackingSystemHUD.TargetTrackingHUD
{

    public class TargetTrackingHUDController : MonoBehaviour, ITrackingHUDController
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

        public void HideScreen() 
            => this.m_View.HideGUI();

        public void ShowScreen() 
            => this.m_View.ShowGUI();

        #endregion Methods
  
    }

}