using ProjectExodus.Management.Enumeration;
using ProjectExodus.UserInterface.Controllers;
using UnityEngine;

namespace ProjectExodus.DebugSupport.SceneStartup
{

    public class DebugMainMenuStartupSupport : DebugSceneStartupSupport
    {

        #region - - - - - - Fields - - - - - -

        private bool CanLoadInitialScene;

        #endregion Fields
  
        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Awake()
        {
            // If the game manager support existed before the scene support... The scene debug should not run.
            if (this.ValidateSupportRequirements()) return;
            
            this.LoadPersistenceScene();
        }

        #endregion Unity Lifecycle Methods

        #region - - - - - - Methods - - - - - -

        public override void ActivateScene()
        {
            base.ActivateScene();
            
            IUserInterfaceController _UserInterfaceController =
                Object.FindFirstObjectByType<MainMenuInterfacesController>();
            _UserInterfaceController.InitialiseUserInterfaceController(); 
            _UserInterfaceController.OpenScreen(UIScreenType.GameSaveMenu);
        }

        #endregion Methods
  
    }

}