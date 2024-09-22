using ProjectExodus.GameLogic.Camera;
using ProjectExodus.GameLogic.Player.PlayerProvider;
using ProjectExodus.Management.InputManager;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerSpawner
{

    public interface IPlayerSpawner
    {

        #region - - - - - - Methods - - - - - -

        void InitialisePlayerSpawner(
            ICameraController cameraController, 
            IInputManager inputManager, 
            IPlayerProvider playerProvider);

        GameObject SpawnPlayer();

        #endregion Methods

    }

}