using UnityEngine;

namespace ProjectExodus
{

    public class EnemyManager : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public static EnemyManager Instance;

        [RequiredField] 
        [SerializeField] 
        public EnemySettings EnemySettings;

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