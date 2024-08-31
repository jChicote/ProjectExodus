using System;
using System.Collections.Generic;
using System.Windows.Input;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;
using ProjectExodus.Common.Infrastructure;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.ScriptableObjects;
using UnityEngine;
using UnityEngine.Rendering;
using UserInterface.GameSaveSelectionMenu.Mediator;
using ICommand = ProjectExodus.Common.Services.ICommand;

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
        private readonly UserInterfaceSettings m_UserInterfaceSettings;

        private ICreateGameSaveOutputPort m_CreateOutputPort;
        private IUpdateGameSaveOutputPort m_UpdateOutputPort;

        private string m_DisplayName;
        private Sprite m_ProfileImage;
        
        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public EditGameSlotViewModel(
            EditGameSlotView editGameSlotView,
            IGameSaveFacade gameSaveFacade,
            IGameSaveSelectionMenuMediator gameSaveSelectionMenuMediator,
            IObjectMapper mapper,
            UserInterfaceSettings userInterfaceSettings)
        {
            this.m_EditGameSlotView = editGameSlotView ?? throw new ArgumentNullException(nameof(editGameSlotView));
            this.m_GameSaveFacade = gameSaveFacade ?? throw new ArgumentNullException(nameof(gameSaveFacade));
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_Mediator = gameSaveSelectionMenuMediator ??
                                throw new ArgumentNullException(nameof(gameSaveSelectionMenuMediator));
            this.m_UserInterfaceSettings =
                userInterfaceSettings ?? throw new ArgumentNullException(nameof(userInterfaceSettings));

            this.BindInteractionMethodsToCommands();
            this.RegisterMediatorActions();
        }

        #endregion Constructors

        #region - - - - - - Events - - - - - -

        public event Action<string> OnDisplayNameChanged;
        
        public event Action<Sprite> OnSelectedImageChanged;

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

        public Sprite SelectedProfileImage
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
            this.m_Mediator.Register<int>(
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
        // View Model Actions
        // -----------------------------------------

        private void SetSlotSelectionValuesToModal(EditGameSaveSlotDisplayWrapper displayWrapper)
        {
            this.DisplayIndex = displayWrapper.GameSaveSlotDto.DisplayIndex;
            this.DisplayName = displayWrapper.GameSaveSlotDto.DisplayName;
            this.SelectedProfileImage = displayWrapper.GameSaveSlotDto.ProfileImage;
            this.m_CreateOutputPort = displayWrapper.CreateOutputPort;
            this.m_UpdateOutputPort = displayWrapper.UpdateOutputPort;

            this.m_EditGameSlotView.BindToViewModel(this);
            
            if (this.m_UserInterfaceSettings.ProfileImages.TryGetValue(this.DisplayIndex, out Sprite _Image))
                this.OnSelectedImageChanged?.Invoke(_Image); // TODO: Move this to a seperate provider
        }

        private void ShowCreateSlotModal()
        {
            if (this.m_UserInterfaceSettings.ProfileImages.TryGetValue(0, out Sprite _Image))
                this.OnSelectedImageChanged?.Invoke(_Image); // TODO: Move this to a seperate provider
            
            this.m_EditGameSlotView.CreateButton.gameObject.SetActive(true);
            this.m_EditGameSlotView.SaveButton.gameObject.SetActive(false);
            this.m_EditGameSlotView.ContentGroup.SetActive(true);
        }

        private void ShowEditSlotModal()
        {
            this.m_EditGameSlotView.CreateButton.gameObject.SetActive(false);
            this.m_EditGameSlotView.SaveButton.gameObject.SetActive(true);
            this.m_EditGameSlotView.ContentGroup.SetActive(true);
        }

        private void UpdateProfileImage(int imageIndex)
        {
            this.DisplayIndex = imageIndex;

            if (this.m_UserInterfaceSettings.ProfileImages.TryGetValue(imageIndex, out Sprite _Image))
                this.OnSelectedImageChanged?.Invoke(_Image);
            else
                Debug.LogError("[Error] Could not find the profile image.");
        }

        #endregion Methods
        
    }

}