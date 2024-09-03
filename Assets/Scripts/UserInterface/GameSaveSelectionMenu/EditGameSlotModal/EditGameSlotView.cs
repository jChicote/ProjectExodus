using ProjectExodus.Domain.Models;
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
        [SerializeField] private CanvasGroup m_CanvasGroup;
        
        [Header("Modal Buttons")]
        [SerializeField] private Button m_CreateButton;
        [SerializeField] private Button m_SaveButton;
        [SerializeField] private Button m_ExitButton;
        
        [Header("Game Save Details")]
        [SerializeField] private TMP_InputField m_DisplayNameInputField;
        [SerializeField] private Image m_SelectedProfileImage;
        [SerializeField] private Button m_SelectedProfileImageButton;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void BindToViewModel(EditGameSlotViewModel viewModel)
        {
            viewModel.OnDisplayNameChanged += this.OnDisplayNameChanged;
            viewModel.OnSelectedImageChanged += this.OnSelectedProfileImageChanged;
            viewModel.OnShowEditGameSlotModal += this.ShowSlotModal;
            
            this.m_CreateButton.onClick.AddListener(viewModel.CreateGameSlotCommand.Execute);
            this.m_CreateButton.onClick.AddListener(this.HideModal);
            this.m_SaveButton.onClick.AddListener(viewModel.SaveGameSlotCommand.Execute);
            this.m_SaveButton.onClick.AddListener(this.HideModal);
            this.m_ExitButton.onClick.AddListener(viewModel.ExitModalCommand.Execute);
            this.m_ExitButton.onClick.AddListener(this.HideModal);
            this.m_SelectedProfileImageButton.onClick.AddListener(viewModel.SelectProfileImageCommand.Execute);
            this.m_SelectedProfileImageButton.onClick.AddListener(this.DisableViewInteraction);
            this.m_DisplayNameInputField.onValueChanged.AddListener(viewModel.EditDisplayNameCommand.Execute);
        }
        
        // -------------------------------------
        // Event Handlers
        // -------------------------------------
        
        private void EnableViewInteraction()
        {
            this.m_CanvasGroup.interactable = true;
            this.m_CanvasGroup.blocksRaycasts = true;
        }
        
        private void DisableViewInteraction()
        {
            this.m_CanvasGroup.interactable = false;
            this.m_CanvasGroup.blocksRaycasts = false;
        }

        private void ShowSlotModal(bool isCreatingSave)
        {
            this.EnableViewInteraction();
            
            this.m_CreateButton.gameObject.SetActive(isCreatingSave);
            this.m_SaveButton.gameObject.SetActive(!isCreatingSave);
            this.m_ContentGroup.SetActive(true);
        }
        
        private void OnDisplayNameChanged(string displayName)
            => this.m_DisplayNameInputField.text = !string.IsNullOrWhiteSpace(displayName) 
                                                    ? displayName
                                                    : "My Game Save";

        private void OnSelectedProfileImageChanged(ProfileImageModel selectedImage)
        {
            this.m_SelectedProfileImage.sprite = selectedImage.Image;
            this.EnableViewInteraction();
        }

        private void HideModal() 
            => this.m_ContentGroup.SetActive(false);

        #endregion Methods

    }

}