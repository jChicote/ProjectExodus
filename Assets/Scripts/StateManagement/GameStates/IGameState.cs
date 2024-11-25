using System.Collections;

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