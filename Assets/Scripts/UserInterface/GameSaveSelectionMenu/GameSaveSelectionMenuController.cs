using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenuController
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
        [SerializeField] private Button m_QuitButton;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        void IScreenStateController.HideScreen() 
            => this.m_ContentGroup.SetActive(false);

        void IScreenStateController.ShowScreen() 
            => this.m_ContentGroup.SetActive(true);

        #endregion Methods
  
    }

}