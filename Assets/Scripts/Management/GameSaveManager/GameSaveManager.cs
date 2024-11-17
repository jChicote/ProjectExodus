using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Backend.UseCases.PlayerUseCases.GetPlayer;
using ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip;
using ProjectExodus.Backend.UseCases.ShipUseCases.GetShip;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Facades.PlayerActionFacades;
using ProjectExodus.GameLogic.Facades.ShipActionFacade;
using ProjectExodus.GameLogic.OutputHandlers;
using UnityEngine;

namespace ProjectExodus.Management.GameSaveManager
{

    // TODO: Convert the class to a GameDataManager
    public class GameSaveManager : MonoBehaviour, IGameSaveManager
    {

        #region - - - - - - Fields - - - - - -

        public static GameSaveManager Instance;

        private IDataContext m_DataContext;
        private IPlayerActionFacade m_PlayerActionFacade;
        private IShipActionFacade m_ShipActionFacade;
        
        // Loaded Data
        private GameSaveModel m_SelectedGameSaveModel;
        private PlayerModel m_SelectedPlayer;

        #endregion Fields
  
        #region - - - - - - Initializers - - - - - -

        void IGameSaveManager.InitializeGameSaveManager(
            IDataContext dataContext, 
            IPlayerActionFacade playerActionFacade)
        {
            this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.m_PlayerActionFacade =
                playerActionFacade ?? throw new ArgumentNullException(nameof(playerActionFacade));
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

        GameSaveModel IGameSaveManager.GameSaveModel 
            => this.m_SelectedGameSaveModel;

        public List<ShipModel> PlayerShips
            => this.m_SelectedPlayer.Ships;
            
        #endregion Properties
  
        #region - - - - - - Methods - - - - - -
        
        void IGameSaveManager.SaveGameSave()
        {
            this.SaveGameplayData();
            this.m_DataContext.SaveChanges();
        }

        void IGameSaveManager.SetGameSave(GameSaveModel gameSaveModel)
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
            
            GetPlayerOutputResult _PlayerOutput = new GetPlayerOutputResult();
            this.m_PlayerActionFacade.GetPlayer(
                new GetPlayerInputPort { ID = this.m_SelectedGameSaveModel.PlayerID },
                _PlayerOutput);

            if (_PlayerOutput.IsSuccessful)
                this.m_SelectedPlayer = _PlayerOutput.Result;
        }

        private void SaveGameplayData()
        {
            // Save the player object
            if (this.m_SelectedGameSaveModel == null || this.m_SelectedPlayer == null)
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
                    CreateShipOutputResult _CreatedShipResult = new CreateShipOutputResult();
                    this.m_ShipActionFacade.CreateShip();
                }
                else
                {
                    CreateShipOutputResult _CreatedShipResult = new CreateShipOutputResult();
                    this.m_ShipActionFacade.CreateShip(new CreateShipInputPort()
                    {
                        AssetID = _Ship.AssetID,
                        Weapons = _Ship.Weapons.Select(w => w.ID).ToList()
                    },
                    _CreatedShipResult);
                }
            }
            
            // Save the Weapons
            foreach (WeaponModel _Weapon in this.m_SelectedPlayer.Ships.SelectMany(s => s.Weapons))
            {
                
            }
        }
        
        #endregion Methods
        
    }
    
}