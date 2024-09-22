using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerProvider
{

    public interface IPlayerProvider
    {

        #region - - - - - - Methods - - - - - -

        GameObject GetActivePlayer();

        void SetActivePlayer(GameObject activePlayer);

        #endregion Methods

    }

}