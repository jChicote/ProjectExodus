using UnityEngine;

namespace ProjectExodus.UserInterface.TrackingSystemHUD.TargetTrackingHUD
{

    public interface ITrackingHUDController
    {

        #region - - - - - - Initializers - - - - - -

        void Initialize();

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        void SetTargetCrosshairPosition(Vector2 screenPosition);

        #endregion Methods

    }

}