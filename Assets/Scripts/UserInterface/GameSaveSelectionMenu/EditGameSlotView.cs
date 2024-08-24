using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenuController
{

    public class EditGameSlotView
    {

        #region - - - - - - Fields - - - - - -

        [Header("Views")] 
        [SerializeField] private GameObject m_ContentGroup;
        [SerializeField] private Button m_ExitButton;
        [SerializeField] private TMP_InputField m_DisplayNameInputField;
        [SerializeField] private Image m_SelectedProfileImage;
        [SerializeField] private Button m_SelectedProfileImageButton;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public GameObject ContentGroup => this.m_ContentGroup;

        public Button ExitButton => this.m_ExitButton;

        public TMP_InputField DisplayNameInputField => this.m_DisplayNameInputField;

        public Image SelectedProfileImage => this.m_SelectedProfileImage;

        public Button SelectedProfileImageButton => this.m_SelectedProfileImageButton;

        #endregion Properties

    }

}