using ProjectExodus.GameLogic.Scene;

namespace ProjectExodus.Management.SceneManager
{

    public interface ISceneManager
    {

        #region - - - - - - Methods - - - - - -

        void InitialiseSceneManager();

        ISceneController GetActiveSceneController();

        #endregion Methods

    }

}