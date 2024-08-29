using UnityEngine;

namespace ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave
{

    public class UpdateGameSaveInputPort
    {

        #region - - - - - - Properties - - - - - -

        public float CompletionProgress { get; set; }
        
        public string GameSaveName { get; set; }
        
        public Sprite SelectedProfileImage { get; set; }

        #endregion Properties
  
    }

}