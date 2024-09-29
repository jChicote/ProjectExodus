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
            => this.LoadPersistenceScene();

        // private void Update()
        // {
        //     if (!CanLoadInitialScene) return;
        //     this.LateOpenInitialScreen();
        // }

        #endregion Unity Lifecycle Methods

        #region - - - - - - Methods - - - - - -

        public override void ActivateSceneObjects()
        {
            base.ActivateSceneObjects();
            
            IUserInterfaceController _UserInterfaceController =
                Object.FindFirstObjectByType<MainMenuInterfacesController>();
            _UserInterfaceController.InitialiseUserInterfaceController(); 
            _UserInterfaceController.OpenScreen(UIScreenType.GameSaveMenu);
        }

        /// <remark>
        /// This can be late opened to ensure all initialisation logic is handled correctly.
        /// </remark>
        private void LateOpenInitialScreen()
        {
            // startup the main menu behaviour
            IUserInterfaceController _UserInterfaceController =
                Object.FindFirstObjectByType<MainMenuInterfacesController>();
            _UserInterfaceController.InitialiseUserInterfaceController(); 
            _UserInterfaceController.OpenScreen(UIScreenType.GameSaveMenu);
            
            // Disable to ensure no further Update is performed
            this.enabled = false;
        }

        #endregion Methods
  
    }

}