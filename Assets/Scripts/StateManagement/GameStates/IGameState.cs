using System.Collections;
using System.Threading.Tasks;

namespace ProjectExodus.StateManagement.GameStates
{

    public interface IGameState
    {

        #region - - - - - - Methods - - - - - -

        IEnumerator StartState();

        IEnumerator EndState();

        #endregion Methods

    }

}