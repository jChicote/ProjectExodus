using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Domain.Models;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.ProfileImageSelectionModal
{

    public class ProfileImageSelectionView : MonoBehaviour, IProfileImageSelectionView
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_ContentGroup;
        
        [Header("Views")] 
        [SerializeField] private List<ProfileImageView> m_ImageOptions;
        [SerializeField] private Button m_SaveButton;
        [SerializeField] private Button m_ExitButton;

        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Awake()
        {
            this.m_SaveButton.onClick.AddListener(this.HideProfileImageSelectorModal);
            this.m_ExitButton.onClick.AddListener(this.HideProfileImageSelectorModal);
        }

        #endregion Unity Methods
  
        #region - - - - - - Methods - - - - - -

        void IProfileImageSelectionView.BindToViewModel(IProfileImageSelectionViewModelCommands viewModelCommands)
        {
            viewModelCommands.OnShowMenuModalWithImage += this.ShowProfileImageSelectionModal;
            this.m_SaveButton.onClick.AddListener(viewModelCommands.SaveSelectionCommand.Execute);

            // Bind Sub-Views
            foreach (ProfileImageView _ImageOptionView in this.m_ImageOptions)
                _ImageOptionView.BindToViewModel(viewModelCommands);
        }
        
        private void ShowProfileImageSelectionModal(List<ProfileImageModel> profileImages)
        {
            for (int _Identifier = 0; _Identifier < this.m_ImageOptions.Count; _Identifier++)
            {
                ProfileImageView _ImageOption = this.m_ImageOptions.ElementAt(_Identifier);
                _ImageOption.SetView(
                                _Identifier, 
                                profileImages.FirstOrDefault(pi => pi.ID == _Identifier)?.Image);
            }
            
            this.m_ContentGroup.SetActive(true);
        }

        private void HideProfileImageSelectorModal() 
            => this.m_ContentGroup.SetActive(false);
        
        #endregion Methods
  
    }

}