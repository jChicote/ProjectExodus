namespace ProjectExodus.GameLogic.Scene
{

    public interface ISceneController
    {

        #region - - - - - - Methods - - - - - -

        void InitialiseSceneController();

        bool IsActiveInScene();

        void RunSceneStartup();

        #endregion Methods

    }

}