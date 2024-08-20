using ProjectExodus.GameLogic.Scene;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.InputManager;
using ProjectExodus.Management.SceneManager;
using ProjectExodus.Management.UserInterfaceScreenStatesManager;

namespace ProjectExodus.GameLogic.GameStates.GameplayState
{

    public class GameplayState : IGameState
    {

        #region - - - - - - Fields - - - - - -

        private readonly IInputManager m_InputManager;
        private readonly ISceneManager m_SceneManager;
        private readonly IUserInterfaceScreenStateManager m_UserInterfaceStateManager;

        #endregion Fields
  
        #region - - - - - - Constructor - - - - - -

        public GameplayState(
            IInputManager inputManager, 
            ISceneManager sceneManager,
            IUserInterfaceScreenStateManager userInterfaceScreenStateManager)
        {
            this.m_InputManager = inputManager;
            this.m_SceneManager = sceneManager;
            this.m_UserInterfaceStateManager = userInterfaceScreenStateManager;
        }

        #endregion Constructor
  
        #region - - - - - - Methods - - - - - -

        void IGameState.StartState()
        {
            // Activate game input
            this.m_InputManager.PossesGameplayInputControls();
            this.m_InputManager.SwitchToGameplayInputControls();

            // Startup the scene
            ISceneController _ActiveSceneController = this.m_SceneManager.GetActiveSceneController();
            _ActiveSceneController.InitialiseSceneController();
            _ActiveSceneController.RunSceneStartup();
            
            this.m_UserInterfaceStateManager.OpenScreen(UIScreenType.GameplayHUD);
        }

        void IGameState.EndState() 
            => this.m_InputManager.UnpossesGameplayInputControls();

        #endregion Methods
  
    }

}