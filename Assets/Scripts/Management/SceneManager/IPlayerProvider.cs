using UnityEngine;

namespace ProjectExodus.Management.SceneManager
{

    public interface IPlayerProvider
    {

        #region - - - - - - Methods - - - - - -

        GameObject GetActivePlayer();

        #endregion Methods

    }

}