using ProjectExodus.GameLogic.Player.PlayerProvider;
using ProjectExodus.Management.InputManager;

namespace ProjectExodus.GameLogic.Player.PlayerSpawner
{

    public interface IPlayerSpawner
    {

        #region - - - - - - Methods - - - - - -

        void InitialisePlayerSpawner(IInputManager inputManager, IPlayerProvider playerProvider);

        void SpawnPlayer();

        #endregion Methods

    }

}