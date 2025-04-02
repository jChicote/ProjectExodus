using System;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Common.Timers;
using ProjectExodus.GameLogic.Enumeration;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.GameLogic.Player.PlayerProvider;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectExodus
{

    [Obsolete]
    public class ZetoEnemySpawner_Old : PausableMonoBehavior, IInitialize<ZetoEnemySpawnerInitializerData>
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_EnemyTemplate;
        [SerializeField] private Transform m_SpawnPoint;
        
        // Change to serialized
        public float m_SpawnInterval;
        public float m_SpawnRadius;
        public EventTimer m_SpawnTimer;
        public CircleCollider2D m_DetectionCollider;

        private bool m_IsSpawnerActive;
        private IPlayerProvider m_PlayerProvider;
        private Transform m_PlayerTransform;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public Transform SpawnPoint
            => this.m_SpawnPoint;

        #endregion Properties

        #region - - - - - - Initializers - - - - - -

        public void Initialize(ZetoEnemySpawnerInitializerData initializerData)
        {
            this.m_SpawnTimer = new(this.m_SpawnInterval, Time.deltaTime, this.SpawnEnemy);
            this.m_PlayerProvider = initializerData.PlayerProvider ??
                                    throw new ArgumentNullException(nameof(initializerData.PlayerProvider));

            this.m_DetectionCollider.radius = this.m_SpawnRadius;
        }

        #endregion Initializers

        #region - - - - - - Unity Events - - - - - -

        private void Update()
        {
            if (this.m_IsPaused || !this.m_IsSpawnerActive) return;

            this.m_SpawnTimer.TickTimer();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag != GameTag.Player) return;
            this.StartSpawner();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag != GameTag.Player) return;
            this.StopSpawner();
        }

        #endregion Unity Events
  
        #region - - - - - - Methods - - - - - -

        public void SpawnEnemy()
        {
            Vector2 _RandomizedSpawnPoint = new Vector2(
                Random.Range(this.transform.position.x - this.m_SpawnRadius, this.transform.position.x + this.m_SpawnRadius),
                Random.Range(this.transform.position.y - this.m_SpawnRadius, this.transform.position.y + this.m_SpawnRadius));
            
            GameObject _SpawnedEnemy = Instantiate(this.m_EnemyTemplate, _RandomizedSpawnPoint, Quaternion.identity);
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
            Gizmos.DrawWireCube(this.m_SpawnPoint.position, new Vector3(this.m_SpawnRadius * 2, this.m_SpawnRadius * 2, this.m_SpawnRadius * 2));
            Gizmos.DrawWireSphere(this.transform.position, 0.5f);
        }
        
        #endregion Gizmos
  
    }

    public class ZetoEnemySpawnerInitializerData
    {

        #region - - - - - - Properties - - - - - -

        public IPlayerProvider PlayerProvider { get; set; }

        #endregion Properties
  
    }

}