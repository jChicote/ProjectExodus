using System;
using System.Collections.Generic;
using ProjectExodus.Common.Infrastructure;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;
using ProjectExodus.Domain.Services;
using UserInterface.GameSaveSelectionMenu.Mediator;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.ProfileImageSelectionModal
{

    public class ProfileImageSelectionViewModel : IProfileImageSelectionViewModelCommands
    {

        #region - - - - - - Fields - - - - - -

        private ICommand<ProfileImageModel> m_SelectProfileImageCommand;
        private ICommand m_SaveSelectionCommand;
        private ICommand m_ExitModalCommand;

        private readonly IGameSaveSelectionMenuMediator m_Mediator;
        private readonly IProfileImageModelProvider m_ProfileImageProvider;
        private readonly IProfileImageSelectionView m_ProfileImageSelectionView;

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
;           this.m_ProfileImageSelectionView = 
                profileImageSelectionView ?? throw new ArgumentNullException(nameof(profileImageSelectionView));

            this.BindInteractionMethodsToCommands();
            this.RegisterViewModelActions();
        }

        #endregion Constructors

        #region - - - - - - Events - - - - - -
        
        public event Action<List<ProfileImageModel>> OnShowMenuModalWithImage;
        

        #endregion Events
  
        #region - - - - - - Properties - - - - - -

        ICommand<ProfileImageModel> IProfileImageSelectionViewModelCommands.SelectProfileImageCommand
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
            this.m_SelectProfileImageCommand = new RelayCommand<ProfileImageModel>(this.SelectProfileImage);
            this.m_SaveSelectionCommand = new RelayCommand(this.SaveProfileImageSelection);
            
            this.m_ProfileImageSelectionView.BindToViewModel(this);
        }

        private void RegisterViewModelActions() 
            => this.m_Mediator.Register(GameSaveMenuEventType.ShowProfileImageSelectionMenu, this.ShowModal);

        // -------------------------------------
        // Command Methods
        // -------------------------------------

        private void SelectProfileImage(ProfileImageModel profileImageModel) 
            => this.m_SelectedImage = profileImageModel;

        private void SaveProfileImageSelection() 
            => this.m_Mediator.Invoke(GameSaveMenuEventType.UpdateProfileImageSelection, this.m_SelectedImage);

        // -------------------------------------
        // ViewModel Action Methods
        // -------------------------------------

        private void ShowModal() 
            => this.OnShowMenuModalWithImage?.Invoke(this.m_ProfileImageProvider.ProvideAll());

        #endregion Methods
  
    }

}