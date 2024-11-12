using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace ProjectExodus.GameLogic.Pause.PauseController
{

    public class PauseController : MonoBehaviour, IPauseController
    {

        #region - - - - - - Events - - - - - -

        public UnityEvent OnPause;

        public UnityEvent OnUnpause;

        #endregion Events
  
        #region - - - - - - Methods - - - - - -

        // Note: This is an expensive method call.
        void IPauseController.Pause()
        {
            this.OnPause.Invoke();
            
            // Pause all components
            IPausable[] _PausableComponents = this.GetAllPausableComponents();
            foreach (IPausable _PausableComponent in _PausableComponents) 
                _PausableComponent.Pause();
        }

        // Note: This is an expensive method call.
        void IPauseController.UnPause()
        {
            this.OnUnpause.Invoke();
            
            // Unpause all components
            IPausable[] _PausableComponents = this.GetAllPausableComponents();
            foreach (IPausable _PausableComponent in _PausableComponents) 
                _PausableComponent.Unpause();
        }

        void IPauseController.SubscribePauseActionEvent(UnityAction actionToSubscribe) 
            => this.OnPause.AddListener(actionToSubscribe);

        void IPauseController.UnSubscribePauseActionEvent(UnityAction actionToUnsubscribe) 
            => this.OnUnpause.RemoveListener(actionToUnsubscribe);

        void IPauseController.SubscribeUnPauseActionEvent(UnityAction actionToSubscribe)
            => this.OnUnpause.AddListener(actionToSubscribe);
        
        void IPauseController.UnSubscribeUnPauseActionEvent(UnityAction actionToSubscribe)
            => this.OnUnpause.RemoveListener(actionToSubscribe);

        /// <summary>
        /// Only gets pausable components inheriting from PausableMonoBehavior.
        /// </summary>
        private IPausable[] GetAllPausableComponents()
            => Object.FindObjectsByType<PausableMonoBehavior.PausableMonoBehavior>(FindObjectsSortMode.None)
                    .Select(pmb => (IPausable)pmb).ToArray();

        #endregion Methods

    }

}