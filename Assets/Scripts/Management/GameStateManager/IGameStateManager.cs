using System.Threading.Tasks;
using ProjectExodus.Management.Enumeration;

namespace ProjectExodus.Management.GameStateManager
{

    public interface IGameStateManager
    {
  
        #region - - - - - - Methods - - - - - -
        
        void InitialiseGameStateManager();

        Task ChangeGameState(GameState gameState);

        #endregion Methods

    }

}