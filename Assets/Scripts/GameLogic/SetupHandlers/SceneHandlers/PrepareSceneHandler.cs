using ProjectExodus.Management.SceneManager;
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
            
            this.m_NextHandler?.Handle(initializationContext);
        }

        /// <summary>
        /// Ensures that existing services from the SceneSetupInitializationContext is applied.
        /// </summary>
        private void AssignToManagers(
            SceneSetupInitializationContext initializationContext)
        {
            SceneManager.Instance.PlayerObserver = initializationContext.PlayerObserver;
        }

        #endregion Methods
  
    }

}