using UnityEngine;

namespace UserInterface.GameSaveSelectionMenu.Dtos
{

    public class GameSaveSlotDto
    {
        
        #region - - - - - - Properties - - - - - -

        public string DisplayName { get; set; }
        
        public int DisplayIndex { get; set; }

        public Sprite ProfileImage { get; set; }

        #endregion Properties
        
    }

}