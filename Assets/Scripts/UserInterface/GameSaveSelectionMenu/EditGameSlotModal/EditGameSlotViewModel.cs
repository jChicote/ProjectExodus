
using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.EditGameSlotModal
{

    public class EditGameSlotViewModel
    {

        #region - - - - - - Fields - - - - - -

        private readonly EditGameSlotView m_EditGameSlotView;

        private string m_DisplayName;
        private Sprite m_SelectedProfileImage;
        
        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public EditGameSlotViewModel(EditGameSlotView editGameSlotView)
        {
            this.m_EditGameSlotView = editGameSlotView;
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public string DisplayName
        {
            get => this.m_DisplayName;
            set
            {
                this.m_DisplayName = value;
            }
        }

        public Sprite SelectedProfileImage
        {
            get => this.m_SelectedProfileImage;
            set
            {
                this.m_SelectedProfileImage = value;
            }
        }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        public void ShowModal(GameSaveSlotViewModel gameSaveSlotViewModel)
        {
            
        }

        #endregion Methods
  
    }

}