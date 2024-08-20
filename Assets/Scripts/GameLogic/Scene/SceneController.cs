using ProjectExodus.GameLogic.Scene.SceneStartup;
using UnityEngine;

namespace ProjectExodus.GameLogic.Scene
{

    public class SceneController : MonoBehaviour, ISceneController
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private SceneStartupController m_SceneStartupController;

        #endregion Fields

        #region - - - - - - Initialisers - - - - - -

        void ISceneController.InitialiseSceneController()
        {
            this
        }

        #endregion Initialisers

        #region - - - - - - Methods - - - - - -
  
        
        
        #endregion Methods
  
    }

}