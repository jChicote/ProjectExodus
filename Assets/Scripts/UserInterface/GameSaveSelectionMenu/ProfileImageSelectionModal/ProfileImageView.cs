using ProjectExodus.Domain.Models;
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

        #region - - - - - - Methods - - - - - -

        public void BindToViewModel(IProfileImageSelectionModalNotifyEvents viewModelCommands) 
            => this.m_Button.onClick.AddListener(() 
                => viewModelCommands.SelectProfileImageCommand.Execute(this.GetModelFromViewDetails()));

        public void SetView(int imageID, Sprite image)
        {
            this.m_ImageID = imageID;
            this.m_Image.sprite = image;
        }

        private ProfileImageModel GetModelFromViewDetails()
            => new()
            {
                ID = this.m_ImageID,
                Image = this.m_Image.sprite
            };

        #endregion Methods
  
    }

}