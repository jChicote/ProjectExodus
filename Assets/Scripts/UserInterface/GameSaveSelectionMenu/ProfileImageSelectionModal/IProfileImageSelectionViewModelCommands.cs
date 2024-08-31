using System;
using System.Collections.Generic;
using ProjectExodus.Common.Services;
using UnityEngine;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.ProfileImageSelectionModal
{

    public interface IProfileImageSelectionViewModelCommands
    {

        #region - - - - - - Events - - - - - -

        event Action<Dictionary<int, Sprite>> OnShowMenuModalWithImage;

        #endregion Events
  
        #region - - - - - - Properties - - - - - -

        ICommand<int> SelectProfileImageCommand { get; }
        
        ICommand SaveSelectionCommand { get; }
        
        ICommand ExitModalCommand { get; }
        
        #endregion Properties

    }

}