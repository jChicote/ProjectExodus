using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;
using ProjectExodus.Domain.Services;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.Management.GameSaveManager;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.ScriptableObjects;
using ProjectExodus.StateManagement.ScreenStates;
using ProjectExodus.UserInterface.Controllers;
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

        private IGameSaveFacade m_GameSaveFacade;
        private IGameSaveManager m_GameSaveManager;
        private IObjectMapper m_Mapper;
        private IGameSaveSelectionMenuMediator m_Mediator;
        private IUserInterfaceController m_UserInterfaceController;

        private List<GameSaveSlotViewModel> m_GameSaveViewModelCollection;
        private GameSaveSelectionMenuViewModel m_GameSaveSelectionMenuViewModel;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IGameSaveSelectionMenuController.InitializeGameSelectionMenuController(
            IDataContext dataContext,
            IGameSaveFacade gameSaveFacade,
            IObjectMapper objectMapper)
        {
            IServiceLocator _ServiceLocator = GameManager.Instance.ServiceLocator;
            
            this.m_GameSaveFacade = gameSaveFacade ?? throw new ArgumentNullException(nameof(gameSaveFacade));
            this.m_Mapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
            this.m_GameSaveManager = _ServiceLocator.GetService<IGameSaveManager>();

            var _UserInterfaceManager = _ServiceLocator.GetService<IUserInterfaceManager>();
            this.m_UserInterfaceController = _UserInterfaceManager.GetTheActiveUserInterfaceController() 
                                                ?? throw new NullReferenceException();

            this.m_Mediator = new GameSaveSelectionMenuMediator();
            this.m_GameSaveSelectionMenuViewModel =
                new GameSaveSelectionMenuViewModel(
                    this.m_GameSaveFacade,
                    this.m_GameSaveManager,
                    this.m_Mediator,
                    this.m_GameSaveSelectionMenuView);
            
            _ = new EditGameSlotViewModel(
                    this.m_EditGameSlotView, 
                    this.m_GameSaveFacade,
                    this.m_Mediator, 
                    this.m_Mapper);
            
            _ = new ProfileImageSelectionViewModel(
                    this.m_Mediator,
                    _ServiceLocator.GetService<IProfileImageModelProvider>(),
                    this.m_ProfileImageSelectionView);
            
            // Load game saves to screen
            this.m_GameSaveFacade.GetGameSaves(this);

            IScreenState _GameSaveScreenState = this.GetComponent<IScreenState>();
            _GameSaveScreenState.Initialize();
        }

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        public IScreenStateController GetScreenController()
            => this.m_GameSaveSelectionMenuViewModel;

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
                this.m_GameSaveManager,
                model, 
                ((IGameSaveSelectionView)this.m_GameSaveSelectionMenuView).GetGameSaveSlotViewAtIndex(gameSlotIndex),
                this.m_Mediator,
                this.m_Mapper,
                this.m_UserInterfaceController);
        
        #endregion Methods

    }

}