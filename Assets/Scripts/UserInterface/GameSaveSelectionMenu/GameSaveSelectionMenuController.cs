using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.UserInterface.GameSaveSelectionMenu.EditGameSlotModal;
using UnityEditor;
using UnityEngine;
using UserInterface.GameSaveSelectionMenu.GameSaveSelectionMenuScreen;
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
        private GameSaveSelectionMenuViewModel m_GameSaveSelectionMenuViewModel;
        
        private IGameSaveFacade m_GameSaveFacade;
        private IObjectMapper m_Mapper;
        private IGameSaveSelectionMenuMediator m_Mediator;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IGameSaveSelectionMenuController.InitializeGameSelectionMenuController(
            IGameSaveFacade gameSaveFacade,
            IObjectMapper objectMapper)
        {
            this.m_GameSaveFacade = gameSaveFacade ?? throw new ArgumentNullException(nameof(gameSaveFacade));
            this.m_Mapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));

            this.m_Mediator = new GameSaveSelectionMenuMediator();
            this.m_GameSaveSelectionMenuViewModel = new GameSaveSelectionMenuViewModel(
                this.m_GameSaveFacade,
                this.m_Mediator,
                this.m_GameSaveSelectionMenuView);
            this.m_EditGameSlotViewModel = new EditGameSlotViewModel(this.m_EditGameSlotView, this.m_Mediator, this.m_Mapper);
            
            this.m_GameSaveFacade.GetGameSaves(this);
        }

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        void IGetGameSavesOutputPort.PresentGameSaves(IEnumerable<GameSaveModel> gameSaves)
        {
            var _OrderedSlots = gameSaves.OrderBy(gsm => gsm.GameSlotDisplayIndex).ToList();
            for (int _I = 0 ; _I < this.m_GameSaveSelectionMenuView.GameSaveSlotCollection.Count(); _I++)
            {
                if (_I < _OrderedSlots.Count())
                {
                    GameSaveModel _Model = gameSaves.ElementAt(_I);
                    var _ViewModel = new GameSaveSlotViewModel(
                                        _Model,
                                        this.m_GameSaveSelectionMenuView.GameSaveSlotCollection.ElementAt(_I),
                                        this.m_Mediator,
                                        this.m_Mapper);
                    _ViewModel.DisplayGameSaveSlot();
                }
                else
                {
                    var _ViewModel = new GameSaveSlotViewModel(
                                        new GameSaveModel(), 
                                        this.m_GameSaveSelectionMenuView.GameSaveSlotCollection.ElementAt(_I),
                                        this.m_Mediator,
                                        this.m_Mapper);
                    _ViewModel.DisplayIndex = _I;
                    _ViewModel.DisplayEmptySlot();
                }
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