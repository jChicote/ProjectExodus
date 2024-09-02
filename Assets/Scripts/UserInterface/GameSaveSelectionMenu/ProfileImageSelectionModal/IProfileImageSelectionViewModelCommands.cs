using System;
using System.Collections.Generic;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.ProfileImageSelectionModal
{

    public interface IProfileImageSelectionViewModelCommands
    {

        #region - - - - - - Events - - - - - -

        event Action<List<ProfileImageModel>> OnShowMenuModalWithImage;
        
        #endregion Events
  
        #region - - - - - - Properties - - - - - -

        ICommand<ProfileImageModel> SelectProfileImageCommand { get; }
        
        ICommand SaveSelectionCommand { get; }
        
        ICommand ExitModalCommand { get; }
        
        #endregion Properties

    }

}