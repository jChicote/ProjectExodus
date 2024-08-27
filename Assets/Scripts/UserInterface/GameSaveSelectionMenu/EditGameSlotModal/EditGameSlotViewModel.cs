using ProjectExodus.GameLogic.Facades.GameSaveFacade;
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
        private readonly IGameSaveSelectionMenuMediator m_Mediator;

        private GameSaveSlotDto m_GameSaveSlotDto; // Cache dto to simplify data handling
        
        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public EditGameSlotViewModel(
            IGameSaveSelectionMenuMediator gameSaveSelectionMenuMediator,
            EditGameSlotView editGameSlotView)
        {
            this.m_EditGameSlotView = editGameSlotView;
            this.m_Mediator = gameSaveSelectionMenuMediator;
            
            // Register Handlers
            this.m_Mediator.Register(
                                GameSaveMenuEventType.StartCreatingNewGameSlot,
                                this.ShowCreateSlotModal);
            this.m_Mediator.Register<GameSaveSlotDto>(
                                GameSaveMenuEventType.StartEditingGameSlot,
                                this.ShowEditSlotModal);
            
            // Bind Views
            this.m_EditGameSlotView.CreateButton.onClick.AddListener(this.OnCreateGameSlot);
            this.m_EditGameSlotView.SaveButton.onClick.AddListener(this.OnSaveGameSlot);
            this.m_EditGameSlotView.ExitButton.onClick.AddListener(this.OnExitModalMenu);
            this.m_EditGameSlotView.SelectedProfileImageButton.onClick.AddListener(this.OnProfileSelection);
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public string DisplayName
        {
            get => this.m_GameSaveSlotDto.DisplayName;
            set
            {
                this.m_GameSaveSlotDto.DisplayName = value;
            }
        }

        public Sprite SelectedProfileImage
        {
            get => this.m_GameSaveSlotDto.ProfileImage;
            set
            {
                this.m_GameSaveSlotDto.ProfileImage = value;
            }
        }

        #endregion Properties

        #region - - - - - - Events - - - - - -

        private void OnCreateGameSlot()
        {
            this.m_Mediator.Invoke(GameSaveMenuEventType.CreateGameSaveSlot, this.m_GameSaveSlotDto);
            this.m_EditGameSlotView.ContentGroup.SetActive(false);
        }

        private void OnSaveGameSlot()
        {
            this.m_Mediator.Invoke(GameSaveMenuEventType.UpdateGameSaveSlot, this.m_GameSaveSlotDto);
            this.m_EditGameSlotView.ContentGroup.SetActive(false);
        }
        
        private void OnExitModalMenu()
        {
            Debug.Log("[LOG] - Exit modal menu");
            this.m_EditGameSlotView.ContentGroup.SetActive(false);
            this.m_GameSaveSlotDto = default;
        }

        private void OnProfileSelection()
        {
            Debug.LogWarning("[WARNING] - Exit modal menu. Data has been thrown out.");
        }

        #endregion Events
  
        #region - - - - - - Methods - - - - - -

        private void ShowCreateSlotModal()
        {
            this.m_GameSaveSlotDto = new();
            this.m_GameSaveSlotDto.DisplayName = "Game Save";
            this.m_GameSaveSlotDto.ProfileImage = default(Sprite);
            this.m_EditGameSlotView.CreateButton.gameObject.SetActive(true);
            this.m_EditGameSlotView.SaveButton.gameObject.SetActive(false);
            this.m_EditGameSlotView.ContentGroup.SetActive(true);
        }

        private void ShowEditSlotModal(GameSaveSlotDto gameSaveSlotDto)
        {
            this.m_GameSaveSlotDto = gameSaveSlotDto;
            this.m_EditGameSlotView.CreateButton.gameObject.SetActive(false);
            this.m_EditGameSlotView.SaveButton.gameObject.SetActive(true);
            this.m_EditGameSlotView.ContentGroup.SetActive(true);
        }

        #endregion Methods
  
    }

}