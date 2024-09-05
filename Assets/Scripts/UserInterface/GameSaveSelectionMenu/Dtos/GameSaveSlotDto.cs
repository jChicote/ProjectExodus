using System;
using ProjectExodus.Domain.Models;

namespace UserInterface.GameSaveSelectionMenu.Dtos
{

    public class GameSaveSlotDto
    {
        
        #region - - - - - - Properties - - - - - -

        public Guid ID;

        public string DisplayName { get; set; }
        
        public int DisplayIndex { get; set; }

        public ProfileImageModel ProfileImage { get; set; }

        #endregion Properties
        
    }

}