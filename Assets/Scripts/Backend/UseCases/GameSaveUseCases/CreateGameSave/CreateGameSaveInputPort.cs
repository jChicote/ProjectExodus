using System;
using UnityEngine;

namespace ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave
{

    public class CreateGameSaveInputPort
    {

        #region - - - - - - Properties - - - - - -

        public float CompletionProgress { get; set; }
        
        public string GameSaveName { get; set; }
        
        public int GameSlotDisplayIndex { get; set; }
        
        public DateTime LastAccessedDate { get; set; }
        
        public int ProfileImageID { get; set; }

        #endregion Properties
  
    }

}