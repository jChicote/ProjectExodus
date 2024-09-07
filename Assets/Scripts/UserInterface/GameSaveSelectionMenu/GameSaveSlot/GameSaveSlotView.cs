using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.GameSaveSlot
{

    public class GameSaveSlotView : MonoBehaviour, IGameSaveSlotView
    {

        #region - - - - - - Fields - - - - - -
        
        private const int MAX_DISPLAYNAME_LENGTH = 10;

        [Header("Content Groups")]
        [SerializeField] private GameObject m_GameSlotContentGroup;
        
        [Header("Views")] 
        [SerializeField] private Image m_BackgroundImage;
        [SerializeField] private Slider m_SelectionSlider;
        [SerializeField] private Image m_SlotProfileImage;
        [SerializeField] private Button m_SlotButton;
        [SerializeField] private TMP_Text m_SlotTitle;
        [SerializeField] private TMP_Text m_SlotLastAccessedDate;
        [SerializeField] private TMP_Text m_SlotPercentage;

        [Header("View Parameters")]
        [SerializeField] private Color m_BackgroundColor;
        [SerializeField] private Color m_EmptyBackgroundColor;
        
        #endregion Fields

        #region - - - - - - Methods - - - - - -

        void IGameSaveSlotView.BindToViewModel(IGameSaveSlotNotifyEvents viewModelNotify)
        {
            viewModelNotify.OnDisplayGameSaveSlot += this.DisplayGameSaveSlot;
            viewModelNotify.OnPropertyChangeEvent += this.OnPropertyChanged;
            
            this.m_SlotButton.onClick.AddListener(viewModelNotify.SlotSelectionCommand.Execute);
        }
            
        private void DisplayGameSaveSlot(bool isEmpty)
        {
            if (isEmpty)
            {
                // This will be ideal to invoke when the user clears a slot
                this.m_GameSlotContentGroup.SetActive(false);
                this.m_BackgroundImage.color = this.m_EmptyBackgroundColor;
                return;
            }
            
            this.m_GameSlotContentGroup.SetActive(true);
            this.m_BackgroundImage.color = this.m_BackgroundColor;
        }

        private void OnPropertyChanged(string updateType, object sender)
        {
            switch (updateType)
            {
                case "SlotPercentage":
                    this.m_SlotPercentage.text = $"{(float)sender}%";
                    break;
                case "SlotTitle":
                    var _GameSaveName = (string)sender;
                    this.m_SlotTitle.text = _GameSaveName.Length > MAX_DISPLAYNAME_LENGTH 
                        ? _GameSaveName.Substring(0, MAX_DISPLAYNAME_LENGTH) + "..." 
                        : _GameSaveName;
                    break;
                case "SlotLastAccessedDate":
                    DateTime _LastAccessedDate = ((DateTime)sender);
                    this.m_SlotLastAccessedDate.text =
                        $"{_LastAccessedDate.Day}/" +
                        $"{_LastAccessedDate.Month}/" +
                        $"{_LastAccessedDate.Year}";
                    break;
                case "SlotProfileImage":
                    this.m_SlotProfileImage.sprite = ((Sprite)sender);
                    break;
            }
        }
            
        #endregion Methods
  
    }

}