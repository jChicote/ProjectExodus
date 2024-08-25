using System;
using UnityEngine;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu
{

    public class GameSaveSlotViewModel
    {

        #region - - - - - - Fields - - - - - -

        // Views
        private readonly GameSaveSlotView m_GameSaveSlotView;
        private readonly GameSaveSelectionMenuButtonsView m_ButtonsView;

        private float m_CompletionProgress;
        private string m_GameSaveName;
        private int m_DisplayIndex;
        private DateTime m_LastAccessedDate;
        private Sprite m_ProfileImage; // Temporarily won't display image

        private bool m_IsSlotEmpty;
        
        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameSaveSlotViewModel(
            GameSaveSlotView gameSaveSlotView, 
            GameSaveSelectionMenuButtonsView buttonsView)
        {
            this.m_GameSaveSlotView = gameSaveSlotView;
            this.m_ButtonsView = buttonsView;
            
            this.m_GameSaveSlotView.SlotButton.onClick.AddListener(this.OnSlotSelection);
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public Guid ID { get; set; }

        public float CompletionProgress
        {
            get => this.m_CompletionProgress;
            set
            {
                this.m_CompletionProgress = value;
                this.m_GameSaveSlotView.SlotPercentage.text = $"{this.m_CompletionProgress}%";
            }
        }

        public string GameSaveName
        {
            get => this.m_GameSaveName;
            set
            {
                this.m_GameSaveName = value;
                this.m_GameSaveSlotView.SlotTitle.text = this.GameSaveName;
            }
        }

        public int DisplayIndex
        {
            get => this.m_DisplayIndex;
            set
            {
                this.m_DisplayIndex = value;
            }
        }

        public DateTime LastAccessedDate
        {
            get => this.m_LastAccessedDate;
            set
            {
                this.m_LastAccessedDate = value;
                this.m_GameSaveSlotView.SlotLastAccessedDate.text =
                    $"{this.m_LastAccessedDate.Day}/{this.m_LastAccessedDate.Month}/{this.m_LastAccessedDate.Year}";
            }
        }

        #endregion Properties

        #region - - - - - - Events - - - - - -

        private void OnSlotSelection()
        {
            if (this.m_IsSlotEmpty)
            {
                this.m_ButtonsView.ClearButton.interactable = false;
                this.m_ButtonsView.EditButton.interactable = false;
                this.m_ButtonsView.NewGameButton.interactable = true;
            }
            else
            {
                this.m_ButtonsView.ClearButton.interactable = true;
                this.m_ButtonsView.EditButton.interactable = true;
                this.m_ButtonsView.NewGameButton.interactable = false;
            }
        }

        #endregion Events

        #region - - - - - - Methods - - - - - -

        public void DisplayGameSaveSlot()
        {
            this.m_IsSlotEmpty = false;

            this.m_GameSaveSlotView.GameSlotContentGroup.SetActive(true);
            this.m_GameSaveSlotView.EmptySlotContentGroup.SetActive(false);
        }

        public void DisplayEmptySlot()
        {
            this.m_IsSlotEmpty = true;
            
            this.m_GameSaveSlotView.GameSlotContentGroup.SetActive(false);
            this.m_GameSaveSlotView.EmptySlotContentGroup.SetActive(true);
        }

        #endregion Methods
  
    }

}