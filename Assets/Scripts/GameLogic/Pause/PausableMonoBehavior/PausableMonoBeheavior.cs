using System;
using UnityEngine;

namespace ProjectExodus.GameLogic.Pause.PausableMonoBehavior
{

    public class PausableMonoBeheavior : MonoBehaviour, IPausable
    {

        #region - - - - - - Fields - - - - - -

        protected bool m_IsPaused;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        void IPausable.Pause()
            => this.m_IsPaused = true;

        void IPausable.Unpause()
            => this.m_IsPaused = false;

        #endregion Methods

    }

}