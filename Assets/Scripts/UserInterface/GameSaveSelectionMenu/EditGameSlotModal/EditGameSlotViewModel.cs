using System;
using System.Collections;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Mappers;
using UnityEngine;
using UserInterface.GameSaveSelectionMenu.Dtos;
using UserInterface.GameSaveSelectionMenu.Mediator;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.EditGameSlotModal
{

    public class EditGameSlotViewModel
    {

        #region - - - - - - Fields - - - - - -

        private readonly EditGameSlotView m_EditGameSlotView;
        private readonly IGameSaveFacade m_GameSaveFacade;
        private readonly IObjectMapper m_Mapper;
        private readonly IGameSaveSelectionMenuMediator m_Mediator;

        private string m_DisplayName;
        private Sprite m_SelectedProfileImage;
        private bool m_IsNewGameSlot;
        
        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public EditGameSlotViewModel(
            EditGameSlotView editGameSlotView,
            IGameSaveSelectionMenuMediator gameSaveSelectionMenuMediator,
            IObjectMapper mapper)
        {
            this.m_EditGameSlotView = editGameSlotView ?? throw new ArgumentNullException(nameof(editGameSlotView));
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_Mediator = gameSaveSelectionMenuMediator ??
                                throw new ArgumentNullException(nameof(gameSaveSelectionMenuMediator));
            
            this.BindViewEvents();
            this.RegisterMediatorActions();
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public string DisplayName
        {
            get => this.m_DisplayName;
            set
            {
                this.m_DisplayName = value;
                this.m_EditGameSlotView.DisplayNameInputField.text = value;
            }
        }

        public Sprite SelectedProfileImage
        {
            get => this.m_SelectedProfileImage;
            set
            {
                this.m_SelectedProfileImage = value;
                this.m_EditGameSlotView.SelectedProfileImage.sprite = value;
            }
        }

        #endregion Properties

        #region - - - - - - Events - - - - - -

        private void OnCreateGameSlot()
        {
            GameSaveSlotDto _GameSaveSlotDto = new GameSaveSlotDto();
            this.m_Mapper.Map(this, _GameSaveSlotDto);
            this.m_Mediator.Invoke(GameSaveMenuEventType.CreateGameSaveSlot, _GameSaveSlotDto);
            this.m_EditGameSlotView.ContentGroup.SetActive(false);
        }

        private void OnSaveGameSlot()
        {
            GameSaveSlotDto _GameSaveSlotDto = new GameSaveSlotDto();
            this.m_Mapper.Map(this, _GameSaveSlotDto);
            this.m_Mediator.Invoke(GameSaveMenuEventType.UpdateGameSaveSlot, _GameSaveSlotDto);
            this.m_EditGameSlotView.ContentGroup.SetActive(false);
        }

        private void OnProfileSelection()
        {
            Debug.LogWarning("[WARNING] - Behavior not implemented");
        }
        
        private void OnExitModalMenu()
        {
            Debug.Log("[LOG] - Exit modal menu");
            
            this.m_EditGameSlotView.ContentGroup.SetActive(false);
        }

        #endregion Events
  
        #region - - - - - - Methods - - - - - -

        // -----------------------------------------
        // Initialization methods
        // -----------------------------------------
        
        private void BindViewEvents()
        {
            this.m_EditGameSlotView.CreateButton.onClick.AddListener(this.OnCreateGameSlot);
            this.m_EditGameSlotView.SaveButton.onClick.AddListener(this.OnSaveGameSlot);
            this.m_EditGameSlotView.ExitButton.onClick.AddListener(this.OnExitModalMenu);
            this.m_EditGameSlotView.SelectedProfileImageButton.onClick.AddListener(this.OnProfileSelection);
        }

        private void RegisterMediatorActions()
        {
            this.m_Mediator.Register<GameSaveSlotDto>(
                GameSaveMenuEventType.OnGameSaveSlotSelection,
                this.SetSlotSelectionValuesToModal);
            this.m_Mediator.Register(
                GameSaveMenuEventType.StartCreatingNewGameSlot,
                this.ShowCreateSlotModal);
            this.m_Mediator.Register(
                GameSaveMenuEventType.StartEditingGameSlot,
                this.ShowEditSlotModal);
        }
        
        // -----------------------------------------
        // View Model Actions
        // -----------------------------------------

        private void SetSlotSelectionValuesToModal(GameSaveSlotDto gameSaveSlotDto)
        {
            this.DisplayName = gameSaveSlotDto.DisplayName;
            this.SelectedProfileImage = gameSaveSlotDto.ProfileImage;
        }

        private void ShowCreateSlotModal()
        {
            this.DisplayName = "Game Save";
            this.SelectedProfileImage = default;
            this.m_IsNewGameSlot = true;
            this.m_EditGameSlotView.CreateButton.gameObject.SetActive(true);
            this.m_EditGameSlotView.SaveButton.gameObject.SetActive(false);
            this.m_EditGameSlotView.ContentGroup.SetActive(true);
        }

        private void ShowEditSlotModal()
        {
            this.m_IsNewGameSlot = false;
            this.m_EditGameSlotView.CreateButton.gameObject.SetActive(false);
            this.m_EditGameSlotView.SaveButton.gameObject.SetActive(true);
            this.m_EditGameSlotView.ContentGroup.SetActive(true);
        }

        private void SaveGameSaveSlot()
        {
            // Check whether to update or create
            if (this.m_IsNewGameSlot)
            {
                this.m_GameSaveFacade.CreateGameSave();
            }
            // Invoke use case
            
            // Update the rest of the views on the screen
            
            
        }

        #endregion Methods
  
    }

}