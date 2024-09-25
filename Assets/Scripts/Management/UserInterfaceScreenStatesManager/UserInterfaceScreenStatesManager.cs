// using ProjectExodus.Management.Enumeration;
// using ProjectExodus.Management.Models;
// using ProjectExodus.StateManagement.ScreenStates;
// using UnityEngine;
//
// namespace ProjectExodus.Management.UserInterfaceScreenStatesManager
// {
//
//     /// <summary>
//     /// Manages the defined screens of the game.
//     /// </summary>
//     public class UserInterfaceScreenStateManager : MonoBehaviour, IUserInterfaceScreenStateManager
//     {
//
//         #region - - - - - - Fields - - - - - -
//
//         // UI Screen States
//         private GameSaveMenuScreenState m_GameSaveMenuScreenState;
//         private MainMenuScreenState m_MainMenuScreenState;
//         private OptionsMenuScreenState m_OptionsMenuScreenState;
//         private LoadingBarScreenState m_LoadingBarScreenState;
//         private GameplayHUDScreenState m_GameplayHUDScreenState;
//
//         private IScreenState m_CurrentScreenState;
//         private IScreenState m_PreviousScreenState;
//
//         #endregion Fields
//
//         #region - - - - - - Initializers - - - - - -
//
//         void IUserInterfaceScreenStateManager.InitialiseUserInterfaceScreenStatesManager(GameScreens gameScreens)
//         {
//             // this.m_GameSaveMenuScreenState = new GameSaveMenuScreenState(gameScreens.GameSaveSelectionMenuController);
//             // this.m_MainMenuScreenState = new MainMenuScreenState(gameScreens.MainMenuController);
//             // this.m_OptionsMenuScreenState = new OptionsMenuScreenState(gameScreens.OptionsMenuController);
//             // this.m_LoadingBarScreenState = new LoadingBarScreenState(gameScreens.LoadingScreenController);
//             // this.m_GameplayHUDScreenState = new GameplayHUDScreenState(gameScreens.GameplayHUDController);
//             
//             // Default opening game screen
//             ((IUserInterfaceScreenStateManager)this).OpenScreen(UIScreenType.GameSaveMenu);
//         }
//
//         #endregion Initializers
//
//         #region - - - - - - Methods - - - - - -
//
//         void IUserInterfaceScreenStateManager.OpenScreen(UIScreenType uiScreenType)
//         {
//             var _PreviousScreen = this.m_CurrentScreenState;
//             this.m_CurrentScreenState?.EndState();
//             
//             switch (uiScreenType)
//             {
//                 case UIScreenType.GameSaveMenu:
//                     this.m_CurrentScreenState = this.m_GameSaveMenuScreenState;
//                     break;
//                 case UIScreenType.MainMenu:
//                     this.m_CurrentScreenState = this.m_MainMenuScreenState;
//                     break;
//                 case UIScreenType.OptionsMenu:
//                     this.m_CurrentScreenState = this.m_OptionsMenuScreenState;
//                     break;
//                 case UIScreenType.LoadingScreen:
//                     this.m_CurrentScreenState = this.m_LoadingBarScreenState;
//                     break;
//                 case UIScreenType.GameplayHUD:
//                     this.m_CurrentScreenState = this.m_GameplayHUDScreenState;
//                     break;
//             }
//             
//             this.m_CurrentScreenState?.StartState();
//             this.m_PreviousScreenState = _PreviousScreen;
//         }
//
//         void IUserInterfaceScreenStateManager.OpenPreviousScreen()
//         {
//             this.m_CurrentScreenState?.EndState();
//             this.m_CurrentScreenState = this.m_PreviousScreenState;
//             this.m_CurrentScreenState.StartState();
//         }
//
//         #endregion Methods
//   
//     }
//     
// }