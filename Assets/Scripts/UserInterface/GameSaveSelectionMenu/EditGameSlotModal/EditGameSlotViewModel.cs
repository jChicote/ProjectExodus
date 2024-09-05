using System;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;
using ProjectExodus.Common.Infrastructure;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.UserInterface.GameSaveSelectionMenu.Common;
using UserInterface.GameSaveSelectionMenu.Mediator;
using Random = UnityEngine.Random;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.EditGameSlotModal
{

    public class EditGameSlotViewModel : IEditGameSlotNotifyEvents
    {

        #region - - - - - - Fields - - - - - -
        
        private readonly IGameSaveFacade m_GameSaveFacade;
        private readonly IObjectMapper m_Mapper;
        private readonly IGameSaveSelectionMenuMediator m_Mediator;

        private ICommand m_CreateGameSlotCommand;
        private ICommand<string> m_EditDisplayNameCommand;
        private ICommand m_ExitModalCommand;
        private ICommand m_SaveGameSlotCommand;
        private ICommand m_SelectProfileImageCommand;

        private ICreateGameSaveOutputPort m_CreateOutputPort;
        private IUpdateGameSaveOutputPort m_UpdateOutputPort;

        private string m_DisplayName;
        private ProfileImageModel m_ProfileImage;
        
        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public EditGameSlotViewModel(
            IEditGameSlotView editGameSlotView,
            IGameSaveFacade gameSaveFacade,
            IGameSaveSelectionMenuMediator gameSaveSelectionMenuMediator,
            IObjectMapper mapper)
        {
            this.m_GameSaveFacade = gameSaveFacade ?? throw new ArgumentNullException(nameof(gameSaveFacade));
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_Mediator = gameSaveSelectionMenuMediator ??
                                throw new ArgumentNullException(nameof(gameSaveSelectionMenuMediator));

            this.BindLogicToCommands();
            this.RegisterMediatorActions();
            editGameSlotView.BindToViewModel(this);
        }

        #endregion Constructors
  
        #region - - - - - - Properties - - - - - -

        public int DisplayIndex { get; private set; }
        
        public string DisplayName
        {
            get => this.m_DisplayName;
            private set
            {
                this.m_DisplayName = !string.IsNullOrWhiteSpace(value) 
                                        ? value
                                        : $"Game Save {Random.Range(1, 10)}";
                this.OnDisplayNameChanged?.Invoke(this.m_DisplayName);
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
        
        ICommand IEditGameSlotNotifyEvents.CreateGameSlotCommand => this.m_CreateGameSlotCommand;

        ICommand<string> IEditGameSlotNotifyEvents.EditDisplayNameCommand => this.m_EditDisplayNameCommand;

        ICommand IEditGameSlotNotifyEvents.ExitModalCommand => this.m_ExitModalCommand;

        ICommand IEditGameSlotNotifyEvents.SaveGameSlotCommand => this.m_SaveGameSlotCommand;

        ICommand IEditGameSlotNotifyEvents.SelectProfileImageCommand => this.m_SelectProfileImageCommand;

        #endregion Properties

        #region - - - - - - Events - - - - - -

        public event Action<string> OnDisplayNameChanged;

        public event Action OnEnableEditGameSlotModal;
        
        public event Action<ProfileImageModel> OnSelectedImageChanged;

        public event Action<bool> OnShowEditGameSlotModal;

        #endregion Events
  
        #region - - - - - - Methods - - - - - -

        // -----------------------------------------
        // Setup Methods
        // -----------------------------------------

        private void BindLogicToCommands()
        {
            this.m_EditDisplayNameCommand = new RelayCommand<string>(name => { this.DisplayName = name; } );
            this.m_CreateGameSlotCommand = new RelayCommand(this.CreateNewGameSlot);
            this.m_SaveGameSlotCommand = new RelayCommand(this.SaveGameSlot);
            this.m_ExitModalCommand = new RelayCommand(this.ExitModalMenu);
            this.m_SelectProfileImageCommand = new RelayCommand(this.ShowProfileSelectionModal);
        }
        
        private void RegisterMediatorActions()
        {
            this.m_Mediator.Register(
                GameSaveMenuEventType.CreateNewGameSlot_Open,
                this.ShowCreateSlotModal);
            this.m_Mediator.Register(
                GameSaveMenuEventType.EditGameSlot_Open,
                this.ShowEditSlotModal);
            this.m_Mediator.Register(
                GameSaveMenuEventType.EditGameSlot_Open,
                () => { this.OnEnableEditGameSlotModal?.Invoke(); });
            this.m_Mediator.Register<ProfileImageModel>(
                GameSaveMenuEventType.EditGameSlotImage_Update,
                this.UpdateProfileImage);
            this.m_Mediator.Register<GameSaveSlotModelWrapper>(
                GameSaveMenuEventType.GameSaveSlot_Selected,
                this.SetSlotSelectionValuesToModal);
        }
        
        // -----------------------------------------
        // Command Methods
        // -----------------------------------------
        
        private void CreateNewGameSlot()
        {
            CreateGameSaveInputPort _InputPort = new();
            this.m_Mapper.Map(this, _InputPort);
            this.m_GameSaveFacade.CreateGameSave(_InputPort, this.m_CreateOutputPort);
            
            this.m_Mediator.Invoke(GameSaveMenuEventType.GameSaveMenuInteraction_Enabled);
        }

        private void SaveGameSlot()
        {
            UpdateGameSaveInputPort _InputPort = new();
            this.m_Mapper.Map(this, _InputPort);
            this.m_GameSaveFacade.UpdateGameSave(_InputPort, this.m_UpdateOutputPort);
            
            this.m_Mediator.Invoke(GameSaveMenuEventType.GameSaveMenuInteraction_Enabled);
        }

        private void ShowProfileSelectionModal()
            => this.m_Mediator.Invoke(GameSaveMenuEventType.ProfileImageSelectionModal_Open);

        private void ExitModalMenu() 
            => this.m_Mediator.Invoke(GameSaveMenuEventType.GameSaveMenuInteraction_Enabled);

        // -----------------------------------------
        // View Model Actions
        // -----------------------------------------

        private void SetSlotSelectionValuesToModal(GameSaveSlotModelWrapper displayWrapper)
        {
            this.DisplayIndex = displayWrapper.GameSaveSlotDto.DisplayIndex;
            this.DisplayName = displayWrapper.GameSaveSlotDto.DisplayName;
            this.SelectedProfileImage = displayWrapper.GameSaveSlotDto.ProfileImage;
            this.m_CreateOutputPort = displayWrapper.CreateOutputPort;
            this.m_UpdateOutputPort = displayWrapper.UpdateOutputPort;
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