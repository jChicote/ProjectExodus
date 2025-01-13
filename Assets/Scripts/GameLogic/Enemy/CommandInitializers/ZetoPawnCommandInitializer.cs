using System;
using System.Linq;
using ProjectExodus.Common.Services;
using UnityEngine;

namespace ProjectExodus
{
    
    public class ZetoPawnCommandInitializer : MonoBehaviour, ICommand
    {

        #region - - - - - - Fields - - - - - -

        private EnemySettings m_EnemySettings;
        private EnemyAssetObject m_EnemySpawnData;

        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        // Must resolve dependencies without a context object, to ensure the spawner does not include
        //  specific logic to setup dependencies for each enemy type.;
        private void Awake()
        {
            EnemyManager _EnemyManager = EnemyManager.Instance;
            
            this.m_EnemySettings = _EnemyManager.EnemySettings;
            this.m_EnemySpawnData =
                _EnemyManager.EnemySettings.Enemies.Single(eao => eao.Name == EnemyConstants.ZetoPawn); // TODO: Change from constants to SmartEnum
        }

        #endregion Unity Methods
  
        #region - - - - - - Methods - - - - - -
        
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public bool CanExecute()
        {
            throw new NotImplementedException();
        }

        #endregion Methods
  
    }

}