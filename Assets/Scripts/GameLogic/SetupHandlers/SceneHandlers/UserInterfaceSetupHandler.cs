using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.UserInterface.Controllers;
using UnityEngine;

namespace GameLogic.SetupHandlers.SceneHandlers
{

    public class UserInterfaceSetupHandler : MonoBehaviour, ISetupHandler
    {

        #region - - - - - - Fields - - - - - -

        private ISetupHandler m_NextHandler;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        void ISetupHandler.SetNext(ISetupHandler next)
            => this.m_NextHandler = next;

        void ISetupHandler.Handle(SceneSetupInitializationContext initializationContext)
        {
            IUserInterfaceManager _UserInterfaceManager = 
                initializationContext.ServiceLocator.GetService<IUserInterfaceManager>();
            IUserInterfaceController _ActiveUserInterfaceController =
                _UserInterfaceManager.GetTheActiveUserInterfaceController();
            
            _ActiveUserInterfaceController.InitialiseUserInterfaceController();
            _ActiveUserInterfaceController.OpenScreen(UIScreenType.GameplayHUD);
            initializationContext.ActiveUserInterfaceController = _ActiveUserInterfaceController;

            initializationContext.LoadingScreenController.UpdateLoadProgress(40);
            
            this.m_NextHandler?.Handle(initializationContext);
        }

        #endregion Methods
  
    }

}