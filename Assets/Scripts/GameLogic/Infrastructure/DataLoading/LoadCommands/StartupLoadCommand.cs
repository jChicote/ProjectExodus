using System;
using ProjectExodus.Backend.UseCases.PlayerUseCases.GetPlayer;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Facades.PlayerActionFacades;
using ProjectExodus.Management.GameDataManager;
using UnityEngine;

namespace ProjectExodus.GameLogic.Infrastructure.DataLoading.LoadCommands
{

    /// <summary>
    /// Responsible for loading data and injecting to scene scoped services.
    /// </summary>
    public class StartupLoadCommand : 
        ILoadCommand<StartupDataOptions>,
        IGetPlayerOutputPort
    {

        #region - - - - - - Fields - - - - - -

        private readonly IPlayerActionFacade m_PlayerControllers;
        private readonly IGameDataManager m_GameSaveManager;

        private bool m_IsComplete = false;
        private StartupDataOptions m_Options;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public StartupLoadCommand(IGameDataManager gameSaveManager, IPlayerActionFacade playerControllers)
        {
            this.m_GameSaveManager = gameSaveManager ?? throw new ArgumentNullException(nameof(gameSaveManager));
            this.m_PlayerControllers = playerControllers ?? throw new ArgumentNullException(nameof(playerControllers));
            
            this.m_Options = new StartupDataOptions();
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void ICommand.Execute()
        {
            GetPlayerInputPort _GetPlayerInputPort = new() { ID = this.m_GameSaveManager.GameSaveModel.PlayerID };
            this.m_PlayerControllers.GetPlayer(_GetPlayerInputPort, this);
        }

        bool ICommand.CanExecute() 
            => this.m_PlayerControllers != null && this.m_GameSaveManager != null && this.m_Options != null;

        string ILoadCommand<StartupDataOptions>.GetLoadCommandName()
            => nameof(StartupLoadCommand);

        StartupDataOptions ILoadCommand<StartupDataOptions>.GetLoadedOptionsObject() 
            => this.m_Options;

        bool ILoadCommand<StartupDataOptions>.IsLoadComplete() 
            => this.m_IsComplete;

        void IGetPlayerOutputPort.PresentPlayer(PlayerModel player)
        {
            this.m_Options.Player = player;
            this.m_IsComplete = true;
        }

        void IGetPlayerOutputPort.PresentPlayerNotFound() 
            => Debug.Log("[ERROR]: The player model has not been found within the save file.");

        #endregion Methods

    }

    public class StartupDataOptions
    {

        #region - - - - - - Properties - - - - - -

        public PlayerModel Player { get; set; }

        #endregion Properties
  
    }

}