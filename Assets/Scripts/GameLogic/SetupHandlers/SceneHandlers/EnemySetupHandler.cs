using UnityEngine;

namespace GameLogic.SetupHandlers.SceneHandlers
{

    public class EnemySetupHandler : MonoBehaviour, ISetupHandler
    {

        #region - - - - - - Fields - - - - - -

        private ISetupHandler m_NextHandler;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        void ISetupHandler.SetNext(ISetupHandler next)
            => this.m_NextHandler = next;

        void ISetupHandler.Handle(SceneSetupInitializationContext initializationContext)
        {
            initializationContext.LoadingScreenController.UpdateLoadProgress(80f);
            this.m_NextHandler?.Handle(initializationContext);
        }

        #endregion Methods
  
    }

}