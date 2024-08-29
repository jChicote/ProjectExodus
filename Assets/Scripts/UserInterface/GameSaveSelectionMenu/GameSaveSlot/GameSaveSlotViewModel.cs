using System;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;
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
            this.m_GameSaveModel = gameSaveModel ?? new GameSaveModel();
            
            // Bind slot button
            this.m_GameSaveSlotView.SlotButton.onClick.AddListener(this.OnSlotSelection);
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public Guid ID { get; set; }

        public float CompletionProgress
        {
            get => this.m_GameSaveModel.CompletionProgress;
            set
            {
                this.m_GameSaveModel.CompletionProgress = value;
                this.m_GameSaveSlotView.SlotPercentage.text = $"{this.m_GameSaveModel.CompletionProgress}%";
            }
        }

        public int DisplayIndex
        {
            get => this.m_GameSaveModel.GameSlotDisplayIndex;
            set => this.m_GameSaveModel.GameSlotDisplayIndex = value;
        }

        public string GameSaveName
        {
            get => this.m_GameSaveModel.GameSaveName;
            set
            {
                this.m_GameSaveModel.GameSaveName = value;
                this.m_GameSaveSlotView.SlotTitle.text = this.GameSaveName;
            }
        }

        public DateTime LastAccessedDate
        {
            get => this.m_GameSaveModel.LastAccessedDate;
            set
            {
                this.m_GameSaveModel.LastAccessedDate = value;
                this.m_GameSaveSlotView.SlotLastAccessedDate.text =
                    $"{this.m_GameSaveModel.LastAccessedDate.Day}/" +
                    $"{this.m_GameSaveModel.LastAccessedDate.Month}/" +
                    $"{this.m_GameSaveModel.LastAccessedDate.Year}";
            }
        }

        public Sprite ProfileImage
        {
            get => default;
            set
            {
                this.m_GameSaveModel.ProfileImage = value;
                this.m_GameSaveSlotView.SlotProfileImage.sprite = this.m_GameSaveModel.ProfileImage;
            }
        }

        #endregion Properties

        #region - - - - - - Events - - - - - -

        private void OnSlotSelection()
        {
            GameSaveSlotDto _GameSaveSlotDto = new GameSaveSlotDto();
            this.m_Mapper.Map(this.m_GameSaveModel, _GameSaveSlotDto);
            
            this.m_Mediator.Invoke(this.m_IsSlotEmpty
                ? GameSaveMenuEventType.OnEmptySlotSelection
                : GameSaveMenuEventType.OnGameSaveSlotSelection);
            this.m_Mediator.Invoke(GameSaveMenuEventType.OnGameSaveSlotSelection, _GameSaveSlotDto);
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

        void ICreateGameSaveOutputPort.PresentCreatedGameSave(GameSaveModel gameSaveModel)
        {
            throw new NotImplementedException();
        }

        void IUpdateGameSaveOutputPort.PresentUpdatedGameSave(GameSaveModel gameSaveModel)
        {
            throw new NotImplementedException();
        }

        #endregion Methods

    }

}