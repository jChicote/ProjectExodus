using System;
using UnityEngine.Events;

namespace ProjectExodus.GameLogic.Pause.PauseController
{

    public interface IPauseController
    {

        #region - - - - - - Methods - - - - - -

        void Pause();
        
        void UnPause();

        void SubscribePauseActionEvent(UnityAction actionToSubscribe);

        void UnSubscribePauseActionEvent(UnityAction actionToUnsubscribe);

        void SubscribeUnPauseActionEvent(UnityAction actionToSubscribe);

        void UnSubscribeUnPauseActionEvent(UnityAction actionToUnsubscribe);

        #endregion Methods

    }

}