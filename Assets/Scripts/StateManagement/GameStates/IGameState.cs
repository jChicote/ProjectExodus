using System.Threading.Tasks;

namespace ProjectExodus.StateManagement.GameStates
{

    public interface IGameState
    {

        #region - - - - - - Methods - - - - - -

        Task StartState();

        Task EndState();

        #endregion Methods

    }

}