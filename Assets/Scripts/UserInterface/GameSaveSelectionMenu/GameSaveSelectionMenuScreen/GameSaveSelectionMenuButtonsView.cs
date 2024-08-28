using System;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.GameSaveSelectionMenuScreen
{

    [Serializable]
    public class GameSaveSelectionMenuButtonsView
    {

        #region - - - - - - Fields - - - - - -

        [Header("Views")]
        [SerializeField] private Button m_ClearButton;
        [SerializeField] private Button m_NewGameButton;
        [SerializeField] private Button m_EditButton;
        [SerializeField] private Button m_QuitButton;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public Button ClearButton => this.m_ClearButton;

        public Button NewGameButton => this.m_NewGameButton;

        public Button EditButton => this.m_EditButton;

        public Button QuitButton => this.m_QuitButton;

        #endregion Properties

    }

}