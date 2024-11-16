using System;
using System.Linq;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Backend.UseCases.PlayerUseCases.GetPlayer;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Facades.PlayerActionFacades;
using ProjectExodus.GameLogic.OutputHandlers;
using UnityEngine;

namespace ProjectExodus.Management.GameSaveManager
{

    // TODO: Convert the class to a GameDataManager
    public class GameSaveManager : MonoBehaviour, IGameSaveManager
    {

        #region - - - - - - Fields - - - - - -

        private IDataContext m_DataContext;
        private IPlayerActionFacade m_PlayerActionFacade;
        
        // Loaded Data
        private GameSaveModel m_SelectedGameSaveModel;
        private PlayerModel m_SelectedPlayer;

        #endregion Fields
  
        #region - - - - - - Initializers - - - - - -

        void IGameSaveManager.InitializeGameSaveManager(IDataContext dataContext)
            => this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

        #endregion Initializers

        #region - - - - - - Properties - - - - - -

        GameSaveModel IGameSaveManager.GameSaveModel 
            => this.m_SelectedGameSaveModel;

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
            if (this.m_SelectedPlayer == null)
            {
                Debug.LogError("[ERROR]: Save action is aborted. A Player must be loaded in order to be saved.");
                return;
            }
            
            // Save the Ships
            foreach (ShipModel _Ship in this.m_SelectedPlayer.Ships)
            {
                
            }
            
            // Save the Weapons
            foreach (WeaponModel _Weapon in this.m_SelectedPlayer.Ships.SelectMany(s => s.Weapons))
            {
                
            }
        }
        
        #endregion Methods
        
    }
    
}