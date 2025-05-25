using System;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.EditGameSlotModal
{

    public interface IEditGameSlotNotifyEvents
    {

        #region - - - - - - Properties - - - - - -

        ICommand CreateGameSlotCommand { get; }
        
        ICommand<string> EditDisplayNameCommand { get; }
        
        ICommand ExitModalCommand { get; }
        
        ICommand SaveGameSlotCommand { get; }
        
        ICommand SelectProfileImageCommand { get; }

        #endregion Properties

        #region - - - - - - Events - - - - - -

        event Action<string> OnDisplayNameChanged;

        event Action OnEnableEditGameSlotModal;
        
        event Action<ProfileImageModel> OnSelectedImageChanged;
        
        event Action<bool> OnShowEditGameSlotModal;
        
        #endregion Events
  
    }

}