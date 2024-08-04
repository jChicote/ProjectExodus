using ProjectExodus.UserInterface.MainMenu;
using UnityEngine;

namespace ProjectExodus.Management.UserInterfaceManager
{

    /// <summary>
    /// Responsible for high-level coordination of different UI views and encapsulating UI related components.
    /// </summary>
    public class UserInterfaceManager : MonoBehaviour, IUserInterfaceManager
    {

        #region - - - - - - Fields - - - - - -

        [Header("GUI Controllers")]
        [SerializeField] private MainMenuController m_MainMenuController;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        IMainMenuController IUserInterfaceManager.MainMenuController
            => this.m_MainMenuController;

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

        void IUserInterfaceManager.InitialiseUserInterfaceManager() 
            => this.InitialiseUserIterfaces();

        private void InitialiseUserIterfaces() 
            => ((IMainMenuController)this.m_MainMenuController).InitialiseMainMenuController();

        #endregion Methods
  
    }

}