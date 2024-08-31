using System.Collections.Generic;
using System.Linq;
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
        
        private void ShowProfileImageSelectionModal(Dictionary<int, Sprite> profileImages)
        {
            for (int _Index = 0; _Index < this.m_ImageOptions.Count; _Index++)
            {
                ProfileImageView _ImageOption = this.m_ImageOptions.ElementAt(_Index);
                _ImageOption.SetView(_Index, profileImages
                                        .FirstOrDefault(pi => pi.Key == _Index).Value);
            }
            
            this.m_ContentGroup.SetActive(true);
        }

        private void HideProfileImageSelectorModal() 
            => this.m_ContentGroup.SetActive(false);

        #endregion Methods
  
    }

}