using ProjectExodus;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.Utility.GameLogging;
using UnityEngine;

namespace GameLogic.SetupHandlers.SceneHandlers
{

    public class FinalizeSceneSetupHandler : MonoBehaviour, ISetupHandler
    {

        #region - - - - - - Fields - - - - - -

        private ISetupHandler m_NextHandler;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        void ISetupHandler.SetNext(ISetupHandler next)
            => this.m_NextHandler = next;

        void ISetupHandler.Handle(SceneSetupInitializationContext initializationContext)
        {
            // Validate Management
            _ = GameManager.Instance.IsMembersValid();
            _ = UserInterfaceManager.Instance.IsMembersValid();
            
            initializationContext.LoadingScreenController.HideScreen();
            initializationContext.LoadingScreenController.ResetLoadingScreen();
            initializationContext.InputManager.EnableActiveInputControl();
            initializationContext.LoadingScreenController.UpdateLoadProgress(100);

            GameLogger.Log("FinalizeSceneSetupHandler has run.");
            this.m_NextHandler?.Handle(initializationContext);
        }

        #endregion Methods
  
    }

}