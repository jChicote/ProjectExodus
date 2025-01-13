using System;
using ProjectExodus.Common.Services;
using UnityEngine;

namespace ProjectExodus
{

    public interface IEnemySpawner
    {
        
    }
    
    public class ZetoEnemySpawner : BaseEnemySpawner
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_EnemyTemplate;
        [SerializeField] private Transform m_SpawnPoint;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public Transform SpawnPoint
            => this.m_SpawnPoint;

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        public void SpawnEnemy(GameObject spawnTemplate)
        {
            // GameObject _SpawnedEnemy = Instantiate(spawnTemplate, this.m_SpawnPoint.position, Quaternion.identity);
            // ICommand _CommandInitializer = _SpawnedEnemy.GetComponent<ICommand>();
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

}