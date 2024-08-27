using System;
using UserInterface.GameSaveSelectionMenu.GameSaveSelectionMenuScreen;

namespace UserInterface.GameSaveSelectionMenu.Mediator
{

    public class GameSaveSelectionMenuMediator : IGameSaveSelectionMenuMediator
    {
        private GameSaveSelectionMenuViewModel m_GameSaveSelectionMenuViewModel;

        public GameSaveSelectionMenuMediator(
            GameSaveSelectionMenuViewModel gameSaveSelectionMenuViewModel)
        {
            this.m_GameSaveSelectionMenuViewModel = gameSaveSelectionMenuViewModel ??
                                                        throw new ArgumentNullException(
                                                            nameof(gameSaveSelectionMenuViewModel));
        }

        public void Execute()
        {
            
        }
    }

    public class GameSaveSelectionMenuMediatorConfig
    {
        
    }

}