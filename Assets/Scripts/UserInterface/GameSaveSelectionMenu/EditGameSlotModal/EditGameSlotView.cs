using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.EditGameSlotModal
{

    public class EditGameSlotView : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [Header("Content")] 
        [SerializeField] private GameObject m_ContentGroup;
        
        [Header("Modal Buttons")]
        [SerializeField] private Button m_CreateButton;
        [SerializeField] private Button m_SaveButton;
        [SerializeField] private Button m_ExitButton;
        
        [Header("Game Save Details")]
        [SerializeField] private TMP_InputField m_DisplayNameInputField;
        [SerializeField] private Image m_SelectedProfileImage;
        [SerializeField] private Button m_SelectedProfileImageButton;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public GameObject ContentGroup => this.m_ContentGroup;

        public Button CreateButton => this.m_CreateButton;

        public Button SaveButton => this.m_SaveButton;

        public Button ExitButton => this.m_ExitButton;

        public TMP_InputField DisplayNameInputField => this.m_DisplayNameInputField;

        public Image SelectedProfileImage => this.m_SelectedProfileImage;

        public Button SelectedProfileImageButton => this.m_SelectedProfileImageButton;

        #endregion Properties

        #region - - - - - - Unity Methods - - - - - -

        private void Awake()
        {
            this.m_DisplayNameInputField.onSubmit.AddListener(OnDisplayNameInputSubmission);
        }

        #endregion Unity Methods
  
        #region - - - - - - Methods - - - - - -

        public void BindToViewModel(EditGameSlotViewModel viewModel)
        {
            viewModel.DisplayNameChanged += this.OnDisplayNameChanged;
            viewModel.SelectedImageChanged += this.OnSelectedProfileImageChanged;
        }
        
        // ----------------------------
        // ViewModel subscribed methods
        // ----------------------------
        
        private void OnDisplayNameChanged(string displayName)
            => this.DisplayNameInputField.text = displayName;

        private void OnSelectedProfileImageChanged(Sprite selectedImage)
            => this.SelectedProfileImage.sprite = selectedImage;
        
        // ----------------------------
        // View subscribed methods
        // ----------------------------

        private static void OnDisplayNameInputSubmission(string newValue)
        {
            
        }

        #endregion Methods

    }

}