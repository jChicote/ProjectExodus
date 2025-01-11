using UnityEngine;

namespace ProjectExodus.Management.AudioManager
{

    public interface IAudioControls
    {

        #region - - - - - - Events - - - - - -

        void OnPause();

        void OnPlay();

        void OnStop();

        void OnSetVolume();

        #endregion Events

        #region - - - - - - Methods - - - - - -

        void SetAudioClip(AudioClip audioClip);

        #endregion Methods

    }

}