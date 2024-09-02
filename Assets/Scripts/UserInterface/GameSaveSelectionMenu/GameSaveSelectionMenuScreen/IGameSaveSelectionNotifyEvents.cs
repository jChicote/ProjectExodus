using System;
using ProjectExodus.Common.Services;

namespace UserInterface.GameSaveSelectionMenu.GameSaveSelectionMenuScreen
{

    public interface IGameSaveSelectionNotifier
    {

        #region - - - - - - Properties - - - - - -

        public ICommand CreateNewGameCommand { get; }
        
        public ICommand ClearGameSaveSlotCommand { get; }
        
        public ICommand EditGameSaveSlotCommand { get; }
        
        public ICommand QuitGameCommand { get; }

        #endregion Properties

        #region - - - - - - Events - - - - - -

        event Action OnDisableViewInteraction;

        event Action OnEnableViewInteraction;
        
        event Action OnShowEmptySlotButtonOptions;
        
        event Action OnShowEditSlotButtonOptions;

        #endregion Events

    }

}