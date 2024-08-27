using System;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.UserInterface.GameSaveSelectionMenu;
using UserInterface.GameSaveSelectionMenu.Mediator;

namespace UserInterface.GameSaveSelectionMenu.GameSaveSelectionMenuScreen
{

    public class GameSaveSelectionMenuViewModel
    {

        #region - - - - - - Fields - - - - - -

        private readonly IGameSaveFacade m_GameSaveFacade;
        private readonly GameSaveSelectionMenuView m_GameSaveSelectionMenuView;
        private readonly IGameSaveSelectionMenuMediator m_Mediator;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameSaveSelectionMenuViewModel(
            IGameSaveFacade gameSaveFacade,
            IGameSaveSelectionMenuMediator gameSaveSelectionMenuMediator,
            GameSaveSelectionMenuView gameSaveSelectionMenuView)
        {
            this.m_GameSaveFacade = gameSaveFacade ?? throw new ArgumentNullException(nameof(gameSaveFacade));
            this.m_GameSaveSelectionMenuView = gameSaveSelectionMenuView ??
                                               throw new ArgumentNullException(nameof(gameSaveSelectionMenuView));
            this.m_Mediator = gameSaveSelectionMenuMediator ??
                              throw new ArgumentNullException(nameof(gameSaveSelectionMenuMediator));
        }

        #endregion Constructors

        #region - - - - - - Events - - - - - -

        

        #endregion Events
  
        #region - - - - - - Methods - - - - - -

        

        #endregion Methods
  
    }

}