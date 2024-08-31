using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.ProfileImageSelectionModal
{

    public class ProfileImageView : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [Header("Views")] 
        [SerializeField] private Image m_Image;
        [SerializeField] private Button m_Button;

        private int m_ImageID;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public Button Button => this.m_Button;

        public Image Image => this.m_Image;

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public void BindToViewModel(IProfileImageSelectionViewModelCommands viewModelCommands) 
            => this.m_Button.onClick.AddListener(() 
                => viewModelCommands.SelectProfileImageCommand.Execute(this.m_ImageID));

        public void SetView(int imageID, Sprite image)
        {
            this.m_ImageID = imageID;
            this.m_Image.sprite = image;
        }

        #endregion Methods
  
    }

}