using System;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;
using ProjectExodus.Common.Infrastructure;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Mappers;
using UnityEngine;
using UserInterface.GameSaveSelectionMenu.Mediator;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.EditGameSlotModal
{

    public class EditGameSlotViewModel
    {

        #region - - - - - - Fields - - - - - -

        // Incoming Commands
        public ICommand<string> EditDisplayNameCommand;
        public ICommand SaveGameSlotCommand;
        public ICommand CreateGameSlotCommand;
        public ICommand ExitModalCommand;
        public ICommand SelectProfileImageCommand;
        
        private readonly EditGameSlotView m_EditGameSlotView;
        private readonly IGameSaveFacade m_GameSaveFacade;
        private readonly IObjectMapper m_Mapper;
        private readonly IGameSaveSelectionMenuMediator m_Mediator;

        private ICreateGameSaveOutputPort m_CreateOutputPort;
        private IUpdateGameSaveOutputPort m_UpdateOutputPort;

        private string m_DisplayName;
        private ProfileImageModel m_ProfileImage;
        
        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public EditGameSlotViewModel(
            EditGameSlotView editGameSlotView,
            IGameSaveFacade gameSaveFacade,
            IGameSaveSelectionMenuMediator gameSaveSelectionMenuMediator,
            IObjectMapper mapper)
        {
            this.m_EditGameSlotView = editGameSlotView ?? throw new ArgumentNullException(nameof(editGameSlotView));
            this.m_GameSaveFacade = gameSaveFacade ?? throw new ArgumentNullException(nameof(gameSaveFacade));
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_Mediator = gameSaveSelectionMenuMediator ??
                                throw new ArgumentNullException(nameof(gameSaveSelectionMenuMediator));

            this.BindInteractionMethodsToCommands();
            this.RegisterMediatorActions();
        }

        #endregion Constructors

        #region - - - - - - Events - - - - - -

        public event Action<string> OnDisplayNameChanged;
        
        public event Action<ProfileImageModel> OnSelectedImageChanged;

        public event Action<bool> OnShowEditGameSlotModal;

        #endregion Events
  
        #region - - - - - - Properties - - - - - -

        public int DisplayIndex { get; private set; }
        
        public string DisplayName
        {
            get => this.m_DisplayName;
            private set
            {
                this.m_DisplayName = value;
                this.OnDisplayNameChanged?.Invoke(value);
            }
        }

        public ProfileImageModel SelectedProfileImage
        {
            get => this.m_ProfileImage;
            private set
            {
                if (this.m_ProfileImage == value)
                    return;
                
                this.m_ProfileImage = value;
                this.OnSelectedImageChanged?.Invoke(value);
            }
        }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        // -----------------------------------------
        // Initialization methods
        // -----------------------------------------

        private void BindInteractionMethodsToCommands()
        {
            this.EditDisplayNameCommand = new RelayCommand<string>(name => { this.DisplayName = name; } );
            this.CreateGameSlotCommand = new RelayCommand(this.CreateNewGameSlot);
            this.SaveGameSlotCommand = new RelayCommand(this.SaveGameSlot);
            this.ExitModalCommand = new RelayCommand(this.ExitModalMenu);
            this.SelectProfileImageCommand = new RelayCommand(this.ShowProfileSelectionModal);
            
            this.m_EditGameSlotView.BindToViewModel(this);
        }
        
        private void RegisterMediatorActions()
        {
            this.m_Mediator.Register<EditGameSaveSlotDisplayWrapper>(
                GameSaveMenuEventType.OnGameSaveSlotSelection,
                this.SetSlotSelectionValuesToModal);
            this.m_Mediator.Register(
                GameSaveMenuEventType.StartCreatingNewGameSlot,
                this.ShowCreateSlotModal);
            this.m_Mediator.Register(
                GameSaveMenuEventType.StartEditingGameSlot,
                this.ShowEditSlotModal);
            this.m_Mediator.Register<ProfileImageModel>(
                GameSaveMenuEventType.UpdateProfileImageSelection,
                this.UpdateProfileImage);
        }
        
        // -----------------------------------------
        // Command methods
        // -----------------------------------------
        
        private void CreateNewGameSlot()
        {
            CreateGameSaveInputPort _InputPort = new();
            this.m_Mapper.Map(this, _InputPort);
            this.m_GameSaveFacade.CreateGameSave(_InputPort, this.m_CreateOutputPort);
            this.m_EditGameSlotView.ContentGroup.SetActive(false);
        }

        private void SaveGameSlot()
        {
            UpdateGameSaveInputPort _InputPort = new();
            this.m_Mapper.Map(this, _InputPort);
            this.m_GameSaveFacade.UpdateGameSave(_InputPort, this.m_UpdateOutputPort);
            this.m_EditGameSlotView.ContentGroup.SetActive(false);
        }

        private void ShowProfileSelectionModal()
            => this.m_Mediator.Invoke(GameSaveMenuEventType.ShowProfileImageSelectionMenu);

        private void ExitModalMenu()
        {
            Debug.Log("[LOG] - Exit modal menu");
            this.m_EditGameSlotView.ContentGroup.SetActive(false);
        }
        
        // -----------------------------------------
        // ViewModel Actions Methods
        // -----------------------------------------

        private void SetSlotSelectionValuesToModal(EditGameSaveSlotDisplayWrapper displayWrapper)
        {
            this.DisplayIndex = displayWrapper.GameSaveSlotDto.DisplayIndex;
            this.DisplayName = displayWrapper.GameSaveSlotDto.DisplayName;
            this.SelectedProfileImage = displayWrapper.GameSaveSlotDto.ProfileImage;
            this.m_CreateOutputPort = displayWrapper.CreateOutputPort;
            this.m_UpdateOutputPort = displayWrapper.UpdateOutputPort;

            this.m_EditGameSlotView.BindToViewModel(this);
        }

        private void ShowCreateSlotModal() 
            => this.OnShowEditGameSlotModal?.Invoke(true);

        private void ShowEditSlotModal() 
            => this.OnShowEditGameSlotModal?.Invoke(false);

        private void UpdateProfileImage(ProfileImageModel profileImageModel) 
            => this.SelectedProfileImage = profileImageModel;

        #endregion Methods
        
    }

}