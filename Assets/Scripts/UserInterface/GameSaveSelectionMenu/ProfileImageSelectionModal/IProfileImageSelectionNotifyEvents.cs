using System;
using System.Collections.Generic;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.ProfileImageSelectionModal
{

    public interface IProfileImageSelectionModalNotifyEvents
    {

        #region - - - - - - Events - - - - - -

        event Action<List<ProfileImageModel>> OnShowMenuModalWithImage;
        
        #endregion Events
  
        #region - - - - - - Properties - - - - - -

        ICommand HideModalCommand { get;  }
        
        ICommand<ProfileImageModel> SelectProfileImageCommand { get; }
        
        ICommand SaveSelectionCommand { get; }
        
        #endregion Properties

    }

}