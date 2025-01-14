using ProjectExodus;
using ProjectExodus.GameLogic.Player.PlayerProvider;
using UnityEngine;

namespace GameLogic.SetupHandlers.SceneHandlers
{

    public class EnemySetupHandler : MonoBehaviour, ISetupHandler
    {

        #region - - - - - - Fields - - - - - -

        private ISetupHandler m_NextHandler;

        public ZetoEnemySpawner EnemySpawner;
        public PlayerProvider PlayerProvider;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        void ISetupHandler.SetNext(ISetupHandler next)
            => this.m_NextHandler = next;

        void ISetupHandler.Handle(SceneSetupInitializationContext initializationContext)
        {
            this.EnemySpawner.Initialize(new()
            {
                PlayerProvider = PlayerProvider
            });
            
            initializationContext.LoadingScreenController.UpdateLoadProgress(80f);
            this.m_NextHandler?.Handle(initializationContext);
        }

        #endregion Methods
  
    }

}