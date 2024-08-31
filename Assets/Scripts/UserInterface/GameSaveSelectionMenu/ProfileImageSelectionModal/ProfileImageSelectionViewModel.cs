using System;
using System.Collections.Generic;
using ProjectExodus.Common.Infrastructure;
using ProjectExodus.Common.Services;
using ProjectExodus.ScriptableObjects;
using UnityEngine;
using UserInterface.GameSaveSelectionMenu.Mediator;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.ProfileImageSelectionModal
{

    public class ProfileImageSelectionViewModel : IProfileImageSelectionViewModelCommands
    {

        #region - - - - - - Fields - - - - - -

        private ICommand<int> m_SelectProfileImageCommand;
        private ICommand m_SaveSelectionCommand;
        private ICommand m_ExitModalCommand;

        private readonly IGameSaveSelectionMenuMediator m_Mediator;
        private readonly IProfileImageSelectionView m_ProfileImageSelectionView;
        private readonly UserInterfaceSettings m_UserInterfaceSettings;

        private int m_SelectedImageID;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public ProfileImageSelectionViewModel(
            IGameSaveSelectionMenuMediator gameSaveSelectionMenuMediator,
            IProfileImageSelectionView profileImageSelectionView,
            UserInterfaceSettings userInterfaceSettings)
        {
            this.m_Mediator = gameSaveSelectionMenuMediator ??
                                throw new ArgumentNullException(nameof(gameSaveSelectionMenuMediator));
;           this.m_ProfileImageSelectionView = 
                profileImageSelectionView ?? throw new ArgumentNullException(nameof(profileImageSelectionView));
            this.m_UserInterfaceSettings =
                userInterfaceSettings ?? throw new ArgumentNullException(nameof(userInterfaceSettings));

            this.BindInteractionMethodsToCommands();
            this.RegisterViewModelActions();
        }

        #endregion Constructors

        #region - - - - - - Events - - - - - -
        
        public event Action<Dictionary<int, Sprite>> OnShowMenuModalWithImage;

        #endregion Events
  
        #region - - - - - - Properties - - - - - -

        ICommand<int> IProfileImageSelectionViewModelCommands.SelectProfileImageCommand
            => this.m_SelectProfileImageCommand;

        ICommand IProfileImageSelectionViewModelCommands.SaveSelectionCommand
            => this.m_SaveSelectionCommand;

        ICommand IProfileImageSelectionViewModelCommands.ExitModalCommand
            => this.m_ExitModalCommand;

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        // -------------------------------------
        // Initialization Methods
        // -------------------------------------
        
        private void BindInteractionMethodsToCommands()
        {
            this.m_SelectProfileImageCommand = new RelayCommand<int>(this.SelectProfileImage);
            this.m_SaveSelectionCommand = new RelayCommand(this.SaveProfileImageSelection);
            
            this.m_ProfileImageSelectionView.BindToViewModel(this);
        }

        private void RegisterViewModelActions() 
            => this.m_Mediator.Register(GameSaveMenuEventType.ShowProfileImageSelectionMenu, this.ShowModal);

        // -------------------------------------
        // Interaction Command Methods
        // -------------------------------------

        private void SelectProfileImage(int imageID) 
            => this.m_SelectedImageID = imageID;

        private void SaveProfileImageSelection() 
            => this.m_Mediator.Invoke(GameSaveMenuEventType.UpdateProfileImageSelection, this.m_SelectedImageID);

        // -------------------------------------
        // View Model Action Methods
        // -------------------------------------

        private void ShowModal() 
            => this.OnShowMenuModalWithImage?.Invoke(this.m_UserInterfaceSettings.ProfileImages);

        #endregion Methods
  
    }

}