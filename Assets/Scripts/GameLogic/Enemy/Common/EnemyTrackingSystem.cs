using MBT;
using ProjectExodus.GameLogic.Scene;
using ProjectExodus.Management.SceneManager;
using UnityEngine;

namespace ProjectExodus
{

    public class EnemyTrackingSystemConstants
    {

        #region - - - - - - Fields - - - - - -

        public const string TrackedTargetTransform = "TargetTransform";

        #endregion Fields

    }
    
    public class EnemyTrackingSystem : MonoBehaviour, IInitialize
    {

        #region - - - - - - Fields - - - - - -

        [RequiredField]
        [SerializeField]
        private Blackboard m_Blackboard;
        
        private TransformVariable m_TargetTrackingVariable;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        public void Initialize()
        {
            this.m_TargetTrackingVariable = this.m_Blackboard
                .GetVariable<TransformVariable>(EnemyTrackingSystemConstants.TrackedTargetTransform);
            
            // Subscribe tracking to player observer
            PlayerObserver _PlayerObserver = SceneManager.Instance.PlayerObserver;
            _PlayerObserver.OnPlayerSpawned.AddListener(this.TrackTarget);
            _PlayerObserver.OnPlayerDeath.AddListener(this.ClearTracking);

            // TODO: The scene controller should only be part of the spawner
            ISceneController _SceneController = SceneManager.Instance.GetActiveSceneController();
            this.TrackTarget(_SceneController.PlayerProvider.GetActivePlayer());
        }

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        private void TrackTarget(GameObject target) 
            => this.m_TargetTrackingVariable.Value = target.transform;

        private void ClearTracking() 
            => this.m_TargetTrackingVariable.Value = null;

        #endregion Methods
  
    }

}