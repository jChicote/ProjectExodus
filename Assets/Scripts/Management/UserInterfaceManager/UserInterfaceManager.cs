using System;
using ProjectExodus.UserInterface.Controllers;
using ProjectExodus.UserInterface.GameplayHUD;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectExodus.Management.UserInterfaceManager
{

    /// <summary>
    /// Responsible for high-level coordination of different UI views and encapsulating UI related components.
    /// </summary>
    public class UserInterfaceManager : MonoBehaviour, IUserInterfaceManager // No Need for interface
    {

        #region - - - - - - Fields - - - - - -

        private IUserInterfaceController m_UserInterfaceController;
        private GameplaySceneGUIControllers m_GameplaySceneGUIControllers;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public GameplaySceneGUIControllers GameplaySceneGUIControllers
            => this.m_GameplaySceneGUIControllers;

        #endregion Properties
  
        #region - - - - - - Unity Methods - - - - - -

        private void Awake()
        {
            UserInterfaceManager[] _UserInterfaceManager = Object.FindObjectsByType<UserInterfaceManager>(FindObjectsSortMode.None);
            if (_UserInterfaceManager.Length > 1)
                Debug.LogError($"Multiple {nameof(UserInterfaceManager)}s detected. " +
                               $"Only one {nameof(UserInterfaceManager)} should exist, unexpected behaviour will occur.");
        }

        #endregion Unity Methods
  
        #region - - - - - - Methods - - - - - -

        /// <remarks>
        /// This method is expensive, reserve for only transitions between scenes.
        /// </remarks>
        IUserInterfaceController IUserInterfaceManager.GetTheActiveUserInterfaceController()
        {
            this.m_UserInterfaceController = null;
            
            if (Object.FindAnyObjectByType<MainMenuInterfacesController>())
                this.m_UserInterfaceController = Object.FindFirstObjectByType<MainMenuInterfacesController>();
            else if (Object.FindAnyObjectByType<GameplayMenuInterfacesController>())
                this.m_UserInterfaceController = Object.FindFirstObjectByType<GameplayMenuInterfacesController>();
            
            if (this.m_UserInterfaceController == null)
                Debug.LogError("[ERROR] There are no User Interface Controllers found.");

            return this.m_UserInterfaceController;
        }
        
        #endregion Methods
  
    }

}