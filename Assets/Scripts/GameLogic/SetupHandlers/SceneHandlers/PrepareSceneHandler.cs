using ProjectExodus;
using ProjectExodus.Management.SceneManager;
using ProjectExodus.Utility.GameLogging;
using UnityEngine;

namespace GameLogic.SetupHandlers.SceneHandlers
{

    public class PrepareSceneHandler : MonoBehaviour, ISetupHandler
    {

        #region - - - - - - Fields - - - - - -

        private ISetupHandler m_NextHandler;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        void ISetupHandler.SetNext(ISetupHandler next)
            => this.m_NextHandler = next;

        void ISetupHandler.Handle(SceneSetupInitializationContext initializationContext)
        {
            this.AssignToManagers(initializationContext);
            
            initializationContext.InputManager.DisableActiveInputControl();
            initializationContext.LoadingScreenController.UpdateLoadProgress(20);
            
            GameLogger.Log("PrepareSceneHandler has run.");
            this.m_NextHandler?.Handle(initializationContext);
        }

        /// <summary>
        /// Ensures that existing services from the SceneSetupInitializationContext is applied.
        /// </summary>
        private void AssignToManagers(
            SceneSetupInitializationContext initializationContext)
        {
            SceneManager.Instance.PlayerObserver = initializationContext.PlayerObserver;
            EnemyManager.Instance.EnemyObserver = initializationContext.EnemyObserver;
        }

        #endregion Methods
  
    }

}