using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.UserInterface.GameSaveSelectionMenu.EditGameSlotModal;
using UnityEngine;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu
{

    public class GameSaveSelectionMenuController : 
        MonoBehaviour, 
        IGameSaveSelectionMenuController, 
        IGetGameSavesOutputPort
    {

        #region - - - - - - Fields - - - - - -

        [Header("GameSave Selection Menu View")] 
        [SerializeField] private GameObject m_ContentGroup;

        // [Header("Game Slots")]
        // [SerializeField] private GameSaveSlotView m_GameSaveSlot1;
        // [SerializeField] private GameSaveSlotView m_GameSaveSlot2;
        // [SerializeField] private GameSaveSlotView m_GameSaveSlot3;
        
        // [Header("Buttons")]
        // [SerializeField] private Button m_ClearButton;
        // [SerializeField] private Button m_NewGameButton;
        // [SerializeField] private Button m_EditButton;
        // [SerializeField] private Button m_QuitButton;

        [SerializeField] private GameSaveSelectionMenuView m_GameSaveSelectionMenuView;

        [Header("Modals")] 
        [SerializeField] private EditGameSlotView m_EditGameSlotView;

        // private GameSaveSlotViewModel m_GameSaveViewModel1;
        // private GameSaveSlotViewModel m_GameSaveViewModel2;
        // private GameSaveSlotViewModel m_GameSaveViewModel3;

        private List<GameSaveSlotViewModel> m_GameSaveViewModelCollection;
        
        private IGameSaveFacade m_GameSaveFacade;
        private IObjectMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IGameSaveSelectionMenuController.InitializeGameSelectionMenuController(
            IGameSaveFacade gameSaveFacade,
            IObjectMapper objectMapper)
        {
            this.m_GameSaveFacade = gameSaveFacade;
            this.m_Mapper = objectMapper;

            this.m_GameSaveViewModelCollection = new List<GameSaveSlotViewModel>
            {
                new(this.m_GameSaveSelectionMenuView.GameSaveSlotCollection.ElementAt(0),
                    this.m_GameSaveSelectionMenuView.MenuButtons),
                new(this.m_GameSaveSelectionMenuView.GameSaveSlotCollection.ElementAt(1),
                    this.m_GameSaveSelectionMenuView.MenuButtons),
                new(this.m_GameSaveSelectionMenuView.GameSaveSlotCollection.ElementAt(2),
                    this.m_GameSaveSelectionMenuView.MenuButtons)
            };
            
            this.m_GameSaveFacade.GetGameSaves(this);
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -
        void IGetGameSavesOutputPort.PresentGameSaves(IEnumerable<GameSaveModel> gameSaves)
        {
            int _SlotIndex = 0;
            foreach (GameSaveSlotViewModel _ViewModel in this.m_GameSaveViewModelCollection)
            {
                if (_SlotIndex < gameSaves.Count())
                {
                    Debug.Log(gameSaves.Count());
                    GameSaveModel _Model = gameSaves.ElementAt(_SlotIndex);
                    this.m_Mapper.Map(_Model, _ViewModel);
                    _ViewModel.DisplayGameSaveSlot();
                }
                else
                {
                    _ViewModel.DisplayEmptySlot();
                }

                _SlotIndex++;
            }
        }

        void IScreenStateController.HideScreen() 
            => this.m_ContentGroup.SetActive(false);

        void IScreenStateController.ShowScreen() 
            => this.m_ContentGroup.SetActive(true);

        #endregion Methods

    }

}