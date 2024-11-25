using ProjectExodus.GameLogic.Pause.PauseController;

namespace ProjectExodus.GameLogic.Scene
{

    public interface ISceneController
    {

        #region - - - - - - Properties - - - - - -

        IPauseController PauseController { get; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        void InitialiseSceneController();

        bool IsActiveInScene();

        void RunSceneStartup();

        #endregion Methods

    }

}
