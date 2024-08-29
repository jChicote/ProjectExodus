using System;
using System.Windows.Input;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Mappers;
using UnityEngine;
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
            IObjectMapper mapper)
        {
            this.m_EditGameSlotView = editGameSlotView ?? throw new ArgumentNullException(nameof(editGameSlotView));
            this.m_GameSaveFacade = gameSaveFacade ?? throw new ArgumentNullException(nameof(gameSaveFacade));
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_Mediator = gameSaveSelectionMenuMediator ??
                                throw new ArgumentNullException(nameof(gameSaveSelectionMenuMediator));
            
            this.BindViewEvents();
            this.RegisterMediatorActions();
        }

        #endregion Constructors

        #region - - - - - - Events - - - - - -

        public event Action<string> DisplayNameChanged;
        
        public event Action<Sprite> SelectedImageChanged;

        #endregion Events
  
        #region - - - - - - Properties - - - - - -

        public int DisplayIndex { get; private set; }
        
        public string DisplayName
        {
            get => this.m_DisplayName;
            private set
            {
                this.m_DisplayName = value;
                this.DisplayNameChanged?.Invoke(value);
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
                this.SelectedImageChanged?.Invoke(value);
            }
        }

        #endregion Properties
  
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
            this.m_Mediator.Register<EditGameSaveSlotDisplayWrapper>(
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
        // Subscribed methods
        // -----------------------------------------
        
        private void OnCreateGameSlot()
        {
            CreateGameSaveInputPort _InputPort = new();
            this.m_Mapper.Map(this, _InputPort);
            this.m_GameSaveFacade.CreateGameSave(_InputPort, this.m_CreateOutputPort);
            this.m_EditGameSlotView.ContentGroup.SetActive(false);
        }

        private void OnSaveGameSlot()
        {
            UpdateGameSaveInputPort _InputPort = new();
            this.m_Mapper.Map(this, _InputPort);
            this.m_GameSaveFacade.UpdateGameSave(_InputPort, this.m_UpdateOutputPort);
            this.m_EditGameSlotView.ContentGroup.SetActive(false);
        }

        private void OnProfileSelection() 
            => Debug.LogWarning("[WARNING] - Behavior not implemented");

        private void OnExitModalMenu()
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
        }

        private void ShowCreateSlotModal()
        {
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

        #endregion Methods
    }

}