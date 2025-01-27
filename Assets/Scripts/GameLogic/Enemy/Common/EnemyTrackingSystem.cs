using ProjectExodus.Management.SceneManager;
using UnityEngine;

namespace ProjectExodus
{

    public interface IEnemyTrackingSystem
    {
    }
    
    public class EnemyTrackingSystem : MonoBehaviour, IEnemyTrackingSystem, IInitialize
    {

        #region - - - - - - Fields - - - - - -

        private GameObject m_TrackedTarget;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        public void Initialize()
        {
            PlayerObserver _PlayerObserver = SceneManager.Instance.PlayerObserver;
            _PlayerObserver.OnPlayerSpawned.AddListener(this.TrackTarget);
            _PlayerObserver.OnPlayerDeath.AddListener(this.ClearTracking);
        }

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        private void TrackTarget(GameObject target) 
            => this.m_TrackedTarget = target;

        private void ClearTracking() 
            => this.m_TrackedTarget = null;

        #endregion Methods
  
    }

}