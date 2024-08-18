using System;

namespace ProjectExodus.Backend.Entities
{

    [Serializable]
    public class GameOptions
    {

        #region - - - - - - Fields - - - - - -

        public Guid ID;

        public int DisplayOption;

        public int DisplayHeight;

        public int DisplayWidth;

        public float EnvironmentFXVolume;

        public float GameMusicVolume;

        public bool IsHUDVisible;

        public bool IsMuted;

        public float MasterVolume;

        public float SoundFXVolume;

        public float UIVolume;

        #endregion Fields

    }

}