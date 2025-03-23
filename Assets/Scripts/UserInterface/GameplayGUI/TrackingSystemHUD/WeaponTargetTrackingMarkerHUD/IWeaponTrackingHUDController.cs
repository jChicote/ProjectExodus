using UnityEngine;

namespace ProjectExodus
{

    public interface IWeaponTrackingHUDController : IInitialize<WeaponTargetTrackingHUDData>
    {

        #region - - - - - - Methods - - - - - -

        void SetTargetCrosshairPosition(Vector2 screenPosition);

        void HideScreen();

        void ShowScreen();

        #endregion Methods

    }


}