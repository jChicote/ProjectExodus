using ProjectExodus.Management.Models;
using ProjectExodus.Management.UserInterfaceScreenStatesManager;
using ProjectExodus.UserInterface.MainMenu;
using ProjectExodus.UserInterface.OptionsMenu;
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
        [SerializeField] private OptionsMenuController m_OptionsMenuController;

        [Header("Sub-Managers")]
        [SerializeField] private UserInterfaceScreenStateManager m_UserInterfaceScreenStateManager;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        IUserInterfaceScreenStateManager IUserInterfaceManager.UserInterfaceScreenStateManager
            => this.m_UserInterfaceScreenStateManager;

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

        #region - - - - - - Initializers - - - - - -

        private void InitialiseUserIterfaces()
        {
            ((IMainMenuController)this.m_MainMenuController).InitialiseMainMenuController();
            ((IOptionsMenuController)this.m_OptionsMenuController).InitialiseOptionsMenu(
                GameManager.Instance.DataContext,
                GameManager.Instance.GameSettings.GameOptionsModel,
                GameManager.Instance.GameOptionsFacade,
                GameManager.Instance.Mapper,
                this.m_UserInterfaceScreenStateManager);
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void IUserInterfaceManager.InitialiseUserInterfaceManager()
        {
            this.InitialiseUserIterfaces();

            GameScreens _GameScreens = new GameScreens(
                this.m_MainMenuController,
                this.m_OptionsMenuController);
            ((IUserInterfaceManager)this).UserInterfaceScreenStateManager
                .InitialiseUserInterfaceScreenStatesManager(_GameScreens);
        }

        #endregion Methods
  
    }

}