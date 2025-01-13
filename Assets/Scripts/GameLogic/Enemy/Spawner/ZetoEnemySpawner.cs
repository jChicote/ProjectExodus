using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Common.Timers;
using UnityEngine;

namespace ProjectExodus
{

    public interface IEnemySpawner
    {

        #region - - - - - - Methods - - - - - -

        void StartSpawner();
        
        void StopSpawner();

        #endregion Methods
  
    }
    
    public class ZetoEnemySpawner : BaseEnemySpawner, IEnemySpawner, IInitialize<ZetoEnemySpawnerInitializerData>
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_EnemyTemplate;
        [SerializeField] private Transform m_SpawnPoint;
        
        // Change to serialized
        public float m_SpawnInterval;
        public EventTimer m_SpawnTimer;

        private bool m_IsSpawnerActive;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public Transform SpawnPoint
            => this.m_SpawnPoint;

        #endregion Properties

        #region - - - - - - Initializers - - - - - -

        public void Initialize(ZetoEnemySpawnerInitializerData initializerData) 
            => this.m_SpawnTimer = new(this.m_SpawnInterval, Time.deltaTime, this.SpawnEnemy);

        #endregion Initializers

        #region - - - - - - Unity Events - - - - - -

        private void Update()
        {
            if (this.m_IsPaused || this.m_IsSpawnerActive) return;
            
            this.m_SpawnTimer.TickTimer();
        }

        #endregion Unity Events
  
        #region - - - - - - Methods - - - - - -

        public void SpawnEnemy()
        {
            GameObject _SpawnedEnemy = Instantiate(this.m_EnemyTemplate, this.m_SpawnPoint.position, Quaternion.identity);
            ICommand _CommandInitializer = _SpawnedEnemy.GetComponent<ICommand>();
            _CommandInitializer.Execute();
        }

        public void StartSpawner() => this.m_IsSpawnerActive = true;

        public void StopSpawner()
        {
            this.m_IsSpawnerActive = false;
            this.m_SpawnTimer.ResetTimer();
        }

        #endregion Methods

        #region - - - - - - Gizmos - - - - - -

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.m_SpawnPoint.position, 2f);
        }

        #endregion Gizmos
  
    }

    public class ZetoEnemySpawnerInitializerData
    {
    }

}