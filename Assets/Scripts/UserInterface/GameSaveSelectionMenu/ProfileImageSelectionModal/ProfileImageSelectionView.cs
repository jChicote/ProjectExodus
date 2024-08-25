using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.ProfileImageSelectionModal
{

    public class ProfileImageSelectionView : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [Header("Views")] 
        [SerializeField] private List<ProfileImageView> m_ImageOptions;
        [SerializeField] private Button m_ExitButton;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public Button ExitButton => this.m_ExitButton;

        public List<ProfileImageView> ImageOptions => this.m_ImageOptions;

        #endregion Properties

    }

}