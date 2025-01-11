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
            initializationContext.InputManager.DisableActiveInputControl();
            initializationContext.LoadingScreenController.UpdateLoadProgress(20);
            
            this.m_NextHandler?.Handle(initializationContext);
        }

        #endregion Methods
  
    }

}