using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.DebugSupport.Presenters;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Facades.PlayerControllers;
using ProjectExodus.GameLogic.Facades.ShipActionFacade;
using ProjectExodus.GameLogic.Facades.WeaponActionFacade;

namespace ProjectExodus.DebugSupport.Provider
{

    /// <summary>
    /// A hard-coded provider. Responsible for providing and ensuring there is a default GameSave model with all
    /// developer access to game assets.
    /// </summary>
    /// <remarks>This class is subject to change based on domain requirements.</remarks>
    public class DebugDefaultGameSaveGenerator
    {

        #region - - - - - - Fields - - - - - -

        public GameSaveModel GeneratedGameSave;

        private readonly IGameSaveFacade m_GameSaveFacade;
        private readonly IPlayerControllers m_PlayerControllers;
        private readonly IShipActionFacade m_ShipActionFacade;
        private readonly IWeaponActionFacade m_WeaponActionFacade;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public DebugDefaultGameSaveGenerator(
            IGameSaveFacade gameSaveFacade,
            IPlayerControllers playerControllers,
            IShipActionFacade shipActionFacade,
            IWeaponActionFacade weaponActionFacade)
        {
            this.m_GameSaveFacade = gameSaveFacade ?? throw new ArgumentNullException(nameof(gameSaveFacade));
            this.m_PlayerControllers = playerControllers ?? throw new ArgumentNullException(nameof(playerControllers));
            this.m_ShipActionFacade = shipActionFacade ?? throw new ArgumentNullException(nameof(shipActionFacade));
            this.m_WeaponActionFacade =
                weaponActionFacade ?? throw new ArgumentNullException(nameof(weaponActionFacade));
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public IEnumerator GenerateDefaultGameSave()
        {
            // Create the weapons
            List<DebugCreateWeapoOutputHandler> _CreateWeaponOutputHandlers = new List<DebugCreateWeapoOutputHandler>();
            for (int i = 0; i < 3; i++)
            {
                DebugCreateWeapoOutputHandler _OutputHandler = new();
                this.m_WeaponActionFacade.CreateWeapon(new()
                {
                    AssetID = 0,
                    AssignedBay = i
                }, _OutputHandler);
                _CreateWeaponOutputHandlers.Add(_OutputHandler);
            }

            while (_CreateWeaponOutputHandlers.Any(output => output.Result == null))
                yield return null;

            // Create the ship
            DebugCreateShipOutputHandler _CreateShipOutputHandler = new();
            this.m_ShipActionFacade.CreateShip(new()
            {
                AssetID = 0,
                Weapons = _CreateWeaponOutputHandlers.Select(handler => handler.Result.ID).ToList()
            },
            _CreateShipOutputHandler);

            while (_CreateShipOutputHandler.Result == null)
                yield return null;

            // Create the player
            DebugCreatePlayerOutputHandler _CreatePlayerOutputHandler = new();
            this.m_PlayerControllers.CreatePlayer(new()
            {
                StartShip = _CreateShipOutputHandler.Result
            },
            _CreatePlayerOutputHandler);
            
            while (_CreatePlayerOutputHandler.Result == null)
                yield return null;

            // Create the game save
            DebugCreateGameSaveOutputHandler _CreateGameSaveOutputHandler = new();
            this.m_GameSaveFacade.CreateGameSave(new()
            {
                GameSaveName = DebugGameContansts.DEBUG_GAMESAVENAME,
                GameSlotDisplayIndex = 0,
                LastAccessedDate = DateTime.Now
            },
            _CreateGameSaveOutputHandler);

            // Set the resulting game save
            this.GeneratedGameSave = _CreateGameSaveOutputHandler.Result;
        }

        #endregion Methods
          
    }

}