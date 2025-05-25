using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using UnityEngine;

namespace ProjectExodus
{

    public class DestroyAfterEmitterCompletion : PausableMonoBehavior
    {

        #region - - - - - - Fields - - - - - -

        private ParticleSystem m_ParticleSystem;

        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Awake() 
            => this.m_ParticleSystem = this.GetComponent<ParticleSystem>();

        private void Update()
        {
            if (this.m_IsPaused) return;

            if (!this.m_ParticleSystem.isPlaying)
                Destroy(this.gameObject);
        }

        #endregion Unity Methods
  
    }

}