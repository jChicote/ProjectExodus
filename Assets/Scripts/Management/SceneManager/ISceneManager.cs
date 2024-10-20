using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Scene;

namespace ProjectExodus.Management.SceneManager
{

    public interface ISceneManager
    {

        #region - - - - - - Properties - - - - - -

        PlayerModel Player { get; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        void InitialiseSceneManager();

        ISceneController GetActiveSceneController();

        void SetCurrentPlayerModel(PlayerModel player);

        #endregion Methods

    }

}