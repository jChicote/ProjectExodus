using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.PlayerUseCases.CreatePlayer;
using ProjectExodus.Backend.UseCases.PlayerUseCases.GetPlayer;
using ProjectExodus.Backend.UseCases.PlayerUseCases.UpdatePlayer;
using ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip;
using ProjectExodus.Backend.UseCases.ShipUseCases.GetShip;
using ProjectExodus.Backend.UseCases.ShipUseCases.UpdateShip;
using ProjectExodus.Backend.UseCases.WeaponUseCases.CreateWeapon;
using ProjectExodus.Backend.UseCases.WeaponUseCases.UpdateWeapon;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Facades.PlayerActionFacades;
using ProjectExodus.GameLogic.Facades.ShipActionFacade;
using ProjectExodus.GameLogic.Facades.WeaponActionFacade;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.GameLogic.OutputHandlers;
using ProjectExodus.ScriptableObjects.AssetEntities;
using UnityEngine;

namespace ProjectExodus.Management.GameDataManager
{

    public class GameDataManager : MonoBehaviour, IGameDataManager
    {

        #region - - - - - - Fields - - - - - -

        public static GameDataManager Instance;

        private IDataContext m_DataContext;
        private IGameSaveFacade m_GameSaveActions;
        private IPlayerActionFacade m_PlayerActionFacade;
        private IShipActionFacade m_ShipActionFacade;
        private IShipAssetProvider m_ShipAssetProvider;
        private IWeaponActionFacade m_WeaponActionFacade;
        
        // Loaded Data
        private GameSaveModel m_SelectedGameSaveModel;
        private PlayerModel m_SelectedPlayer;

        #endregion Fields
  
        #region - - - - - - Initializers - - - - - -

        void IGameDataManager.InitializeGameSaveManager(
            IDataContext dataContext, 
            IPlayerActionFacade playerActionFacade)
        {
            this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.m_PlayerActionFacade =
                playerActionFacade ?? throw new ArgumentNullException(nameof(playerActionFacade));

            IServiceLocator _ServiceLocator = GameManager.Instance.ServiceLocator;
            this.m_ShipAssetProvider = _ServiceLocator.GetService<IShipAssetProvider>();
            this.m_GameSaveActions = _ServiceLocator.GetService<IGameSaveFacade>();
            this.m_ShipActionFacade = _ServiceLocator.GetService<IShipActionFacade>();
            this.m_WeaponActionFacade = _ServiceLocator.GetService<IWeaponActionFacade>();
        }

        #endregion Initializers

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        #endregion Unity Lifecycle Methods
  
        #region - - - - - - Properties - - - - - -

        GameSaveModel IGameDataManager.GameSaveModel 
            => this.m_SelectedGameSaveModel;

        public List<ShipModel> PlayerShips
            => this.m_SelectedPlayer.Ships;

        public PlayerModel SelectedPlayer
            => this.m_SelectedPlayer;

        public ShipModel SelectedShip { get; set; }
        
        #endregion Properties
  
        #region - - - - - - Methods - - - - - -
        
        void IGameDataManager.SaveGameSave()
        {
            this.SaveGameplayData();
            this.m_DataContext.SaveChanges();
        }

        void IGameDataManager.SetGameSave(GameSaveModel gameSaveModel)
        {
            this.m_SelectedGameSaveModel = gameSaveModel ?? throw new ArgumentNullException(nameof(gameSaveModel));
            this.LoadGameplayData();
        }

        private void LoadGameplayData()
        {
            if (this.m_SelectedGameSaveModel == null)
            {
                Debug.LogError("[ERROR]: A GameSave must be loaded and selected before loading the Player.");
                return;
            }
            
            PlayerOutputResult _PlayerOutput = new PlayerOutputResult();
            this.m_PlayerActionFacade.GetPlayer(
                new GetPlayerInputPort { ID = this.m_SelectedGameSaveModel.PlayerID },
                _PlayerOutput);

            if (_PlayerOutput.IsSuccessful)
            {
                this.m_SelectedPlayer = _PlayerOutput.Result;
                
                // On loading a game save, the first default ship is always selected.
                this.SelectedShip = _PlayerOutput.Result.Ships.First(s => s.AssetID == 0);
            }
        }

        private void SaveGameplayData()
        {
            // Save the player object
            if (this.m_SelectedPlayer == null)
            {
                Debug.LogWarning("[ERROR]: Save action is aborted. A GameSave or Player must be loaded in order to be saved.");
                return;
            }
            
            // Save the Ships
            foreach (ShipModel _Ship in this.m_SelectedPlayer.Ships)
            {
                GetShipOutputResult _ShipResult = new GetShipOutputResult();
                this.m_ShipActionFacade.GetShip(new GetShipInputPort { ID = _Ship.ID }, _ShipResult);

                if (_ShipResult.IsSuccessful)
                {
                    UpdateShipOutputResult _UpdatedShipResult = new UpdateShipOutputResult();
                    this.m_ShipActionFacade.UpdateShip(new UpdateShipInputPort()
                    {
                        ID = _Ship.ID,
                        PlatingHealthModifier = _Ship.PlatingHealthModifier,
                        ShieldHealthModifier = _Ship.ShieldHealthModifier,
                        Weapons = _Ship.Weapons.Select(w => w.ID).ToList()
                    },
                    _UpdatedShipResult);
                }
                else
                {
                    ShipOutputResult _CreatedShipResult = new ShipOutputResult();
                    this.m_ShipActionFacade.CreateShip(new CreateShipInputPort()
                    {
                        AssetID = _Ship.AssetID,
                        PlatingHealthModifier = _Ship.PlatingHealthModifier,
                        ShieldHealthModifier = _Ship.ShieldHealthModifier,
                        Weapons = _Ship.Weapons.Select(w => w.ID).ToList()
                    },
                    _CreatedShipResult);
                }
            }
            
            GetWeaponOutputResult _AllWeaponResult = new GetWeaponOutputResult();
            this.m_WeaponActionFacade.GetWeapons(_AllWeaponResult);
            
            // Save the Weapons
            foreach (WeaponModel _Weapon in this.m_SelectedPlayer.Ships.SelectMany(s => s.Weapons))
            {
                if (_AllWeaponResult.Result.Any(w => w.ID == _Weapon.ID))
                {
                    UpdateWeaponOutputResult _UpdateWeaponResult = new UpdateWeaponOutputResult();
                    this.m_WeaponActionFacade.UpdateWeapon(new UpdateWeaponInputPort
                    {
                        ID = _Weapon.ID,
                        AssignedBayID = _Weapon.AssignedBayID
                    },
                    _UpdateWeaponResult);
                }
                else
                {
                    CreateWeaponOutputResult _CreatedWeaponResult = new CreateWeaponOutputResult();
                    this.m_WeaponActionFacade.CreateWeapon(new CreateWeaponInputPort
                    {
                        AssetID = _Weapon.AssetID,
                        AssignedBay = _Weapon.AssignedBayID
                    },
                    _CreatedWeaponResult);

                    // Note: A new ID will be assigned to the ship, and will be overriden after creation.
                    // This is so that future operations can reliably match between Models and Entities without mismatch.
                    _Weapon.ID = _CreatedWeaponResult.Result.ID;
                }
            }
            
            // Save the Player
            UpdatePlayerOutputResult _PlayerUpdateOutput = new UpdatePlayerOutputResult();
            this.m_PlayerActionFacade.UpdatePlayer(new UpdatePlayerInputPort
            {
                PlayerID = this.m_SelectedPlayer.ID,
                Ships = this.m_SelectedPlayer.Ships.Select(s => s.ID).ToList()
            },
            _PlayerUpdateOutput);
        }

        public void CreateNewGameSave(
            CreateGameSaveInputPort gameSaveInputPort, 
            ICreateGameSaveOutputPort gameSaveOutputPort)
        {
            // ----------------------
            // Create Player
            // ----------------------
            List<CreateWeaponOutputResult> _CreatedWeaponOutputs = new List<CreateWeaponOutputResult>();
            for (int i = 0; i < 3; i++)
            {
                CreateWeaponOutputResult _OutputHandler = new();
                this.m_WeaponActionFacade.CreateWeapon(new()
                {
                    AssetID = 0,
                    AssignedBay = i
                }, _OutputHandler);
                _CreatedWeaponOutputs.Add(_OutputHandler);
            }
            
            // Create a starting ship for the new player
            ShipAssetObject _StartingShip = this.m_ShipAssetProvider.Provide(0);
            ShipOutputResult _CreatedShip = new ShipOutputResult();
            this.m_ShipActionFacade.CreateShip(new CreateShipInputPort
            {
                AssetID = _StartingShip.ID, // The default is always 0
                PlatingHealthModifier = 0,
                ShieldHealthModifier = 0,
                Weapons = _CreatedWeaponOutputs.Select(cwor => cwor.Result.ID).ToList()
            },
            _CreatedShip);            
            
            CreatePlayerOutputResult _CreatedPlayer = new CreatePlayerOutputResult();
            this.m_PlayerActionFacade.CreatePlayer(
                new CreatePlayerInputPort { StartShip = _CreatedShip.Result },
                _CreatedPlayer);

            if (!_CreatedPlayer.IsSuccessful)
                Debug.LogError("[ERROR]: A malformed GameSave was created.");
            
            // ----------------------
            // Create GameSave
            // ----------------------
            gameSaveInputPort.PlayerID = _CreatedPlayer.Result.ID;
            this.m_GameSaveActions.CreateGameSave(gameSaveInputPort, gameSaveOutputPort);
        }
        
        #endregion Methods
        
    }
    
}