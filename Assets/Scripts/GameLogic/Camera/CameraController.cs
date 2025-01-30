using Cinemachine;
using ProjectExodus.Management.SceneManager;
using UnityEngine;

namespace ProjectExodus.GameLogic.Camera
{

    public class CameraController : 
        MonoBehaviour, 
        ICameraController, 
        IInitialize<CameraControllerInitializerData>
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private CinemachineVirtualCamera m_VirtualCamera;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        public void Initialize(CameraControllerInitializerData initializerData)
        {
            IPlayerObserver _PlayerObserver = SceneManager.Instance.PlayerObserver;
            _PlayerObserver.OnPlayerSpawned.AddListener(newPlayer => this.SetCameraFollowTarget(newPlayer.transform));
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        public void SetCameraFollowTarget(Transform target)
            => this.m_VirtualCamera.Follow = target;

        #endregion Methods

    }

    public class CameraControllerInitializerData
    {
    }

}