using System;
using ProjectExodus.Backend.UseCases.PlayerUseCases.GetPlayer;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Facades.PlayerActionFacades;
using ProjectExodus.Management.GameSaveManager;
using UnityEngine;

namespace ProjectExodus.GameLogic.Infrastructure.Providers
{

    public interface IPlayerProvider
    {

        #region - - - - - - Methods - - - - - -

        PlayerModel GetCurrentPlayer();
        
        #endregion Methods

    }
    
    public class PlayerProvider: 
        IPlayerProvider,
        IGetPlayerOutputPort
    {

        #region - - - - - - Fields - - - - - -

        private readonly IGameSaveManager m_GameSaveManager;
        private readonly IPlayerActionFacade m_PlayerControllers;

        private PlayerModel m_CurrentPlayer;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public PlayerProvider(IGameSaveManager gameSaveManager, IPlayerActionFacade playerControllers)
        {
            this.m_GameSaveManager = gameSaveManager ?? throw new ArgumentNullException(nameof(gameSaveManager));
            this.m_PlayerControllers = playerControllers ?? throw new ArgumentNullException(nameof(playerControllers));
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -
        
        void IGetPlayerOutputPort.PresentPlayer(PlayerModel player) 
            => this.m_CurrentPlayer = player;

        void IGetPlayerOutputPort.PresentPlayerNotFound() 
            => Debug.LogError("[ERROR]: No Player found in save file.");

        PlayerModel IPlayerProvider.GetCurrentPlayer()
        {
            if (this.m_CurrentPlayer == null)
                Debug.Log("[ERROR]: The player data is missing");

            return this.m_CurrentPlayer;
        }

        #endregion Methods

    }

}