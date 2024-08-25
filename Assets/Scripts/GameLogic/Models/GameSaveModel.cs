using System;

namespace ProjectExodus.GameLogic.Models
{

    public class GameSaveModel
    {

        #region - - - - - - Properties - - - - - -
        
        public Guid ID { get; set; }
        
        public float CompletionProgress { get; set; }
        
        public string GameSaveName { get; set; }
        
        public int GameSlotDisplayIndex { get; set; }
        
        public DateTime LastAccessedDate { get; set; }
        
        public Guid ProfileImageID { get; set; }

        #endregion Properties
  
    }

}