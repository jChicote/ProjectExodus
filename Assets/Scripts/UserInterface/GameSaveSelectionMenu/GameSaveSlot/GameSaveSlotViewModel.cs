using System;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;
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
        IUpdateGameSaveOutputPort
    {

        #region - - - - - - Fields - - - - - -

        private const int MAX_DISPLAYNAME_LENGTH = 10;
        
        private readonly GameSaveSlotView m_GameSaveSlotView;
        private readonly IObjectMapper m_Mapper;
        private readonly IGameSaveSelectionMenuMediator m_Mediator;
        
        private GameSaveModel m_GameSaveModel;
        private bool m_IsSlotEmpty;
        
        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameSaveSlotViewModel(
            GameSaveModel gameSaveModel,
            GameSaveSlotView gameSaveSlotView,
            IGameSaveSelectionMenuMediator gameSaveSelectionMenuMediator,
            IObjectMapper objectMapper)
        {
            this.m_GameSaveSlotView = gameSaveSlotView ?? throw new ArgumentNullException(nameof(gameSaveSlotView));
            this.m_Mapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
            this.m_Mediator = gameSaveSelectionMenuMediator ?? 
                                throw new ArgumentNullException(nameof(gameSaveSelectionMenuMediator));
            
            if (gameSaveModel == null) this.m_IsSlotEmpty = true;
            this.GameSaveModel = gameSaveModel ?? new GameSaveModel();
            
            // Bind slot button
            this.m_GameSaveSlotView.SlotButton.onClick.AddListener(this.OnSlotSelection);
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
                
                this.m_GameSaveSlotView.SlotPercentage.text = $"{this.m_GameSaveModel.CompletionProgress}%";
                this.m_GameSaveSlotView.SlotTitle.text = this.m_GameSaveModel.GameSaveName.Length > MAX_DISPLAYNAME_LENGTH 
                                                            ? this.m_GameSaveModel.GameSaveName
                                                                .Substring(0, MAX_DISPLAYNAME_LENGTH) + "..." 
                                                            : this.m_GameSaveModel.GameSaveName;
                this.m_GameSaveSlotView.SlotLastAccessedDate.text =
                    $"{this.m_GameSaveModel.LastAccessedDate.Day}/" +
                    $"{this.m_GameSaveModel.LastAccessedDate.Month}/" +
                    $"{this.m_GameSaveModel.LastAccessedDate.Year}";
                this.m_GameSaveSlotView.SlotProfileImage.sprite = this.m_GameSaveModel.ProfileImage.Image;
            }
        }

        #endregion Properties

        #region - - - - - - Events - - - - - -

        private void OnSlotSelection()
        {
            GameSaveSlotDto _GameSaveSlotDto = new GameSaveSlotDto();
            this.m_Mapper.Map(this.m_GameSaveModel, _GameSaveSlotDto);

            EditGameSaveSlotDisplayWrapper _DisplayWrapper = new(_GameSaveSlotDto, this, this);
            
            this.m_Mediator.Invoke(this.m_IsSlotEmpty
                ? GameSaveMenuEventType.OnEmptySlotSelection
                : GameSaveMenuEventType.OnGameSaveSlotSelection);
            this.m_Mediator.Invoke(GameSaveMenuEventType.OnGameSaveSlotSelection, _DisplayWrapper);
        }

        #endregion Events

        #region - - - - - - Methods - - - - - -

        public void DisplayGameSaveSlot()
        {
            this.m_IsSlotEmpty = false;

            this.m_GameSaveSlotView.GameSlotContentGroup.SetActive(true);
            this.m_GameSaveSlotView.EmptySlotContentGroup.SetActive(false);
        }

        public void DisplayEmptySlot()
        {
            this.m_IsSlotEmpty = true;
            
            this.m_GameSaveSlotView.GameSlotContentGroup.SetActive(false);
            this.m_GameSaveSlotView.EmptySlotContentGroup.SetActive(true);
        }
        
        // ----------------------------------
        // OutputPort Presentation Methods
        // ----------------------------------

        void ICreateGameSaveOutputPort.PresentCreatedGameSave(GameSaveModel gameSaveModel)
        {
            this.GameSaveModel = gameSaveModel;
            this.DisplayGameSaveSlot();
            Debug.Log("[LOG]: Created GameSaveModel loaded to GameSlot");
        }

        void IUpdateGameSaveOutputPort.PresentUpdatedGameSave(GameSaveModel gameSaveModel)
        {
            this.GameSaveModel = gameSaveModel;
            Debug.Log("[LOG]: Updated GameSaveModel loaded to GameSlot");
        }

        #endregion Methods

    }

}