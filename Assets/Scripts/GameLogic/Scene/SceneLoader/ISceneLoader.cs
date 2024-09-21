using System.Collections;
using ProjectExodus.GameLogic.Enumeration;

namespace ProjectExodus.GameLogic.Scene.SceneLoader
{

    public interface ISceneLoader
    {

        #region - - - - - - Methods - - - - - -

        IEnumerator LoadScene(GameScenes gameScene);

        #endregion Methods

    }

}