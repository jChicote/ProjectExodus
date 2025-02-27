using UnityEngine;
using UnityEngine.Events;

namespace ProjectExodus
{

    public class PlayerObserver : MonoBehaviour, IPlayerObserver
    {

        #region - - - - - - Fields - - - - - -

        private readonly UnityEvent<GameObject> m_OnPlayerSpawned = new();
        private readonly UnityEvent m_OnPlayerDeath = new();

        #endregion Fields
  
        #region - - - - - - Properties - - - - - -

        public UnityEvent<GameObject> OnPlayerSpawned
            => this.m_OnPlayerSpawned;

        public UnityEvent OnPlayerDeath
            => this.m_OnPlayerDeath;

        #endregion Properties
  
    }

}