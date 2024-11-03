using ProjectExodus.Management.Enumeration;
using ProjectExodus.StateManagement.ScreenStates;
using UnityEngine;

namespace ProjectExodus.UserInterface.Controllers
{

    public class GameplayMenuInterfacesController : MonoBehaviour, IUserInterfaceController
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private LoadingBarScreenState m_LoadingBarScreenState;
        [SerializeField] private GameplayHUDScreenState m_GameplayHudScreenState;
        
        private IScreenState m_CurrentScreenState;
        private IScreenState m_PreviousScreenState;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        void IUserInterfaceController.InitialiseUserInterfaceController() 
            => ((IScreenState)this.m_GameplayHudScreenState).Initialize();

        void IUserInterfaceController.OpenScreen(UIScreenType uiScreenType)
        {
            var _PreviousScreen = this.m_CurrentScreenState;
            this.m_CurrentScreenState?.EndState();

            this.m_CurrentScreenState = uiScreenType switch
            {
                UIScreenType.LoadingScreen => this.m_LoadingBarScreenState,
                UIScreenType.GameplayHUD => this.m_GameplayHudScreenState,
                _ => this.m_CurrentScreenState
            };

            this.m_CurrentScreenState?.StartState();
            this.m_PreviousScreenState = _PreviousScreen;
        }

        void IUserInterfaceController.OpenPreviousScreen()
        {
            this.m_CurrentScreenState?.EndState();
            this.m_CurrentScreenState = this.m_PreviousScreenState;
            this.m_CurrentScreenState.StartState();
        }

        bool IUserInterfaceController.TryGetInterfaceController(out object interfaceController)
        {
            interfaceController = this.m_CurrentScreenState?.GetInterfaceController();
            return interfaceController == null;
        }

        #endregion Methods

    }

}