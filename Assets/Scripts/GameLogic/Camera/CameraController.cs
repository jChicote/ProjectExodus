using Cinemachine;
using UnityEngine;

namespace ProjectExodus.GameLogic.Camera
{

    public class CameraController : MonoBehaviour, ICameraController
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private CinemachineVirtualCamera m_VirtualCamera;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        void ICameraController.SetCameraFollowTarget(Transform target)
            => this.m_VirtualCamera.Follow = target;

        #endregion Methods

    }

}