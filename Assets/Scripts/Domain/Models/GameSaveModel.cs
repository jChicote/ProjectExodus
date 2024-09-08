using System;

namespace ProjectExodus.Domain.Models
{

    public class GameSaveModel
    {

        #region - - - - - - Properties - - - - - -
        
        public Guid ID { get; set; }
        
        public float CompletionProgress { get; set; }
        
        public string GameSaveName { get; set; } = string.Empty;

        public int GameSlotDisplayIndex { get; set; }
        
        public DateTime LastAccessedDate { get; set; }

        public ProfileImageModel ProfileImage { get; set; } = new(); // Provide default incase of incorrect load

        #endregion Properties

    }

}