using UnityEngine;
using UnityEngine.Events;

namespace ProjectExodus
{

    public class EnemyManager : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public static EnemyManager Instance;

        [RequiredField] 
        [SerializeField] 
        public EnemySettings EnemySettings;

        public EnemyObserver EnemyObserver;

        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        #endregion Unity Methods
  
    }

}