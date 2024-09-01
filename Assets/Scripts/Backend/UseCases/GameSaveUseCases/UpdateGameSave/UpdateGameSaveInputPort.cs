using UnityEngine;

namespace ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave
{

    public class UpdateGameSaveInputPort
    {

        #region - - - - - - Properties - - - - - -

        public float CompletionProgress { get; set; }
        
        public string GameSaveName { get; set; }
        
        public int SelectedProfileImageID { get; set; }

        #endregion Properties
  
    }

}