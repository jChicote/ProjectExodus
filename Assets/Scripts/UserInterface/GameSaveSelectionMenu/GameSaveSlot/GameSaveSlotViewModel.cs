using System;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;
using ProjectExodus.Common.Infrastructure;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.UserInterface.GameSaveSelectionMenu.EditGameSlotModal;
using UnityEngine;
using UserInterface.GameSaveSelectionMenu.Dtos;
using UserInterface.GameSaveSelectionMenu.Mediator;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.GameSaveSlot
{

    public class GameSaveSlotViewModel :
        ICreateGameSaveOutputPort,
        IGameSaveSlotNotifyEvents,
        IUpdateGameSaveOutputPort
    {

        #region - - - - - - Fields - - - - - -

        private ICommand m_SlotSelectionCommand;
        
        private readonly IObjectMapper m_Mapper;
        private readonly IGameSaveSelectionMenuMediator m_Mediator;
        
        private GameSaveModel m_GameSaveModel;
        private bool m_IsSlotEmpty;
        
        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameSaveSlotViewModel(
            GameSaveModel gameSaveModel,
            IGameSaveSlotView gameSaveSlotView,
            IGameSaveSelectionMenuMediator gameSaveSelectionMenuMediator,
            IObjectMapper objectMapper)
        {
            this.m_Mapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
            this.m_Mediator = gameSaveSelectionMenuMediator ?? 
                                throw new ArgumentNullException(nameof(gameSaveSelectionMenuMediator));
            
            if (gameSaveModel == null) this.m_IsSlotEmpty = true;
            this.GameSaveModel = gameSaveModel ?? new GameSaveModel();
            
            this.BindLogicToCommands();
            gameSaveSlotView.BindToViewModel(this);
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

        ICommand IGameSaveSlotNotifyEvents.SlotSelectionCommand => this.m_SlotSelectionCommand;

        #endregion Properties

        #region - - - - - - Events - - - - - -

        public event Action<bool> OnDisplayGameSaveSlot;

        public event Action<string, object> OnPropertyChangeEvent;
        
        #endregion Events

        #region - - - - - - Methods - - - - - -
        
        private void BindLogicToCommands() 
            => this.m_SlotSelectionCommand = new RelayCommand(this.OnSlotSelection);
        
        private void OnSlotSelection()
        {
            GameSaveSlotDto _GameSaveSlotDto = new GameSaveSlotDto();
            this.m_Mapper.Map(this.m_GameSaveModel, _GameSaveSlotDto);

            EditGameSaveSlotDisplayWrapper _DisplayWrapper = new(_GameSaveSlotDto, this, this);
            
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
            this.OnDisplayGameSaveSlot?.Invoke(false);
        }

        void IUpdateGameSaveOutputPort.PresentUpdatedGameSave(GameSaveModel gameSaveModel) 
            => this.GameSaveModel = gameSaveModel;

        void IUpdateGameSaveOutputPort.PresentFailedUpdateOfGameSave() 
            => Debug.LogError("[ERROR]: Cannot find the Game Save.");

        #endregion Methods

    }

}