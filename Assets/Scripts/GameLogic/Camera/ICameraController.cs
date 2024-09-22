using UnityEngine;

namespace ProjectExodus.GameLogic.Camera
{

    public interface ICameraController
    {

        #region - - - - - - Methods - - - - - -

        void SetCameraFollowTarget(Transform target);

        #endregion Methods

    }

}