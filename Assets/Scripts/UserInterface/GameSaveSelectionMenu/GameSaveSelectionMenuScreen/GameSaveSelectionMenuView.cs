using System.Collections.Generic;
using ProjectExodus.UserInterface.GameSaveSelectionMenu.GameSaveSlot;
using UnityEngine;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.GameSaveSelectionMenuScreen
{

    public class GameSaveSelectionMenuView : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [Header("GameSave Selection Menu View")] 
        [SerializeField] private GameObject m_ContentGroup;

        [Header("Game Slots")]
        [SerializeField] private List<GameSaveSlotView> m_GameSaveSlotCollection;
        
        [Space]
        [SerializeField] private GameSaveSelectionMenuButtonsView m_MenuButtons;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public GameObject ContentGroup => this.m_ContentGroup;

        public List<GameSaveSlotView> GameSaveSlotCollection => this.m_GameSaveSlotCollection;

        public GameSaveSelectionMenuButtonsView MenuButtons => this.m_MenuButtons;

        #endregion Properties

    }

}