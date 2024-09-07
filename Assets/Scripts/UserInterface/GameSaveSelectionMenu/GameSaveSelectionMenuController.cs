using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;
using ProjectExodus.Domain.Services;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Infrastructure;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.ScriptableObjects;
using ProjectExodus.UserInterface.GameSaveSelectionMenu.EditGameSlotModal;
using ProjectExodus.UserInterface.GameSaveSelectionMenu.GameSaveSelectionMenuScreen;
using ProjectExodus.UserInterface.GameSaveSelectionMenu.GameSaveSlot;
using ProjectExodus.UserInterface.GameSaveSelectionMenu.ProfileImageSelectionModal;
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
        [SerializeField] private ProfileImageSelectionView m_ProfileImageSelectionView;

        [Header("Settings")] 
        [SerializeField] private UserInterfaceSettings m_UserInterfaceSettings;

        [Space] 
        [SerializeField] private ServiceLocator m_ServiceLocator;
        
        private EditGameSlotViewModel m_EditGameSlotViewModel;
        private ProfileImageSelectionViewModel m_ProfileImageSelectionViewModel;
        private List<GameSaveSlotViewModel> m_GameSaveViewModelCollection;
        private GameSaveSelectionMenuViewModel m_GameSaveSelectionMenuViewModel;

        private IDataContext m_DataContext;
        private IGameSaveFacade m_GameSaveFacade;
        private IObjectMapper m_Mapper;
        private IGameSaveSelectionMenuMediator m_Mediator;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IGameSaveSelectionMenuController.InitializeGameSelectionMenuController(
            IDataContext dataContext,
            IGameSaveFacade gameSaveFacade,
            IObjectMapper objectMapper)
        {
            this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.m_GameSaveFacade = gameSaveFacade ?? throw new ArgumentNullException(nameof(gameSaveFacade));
            this.m_Mapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));

            this.m_Mediator = new GameSaveSelectionMenuMediator();
            this.m_GameSaveSelectionMenuViewModel = 
                new GameSaveSelectionMenuViewModel(
                    this.m_GameSaveFacade,
                    this.m_Mediator,
                    this.m_GameSaveSelectionMenuView);
            
            this.m_EditGameSlotViewModel = 
                new EditGameSlotViewModel(
                    this.m_EditGameSlotView, 
                    this.m_GameSaveFacade,
                    this.m_Mediator, 
                    this.m_Mapper);
            
            this.m_ProfileImageSelectionViewModel = 
                new ProfileImageSelectionViewModel(
                    this.m_Mediator,
                    ((IServiceLocator)this.m_ServiceLocator).GetService<IProfileImageModelProvider>(),
                    this.m_ProfileImageSelectionView);
            
            // Load game data
            this.m_GameSaveFacade.GetGameSaves(this);
        }

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        // -----------------------------------
        // Populate View methods
        // -----------------------------------
        
        void IGetGameSavesOutputPort.PresentGameSaves(IEnumerable<GameSaveModel> gameSaves)
        {
            var _OrderedSlots = gameSaves.OrderBy(gsm => gsm.GameSlotDisplayIndex).ToList();
            for (int _Index = 0 ; _Index < ((IGameSaveSelectionView)this.m_GameSaveSelectionMenuView).GetAllGameSlotCount(); _Index++)
            {
                if (_Index < _OrderedSlots.Count)
                {
                    GameSaveModel _Model = _OrderedSlots.ElementAt(_Index);
                    GameSaveSlotViewModel _ViewModel = this.CreateGameSaveSlotViewModels(_Model, _Index);
                    _ViewModel.DisplayUsedGameSlot();
                }
                else
                {
                    GameSaveModel _Model = new() { GameSlotDisplayIndex = _Index };
                    GameSaveSlotViewModel _ViewModel = this.CreateGameSaveSlotViewModels(_Model, _Index);
                    _ViewModel.DisplayEmptyGameSlot();
                }
            }
        }

        private GameSaveSlotViewModel CreateGameSaveSlotViewModels(GameSaveModel model, int gameSlotIndex)
            => new(
                this.m_DataContext,
                model, 
                ((IGameSaveSelectionView)this.m_GameSaveSelectionMenuView).GetGameSaveSlotViewAtIndex(gameSlotIndex),
                this.m_Mediator,
                this.m_Mapper);
        
        // -----------------------------------
        // Screen visibility methods
        // -----------------------------------

        void IScreenStateController.HideScreen() 
            => this.m_ContentGroup.SetActive(false);

        void IScreenStateController.ShowScreen() 
            => this.m_ContentGroup.SetActive(true);

        #endregion Methods

    }

}