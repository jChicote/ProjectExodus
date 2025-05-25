using System;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.DeleteGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;
using ProjectExodus.Common.Infrastructure;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.GameDataManager;
using ProjectExodus.UserInterface.Controllers;
using ProjectExodus.UserInterface.GameSaveSelectionMenu.Common;
using UnityEngine;
using UserInterface.GameSaveSelectionMenu.Dtos;
using UserInterface.GameSaveSelectionMenu.Mediator;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.GameSaveSlot
{

    public class GameSaveSlotViewModel :
        ICreateGameSaveOutputPort,
        IDeleteGameSaveOutputPort,
        IGameSaveSlotNotifyEvents,
        IUpdateGameSaveOutputPort
    {

        #region - - - - - - Fields - - - - - -

        private readonly IGameDataManager m_GameSaveManager;
        private readonly IObjectMapper m_Mapper;
        private readonly IGameSaveSelectionMenuMediator m_Mediator;
        private readonly IUserInterfaceController m_UserInterfaceController;

        private ICommand m_PlayGameSaveCommand;
        private ICommand m_SlotSelectionCommand;
        
        private GameSaveModel m_GameSaveModel;
        private bool m_IsSlotEmpty;
        
        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameSaveSlotViewModel(
            IGameDataManager gameSaveManager,
            GameSaveModel gameSaveModel,
            IGameSaveSlotView gameSaveSlotView,
            IGameSaveSelectionMenuMediator gameSaveSelectionMenuMediator,
            IObjectMapper objectMapper,
            IUserInterfaceController userInterfaceController)
        {
            this.m_GameSaveManager = gameSaveManager ?? throw new ArgumentNullException(nameof(gameSaveManager));
            this.m_Mapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
            this.m_Mediator = gameSaveSelectionMenuMediator ?? 
                                throw new ArgumentNullException(nameof(gameSaveSelectionMenuMediator));
            this.m_UserInterfaceController = userInterfaceController ??
                                                        throw new ArgumentNullException(nameof(userInterfaceController));
            
            // 1. Setup view model
            this.BindLogicToCommands();
            gameSaveSlotView.BindToViewModel(this);
            
            // 2. Set initial data
            if (gameSaveModel == null) this.m_IsSlotEmpty = true;
            this.GameSaveModel = gameSaveModel ?? new GameSaveModel();
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public Guid ID { get; set; }

        public GameSaveModel GameSaveModel
        {
            get => this.m_GameSaveModel;
            private set
            {
                if (value == null) return;

                this.m_GameSaveModel = value;

                this.OnPropertyChangeEvent?.Invoke("SlotPercentage", this.m_GameSaveModel.CompletionProgress);
                this.OnPropertyChangeEvent?.Invoke("SlotTitle", this.m_GameSaveModel.GameSaveName);
                this.OnPropertyChangeEvent?.Invoke("SlotLastAccessedDate", this.m_GameSaveModel.LastAccessedDate);
                this.OnPropertyChangeEvent?.Invoke("SlotProfileImage", this.m_GameSaveModel.ProfileImage.Image);
            }
        }

        ICommand IGameSaveSlotNotifyEvents.PlayGameSaveCommand => this.m_PlayGameSaveCommand;

        ICommand IGameSaveSlotNotifyEvents.SlotSelectionCommand => this.m_SlotSelectionCommand;

        #endregion Properties

        #region - - - - - - Events - - - - - -

        public event Action<bool> OnDisplayGameSaveSlot;

        public event Action<string, object> OnPropertyChangeEvent;
        
        #endregion Events

        #region - - - - - - Methods - - - - - -
        
        // ----------------------------------
        // Setup Methods
        // ----------------------------------
        
        private void BindLogicToCommands()
        {
            this.m_PlayGameSaveCommand = new RelayCommand(this.PlayGameSave);
            this.m_SlotSelectionCommand = new RelayCommand(this.SelectSlot);
        }
        
        // ----------------------------------
        // Command Methods
        // ----------------------------------

        private void PlayGameSave()
        {
            this.m_GameSaveManager.SetGameSave(this.m_GameSaveModel);
            this.m_UserInterfaceController.OpenScreen(UIScreenType.MainMenu);
        }

        private void SelectSlot()
        {
            GameSaveSlotDto _GameSaveSlotDto = new GameSaveSlotDto();
            this.m_Mapper.Map(this.m_GameSaveModel, _GameSaveSlotDto);

            GameSaveSlotModelWrapper _DisplayWrapper = new(_GameSaveSlotDto, this, this, this);
            
            this.m_Mediator.Invoke(this.m_IsSlotEmpty
                ? GameSaveMenuEventType.EmptySaveSlot_Selected
                : GameSaveMenuEventType.GameSaveSlot_Selected);
            this.m_Mediator.Invoke(GameSaveMenuEventType.GameSaveSlot_Selected, _DisplayWrapper);
        }

        // This is a hack solution. These game slots should really belong to the GameSaveSlot screen.
        public void DisplayEmptyGameSlot()
        {
            this.OnDisplayGameSaveSlot?.Invoke(true);
            this.m_IsSlotEmpty = true;
        }

        // This is a hack solution. These game slots should really belong to the GameSaveSlot screen.
        public void DisplayUsedGameSlot()
        {
            this.OnDisplayGameSaveSlot?.Invoke(false);
            this.m_IsSlotEmpty = false;
        }

        // ----------------------------------
        // OutputPort Presentation Methods
        // ----------------------------------

        void ICreateGameSaveOutputPort.PresentCreatedGameSave(GameSaveModel gameSaveModel)
        {
            this.GameSaveModel = gameSaveModel;
            this.DisplayUsedGameSlot();

            this.m_GameSaveManager.SaveGameSave();
        }

        void IDeleteGameSaveOutputPort.PresentSuccessfulDeletion()
        {
            this.GameSaveModel = new GameSaveModel();
            this.DisplayEmptyGameSlot();
            
            this.m_GameSaveManager.SaveGameSave();
        }

        void IUpdateGameSaveOutputPort.PresentUpdatedGameSave(GameSaveModel gameSaveModel)
        {
            this.GameSaveModel = gameSaveModel;
            this.DisplayUsedGameSlot();
            
            this.m_GameSaveManager.SaveGameSave();
        }

        void IUpdateGameSaveOutputPort.PresentFailedUpdateOfGameSave() 
            => Debug.LogError("[ERROR]: Cannot find the Game Save.");

        #endregion Methods
        
    }

}