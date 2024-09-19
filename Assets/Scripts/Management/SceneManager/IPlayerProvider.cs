using UnityEngine;

namespace ProjectExodus.Management.SceneManager
{

    public interface IPlayerProvider
    {

        #region - - - - - - Methods - - - - - -

        GameObject GetActivePlayer();

        void SetActivePlayer(GameObject activePlayer);

        #endregion Methods

    }

}