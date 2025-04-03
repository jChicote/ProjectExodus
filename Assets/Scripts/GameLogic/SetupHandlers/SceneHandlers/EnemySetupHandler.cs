using ProjectExodus;
using ProjectExodus.GameLogic.Player.PlayerProvider;
using ProjectExodus.Utility.GameLogging;
using UnityEngine;

namespace GameLogic.SetupHandlers.SceneHandlers
{

    public class EnemySetupHandler : MonoBehaviour, ISetupHandler
    {

        #region - - - - - - Fields - - - - - -

        public PlayerProvider PlayerProvider;

        private ISetupHandler m_NextHandler;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        void ISetupHandler.SetNext(ISetupHandler next)
            => this.m_NextHandler = next;

        void ISetupHandler.Handle(SceneSetupInitializationContext initializationContext)
        {
            initializationContext.LoadingScreenController.UpdateLoadProgress(80f);
            
            // Set required values for Enemy Manager
            EnemyManager _EnemyManager = EnemyManager.Instance;
            _EnemyManager.EnemySpawner = initializationContext.EnemySpawnerController;
            
            GameLogger.Log("EnemySetupHandler has run.");
            this.m_NextHandler?.Handle(initializationContext);
        }

        #endregion Methods
  
    }

}