using System;

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
            }
        }

        public string GameSaveName
        {
            get => this.m_GameSaveName;
            set
            {
                this.m_GameSaveName = value;
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
            }
        }

        #endregion Properties

        #region - - - - - - Events - - - - - -

        private void OnSlotSelection()
        {
            if (this.m_IsSlotEmpty)
            {
                this.m_ButtonsView.ClearButton.enabled = false;
                this.m_ButtonsView.EditButton.enabled = false;
            }
            else
            {
                this.m_ButtonsView.ClearButton.enabled = false;
                this.m_ButtonsView.NewGameButton.enabled = false;
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