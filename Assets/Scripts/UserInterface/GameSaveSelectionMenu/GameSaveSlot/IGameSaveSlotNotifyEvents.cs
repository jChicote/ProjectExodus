using System;
using ProjectExodus.Common.Services;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.GameSaveSlot
{

    public interface IGameSaveSlotNotifyEvents
    {

        #region - - - - - - Properties - - - - - -

        ICommand SlotSelectionCommand { get; }

        #endregion Properties

        #region - - - - - - Events - - - - - -

        event Action<bool> OnDisplayGameSaveSlot;
        
        event Action<string, object> OnPropertyChangeEvent;

        #endregion Events

    }

}