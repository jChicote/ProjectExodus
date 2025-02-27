using ProjectExodus.GameLogic.Pause.PauseController;
using ProjectExodus.GameLogic.Player.PlayerProvider;

namespace ProjectExodus.GameLogic.Scene
{

    public interface ISceneController
    {

        #region - - - - - - Properties - - - - - -

        IPauseController PauseController { get; }
        
        IPlayerProvider PlayerProvider { get; }
        
        IPlayerObserver PlayerObserver { get; }
        
        UnityEngine.Camera Camera { get; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        void InitialiseSceneController();

        bool IsActiveInScene();

        void RunSceneStartup();

        #endregion Methods

    }

}
