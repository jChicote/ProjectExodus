using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectExodus.GameLogic.Pause.PauseController
{

    public class PauseController : MonoBehaviour, IPauseController
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private SceneManager m_SceneManager;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        // Note: This is an expensive method call.
        void IPauseController.PauseAllPausableComponents()
        {
            IPausable[] _PausableComponents = this.GetAllPausableComponents();
            foreach (IPausable _PausableComponent in _PausableComponents) 
                _PausableComponent.Pause();
        }

        // Note: This is an expensive method call.
        void IPauseController.UnPauseAllPausableComponents()
        {
            IPausable[] _PausableComponents = this.GetAllPausableComponents();
            foreach (IPausable _PausableComponent in _PausableComponents) 
                _PausableComponent.Unpause();
        }

        /// <summary>
        /// Only gets pausable components inheriting from PausableMonoBehavior.
        /// </summary>
        private IPausable[] GetAllPausableComponents()
            => Object.FindObjectsByType<PausableMonoBehavior.PausableMonoBehavior>(FindObjectsSortMode.None)
                    .Select(pmb => (IPausable)pmb).ToArray();

        #endregion Methods

    }

}