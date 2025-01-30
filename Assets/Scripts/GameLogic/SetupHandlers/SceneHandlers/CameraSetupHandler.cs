using UnityEngine;

namespace GameLogic.SetupHandlers.SceneHandlers
{

    public class CameraSetupHandler : MonoBehaviour, ISetupHandler
    {

        #region - - - - - - Fields - - - - - -

        private ISetupHandler m_NextHandler;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        public void SetNext(ISetupHandler next) 
            => this.m_NextHandler = next;

        public void Handle(SceneSetupInitializationContext initializationContext)
        {
            this.SetupCameraControl(initializationContext);
            
            this.m_NextHandler?.Handle(initializationContext);
        }
        
        private void SetupCameraControl(SceneSetupInitializationContext initializationContext) 
            => initializationContext.CameraController.Initialize(new());

        #endregion Methods
  
    }

}