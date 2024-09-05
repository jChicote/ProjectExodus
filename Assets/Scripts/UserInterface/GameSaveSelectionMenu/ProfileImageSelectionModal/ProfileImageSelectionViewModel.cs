using System;
using System.Collections.Generic;
using ProjectExodus.Common.Infrastructure;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;
using ProjectExodus.Domain.Services;
using UserInterface.GameSaveSelectionMenu.Mediator;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.ProfileImageSelectionModal
{

    public class ProfileImageSelectionViewModel : IProfileImageSelectionModalNotifyEvents
    {

        #region - - - - - - Fields - - - - - -

        private readonly IGameSaveSelectionMenuMediator m_Mediator;
        private readonly IProfileImageModelProvider m_ProfileImageProvider;
        private readonly IProfileImageSelectionView m_ProfileImageSelectionView;

        private ICommand<ProfileImageModel> m_SelectProfileImageCommand;
        private ICommand m_SaveSelectionCommand;
        private ICommand m_ExitModalCommand;

        private ProfileImageModel m_SelectedImage;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public ProfileImageSelectionViewModel(
            IGameSaveSelectionMenuMediator gameSaveSelectionMenuMediator,
            IProfileImageModelProvider profileImageModelProvider,
            IProfileImageSelectionView profileImageSelectionView)
        {
            this.m_Mediator = gameSaveSelectionMenuMediator ??
                                throw new ArgumentNullException(nameof(gameSaveSelectionMenuMediator));
            this.m_ProfileImageProvider =
                profileImageModelProvider ?? throw new ArgumentNullException(nameof(profileImageModelProvider));
            
            if (profileImageSelectionView == null)
                throw new ArgumentNullException(nameof(profileImageSelectionView));

            this.BindLogicToCommands();
            this.RegisterViewModelActions();
            profileImageSelectionView.BindToViewModel(this);
        }

        #endregion Constructors
  
        #region - - - - - - Properties - - - - - -

        ICommand<ProfileImageModel> IProfileImageSelectionModalNotifyEvents.SelectProfileImageCommand
            => this.m_SelectProfileImageCommand;

        ICommand IProfileImageSelectionModalNotifyEvents.SaveSelectionCommand
            => this.m_SaveSelectionCommand;

        ICommand IProfileImageSelectionModalNotifyEvents.ExitModalCommand
            => this.m_ExitModalCommand;

        #endregion Properties

        #region - - - - - - Events - - - - - -
        
        public event Action<List<ProfileImageModel>> OnShowMenuModalWithImage;

        #endregion Events
  
        #region - - - - - - Methods - - - - - -

        // -------------------------------------
        // Setup Methods
        // -------------------------------------
        
        private void BindLogicToCommands()
        {
            this.m_SelectProfileImageCommand = new RelayCommand<ProfileImageModel>(this.SelectProfileImage);
            this.m_SaveSelectionCommand = new RelayCommand(this.SaveProfileImageSelection);
            this.m_ExitModalCommand = new RelayCommand(this.ExitModal);
        }

        private void RegisterViewModelActions() 
            => this.m_Mediator.Register(GameSaveMenuEventType.ProfileImageSelectionModal_Open, this.ShowModal);

        // -------------------------------------
        // Command Methods
        // -------------------------------------

        private void SelectProfileImage(ProfileImageModel profileImageModel) 
            => this.m_SelectedImage = profileImageModel;

        private void SaveProfileImageSelection() 
            => this.m_Mediator.Invoke(GameSaveMenuEventType.EditGameSlotImage_Update, this.m_SelectedImage);

        private void ExitModal()
        {
            this.m_SelectedImage = default;
            this.m_Mediator.Invoke(GameSaveMenuEventType.EditGameSlot_Open);
        }

        // -------------------------------------
        // ViewModel Actions
        // -------------------------------------

        private void ShowModal() 
            => this.OnShowMenuModalWithImage?.Invoke(this.m_ProfileImageProvider.ProvideAll());

        #endregion Methods
        
    }

}