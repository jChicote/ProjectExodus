using UnityEngine;

namespace ProjectExodus
{

    public class WeaponTargetWeaponTrackingHUDController : MonoBehaviour, IWeaponTrackingHUDController
    {

        #region - - - - - - Fields - - - - - -

        private WeaponTargetTrackingHUDView m_View;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IWeaponTrackingHUDController.Initialize()
            => this.m_View = this.GetComponent<WeaponTargetTrackingHUDView>();

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

}
