using System.Collections.Generic;
using ProjectExodus;
using ProjectExodus.GameLogic.Player.PlayerProvider;
using UnityEngine;

namespace GameLogic.SetupHandlers.SceneHandlers
{

    public class EnemySetupHandler : MonoBehaviour, ISetupHandler
    {

        #region - - - - - - Fields - - - - - -

        private ISetupHandler m_NextHandler;

        public List<ZetoEnemySpawner> EnemySpawners;
        public PlayerProvider PlayerProvider;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        void ISetupHandler.SetNext(ISetupHandler next)
            => this.m_NextHandler = next;

        void ISetupHandler.Handle(SceneSetupInitializationContext initializationContext)
        {
            foreach (ZetoEnemySpawner _EnemySpawner in this.EnemySpawners)
                _EnemySpawner.Initialize(new() { PlayerProvider = PlayerProvider });
            
            initializationContext.LoadingScreenController.UpdateLoadProgress(80f);
            this.m_NextHandler?.Handle(initializationContext);
        }

        #endregion Methods
  
    }

}