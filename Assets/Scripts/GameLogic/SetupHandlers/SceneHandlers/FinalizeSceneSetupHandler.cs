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
            this.ValidateManagementLayer();
            this.ValidateServices();
            
            initializationContext.LoadingScreenController.HideScreen();
            initializationContext.LoadingScreenController.ResetLoadingScreen();
            initializationContext.InputManager.EnableActiveInputControl();

            GameLogger.Log("FinalizeSceneSetupHandler has run.");
            initializationContext.LoadingScreenController.UpdateLoadProgress(100);
            this.m_NextHandler?.Handle(initializationContext);
        }

        private void ValidateManagementLayer()
        {
            _ = GameManager.Instance.Validate();
            _ = UserInterfaceManager.Instance.Validate();
        }

        private void ValidateServices()
        {
            
        }

        #endregion Methods
  
    }

}