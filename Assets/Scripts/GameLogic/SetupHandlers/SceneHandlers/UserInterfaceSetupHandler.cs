using ProjectExodus;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.UserInterface.Controllers;
using ProjectExodus.Utility.GameLogging;
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
            IUserInterfaceManager _UserInterfaceManager = UserInterfaceManager.Instance;
            
            IUserInterfaceController _ActiveUserInterfaceController =
                _UserInterfaceManager.GetTheActiveUserInterfaceController();
            _ActiveUserInterfaceController.InitialiseUserInterfaceController();
            _ActiveUserInterfaceController.OpenScreen(UIScreenType.GameplayHUD);
            initializationContext.ActiveUserInterfaceController = _ActiveUserInterfaceController;
            
            // TODO: Change this so that during the pipeline if it fails, a warning popup is presented to the Player.
            if (!_ActiveUserInterfaceController.TryGetGUIControllers(out object _Controllers))
            {
                Debug.LogError("[ERROR]: No GameplayHUDController is found. Aborting setup pipeline.");
                return;
            }

            // Initialise targeting HUD
            PlayerTargetingHUDData _PlayerTargetingHUDData = new()
            {
                Camera = initializationContext.MainCamera,
                UserInterfaceSettings = initializationContext.UserInterfaceSettings
            };
            ((GameplaySceneGUIControllers)_Controllers).PlayerTargetingHUDController.Initialize(_PlayerTargetingHUDData);

            TractorBeamTrackingHUDData _TractorBeamTrackingHUDData = new()
            {
                Camera = initializationContext.MainCamera,
                UserInterfaceSettings = initializationContext.UserInterfaceSettings
            };
            ((GameplaySceneGUIControllers)_Controllers).TractorBeamTrackingHUDController.Initialize(_TractorBeamTrackingHUDData);

            WeaponTargetTrackingHUDData _WeaponTargetTrackingHUDData = new();
            _WeaponTargetTrackingHUDData.Camera = initializationContext.MainCamera;
            ((GameplaySceneGUIControllers)_Controllers).WeaponTrackingHUDController.Initialize(_WeaponTargetTrackingHUDData);

            initializationContext.LoadingScreenController.UpdateLoadProgress(40);
            
            GameLogger.Log("UserInterfaceSetupHandler has run.");
            this.m_NextHandler?.Handle(initializationContext);
        }

        private void SetupEventMediator()
        {
            
        }

        #endregion Methods
  
    }

}