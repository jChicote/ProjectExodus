using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.UserInterface.GameSaveSelectionMenu.EditGameSlotModal;
using UnityEngine;
using UserInterface.GameSaveSelectionMenu.Mediator;

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
        [SerializeField] private GameSaveSelectionMenuView m_GameSaveSelectionMenuView;

        [Header("Modals")] 
        [SerializeField] private EditGameSlotView m_EditGameSlotView;
        private EditGameSlotViewModel m_EditGameSlotViewModel;
        
        private List<GameSaveSlotViewModel> m_GameSaveViewModelCollection;
        private GameSaveSlotViewModel m_CurrentlySelectedGameSaveSlot;
        
        private IGameSaveFacade m_GameSaveFacade;
        private IObjectMapper m_Mapper;
        private IGameSaveSelectionMenuMediator m_Mediator;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IGameSaveSelectionMenuController.InitializeGameSelectionMenuController(
            IGameSaveFacade gameSaveFacade,
            IObjectMapper objectMapper)
        {
            this.m_GameSaveFacade = gameSaveFacade;
            this.m_Mapper = objectMapper;

            this.m_Mediator = new GameSaveSelectionMenuMediator();

            this.m_GameSaveViewModelCollection = new List<GameSaveSlotViewModel>
            {
                // new (this.,
                //     this.m_GameSaveSelectionMenuView.GameSaveSlotCollection.ElementAt(1),
                //     _Mediator),
                // new(this.m_GameSaveSelectionMenuView.GameSaveSlotCollection.ElementAt(1),
                //     _Mediator,
                //     this.m_GameSaveSelectionMenuView.MenuButtons),
                // new(this.m_GameSaveSelectionMenuView.GameSaveSlotCollection.ElementAt(2),
                //     _Mediator,
                //     this.m_GameSaveSelectionMenuView.MenuButtons)
            };
            
            

            // this.m_EditGameSlotViewModel = new EditGameSlotViewModel(this.m_EditGameSlotView);
            
            this.m_GameSaveFacade.GetGameSaves(this);
        }

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        void IGetGameSavesOutputPort.PresentGameSaves(IEnumerable<GameSaveModel> gameSaves)
        {
            // 1. This should be changed. Instead it should load the existing slots to vm through index.
            // 2. Then seperately populate remaining view models with empty slots.

            var _OrderedSlots = gameSaves.OrderBy(gsm => gsm.GameSlotDisplayIndex).ToList();
            
            for (int _I = 0 ; _I < this.m_GameSaveSelectionMenuView.GameSaveSlotCollection.Count(); _I++)
            {
                if (_I < _OrderedSlots.Count())
                {
                    GameSaveModel _Model = gameSaves.ElementAt(_I);
                    var _ViewModel = new GameSaveSlotViewModel(_Model,
                                        this.m_GameSaveSelectionMenuView.GameSaveSlotCollection.ElementAt(1),
                                        this.m_Mediator);
                    _ViewModel.DisplayGameSaveSlot();
                }
                else
                {
                    new GameSaveSlotViewModel(null, 
                    _ViewModel.DisplayIndex = _SlotIndex;
                    _ViewModel.DisplayEmptySlot();
                }

                _SlotIndex++;
            }
        }

        void IScreenStateController.HideScreen() 
            => this.m_ContentGroup.SetActive(false);

        void IScreenStateController.ShowScreen() 
            => this.m_ContentGroup.SetActive(true);

        // private void CreateNewGameSlot()
        // {
        //     this.m_EditGameSlotViewModel.ShowModal(this.m_CurrentlySelectedGameSaveSlot, true);
        // }
        //
        // private void OpenEditModal()
        // {
        //     if (this.m_CurrentlySelectedGameSaveSlot == null) return;
        //     this.m_EditGameSlotViewModel.ShowModal(this.m_CurrentlySelectedGameSaveSlot, false);
        // }
        
        #endregion Methods

    }

}