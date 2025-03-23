using UnityEngine;

namespace ProjectExodus
{

    public class WeaponTargetWeaponTrackingHUDController : 
        MonoBehaviour, 
        IWeaponTrackingHUDController
    {

        #region - - - - - - Fields - - - - - -

        private WeaponTargetTrackingHUDView m_View;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        public void Initialize(WeaponTargetTrackingHUDData initializerData)
        {
            this.m_View = this.GetComponent<WeaponTargetTrackingHUDView>();
            this.m_View.Initialize(initializerData);
        }

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        public void SetTargetCrosshairPosition(Vector2 screenPosition)
            => this.m_View.UpdateCrosshairPosition(screenPosition);

        public void HideScreen()
            => this.m_View.HideTargetRecticle();

        public void ShowScreen()
            => this.m_View.ShowTargetRecticle();

        #endregion Methods

    }

    public class WeaponTargetTrackingHUDData
    {

        #region - - - - - - Properties - - - - - -

        public Camera Camera { get; set; }

        #endregion Properties
  
    }

}
