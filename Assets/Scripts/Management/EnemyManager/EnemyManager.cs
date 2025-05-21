using UnityEngine;
using UnityEngine.Events;

namespace ProjectExodus
{

    public class EnemyManager : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public static EnemyManager Instance;

        [RequiredField, SerializeField] public EnemySettings EnemySettings;
        [RequiredField] public EnemyObserver EnemyObserver;
        [RequiredField] public EnemyCollection EnemyCollection;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public IEnemySpawner EnemySpawner { get; set; }

        #endregion Properties
        
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