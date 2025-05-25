using System;

namespace ProjectExodus.Domain.Entities
{

    [Serializable]
    public class GameSave
    {

        #region - - - - - - Fields - - - - - -

        public Guid ID;

        public float CompletionProgress;

        public string GameSaveName;

        public int GameSlotDisplayIndex;

        public DateTime LastAccessedDate;

        public Guid PlayerID;

        public int ProfileImageID;

        #endregion Fields

    }

}