using ProjectExodus.UserInterface.GameSaveSelectionMenu.EditGameSlotModal;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu
{

    public class GameSaveSelectionMenuController : MonoBehaviour, IGameSaveSelectionMenuController
    {

        #region - - - - - - Fields - - - - - -

        [Header("GameSave Selection Menu View")] 
        [SerializeField] private GameObject m_ContentGroup;

        [Header("Game Slots")]
        [SerializeField] private GameSaveSlotView m_GameSaveSlot1;
        [SerializeField] private GameSaveSlotView m_GameSaveSlot2;
        [SerializeField] private GameSaveSlotView m_GameSaveSlot3;
        
        [Header("Buttons")]
        [SerializeField] private Button m_ClearButton;
        [SerializeField] private Button m_NewGameButton;
        [SerializeField] private Button m_EditButton;
        [SerializeField] private Button m_QuitButton;

        [Header("Modals")] 
        [SerializeField] private EditGameSlotView m_EditGameSlotView;
        // [SerializeField] private 

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        void IScreenStateController.HideScreen() 
            => this.m_ContentGroup.SetActive(false);

        void IScreenStateController.ShowScreen() 
            => this.m_ContentGroup.SetActive(true);

        #endregion Methods
  
    }

}