using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using UnityEngine;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.EditGameSlotModal
{

    public class EditGameSlotViewModel
    {

        #region - - - - - - Fields - - - - - -

        private readonly EditGameSlotView m_EditGameSlotView;
        private readonly IGameSaveFacade m_GameSaveFacade;

        private string m_DisplayName;
        private Sprite m_SelectedProfileImage;

        private int m_EditedGameIndex;
        
        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public EditGameSlotViewModel(EditGameSlotView editGameSlotView)
        {
            this.m_EditGameSlotView = editGameSlotView;
            
            this.m_EditGameSlotView.CreateButton.onClick.AddListener(this.CreateGameSlot);
            this.m_EditGameSlotView.SaveButton.onClick.AddListener(this.UpdateGameSlot);
            this.m_EditGameSlotView.ExitButton.onClick.AddListener(this.ExitModalMenu);
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public string DisplayName
        {
            get => this.m_DisplayName;
            set
            {
                this.m_DisplayName = value;
            }
        }

        public Sprite SelectedProfileImage
        {
            get => this.m_SelectedProfileImage;
            set
            {
                this.m_SelectedProfileImage = value;
            }
        }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        public void ShowModal(GameSaveSlotViewModel gameSaveSlotViewModel, bool isNewGameSave)
        {
            this.m_EditedGameIndex = gameSaveSlotViewModel.DisplayIndex;

            if (isNewGameSave)
            {
                this.DisplayName = "Game Save";
                this.SelectedProfileImage = default(Sprite);
                this.m_EditGameSlotView.CreateButton.gameObject.SetActive(true);
            }
            else
            {
                this.DisplayName = gameSaveSlotViewModel.GameSaveName;
                this.SelectedProfileImage = gameSaveSlotViewModel.ProfileImage;
                this.m_EditGameSlotView.CreateButton.gameObject.SetActive(false);
            }
            
            this.m_EditGameSlotView.ContentGroup.SetActive(true);
        }

        public void CreateGameSlot()
        {
            Debug.Log("[LOG] - Create game slot");
        }

        public void UpdateGameSlot()
        {
            Debug.Log("[LOG] - Update game slot");
        }

        public void ExitModalMenu()
        {
            Debug.Log("[LOG] - Exit modal menu");
            this.m_EditGameSlotView.ContentGroup.SetActive(false);
        }

        #endregion Methods
  
    }

}