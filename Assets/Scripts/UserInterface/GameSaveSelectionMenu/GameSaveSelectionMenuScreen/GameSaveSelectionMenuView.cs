using System.Collections.Generic;
using System.Linq;
using ProjectExodus.UserInterface.GameSaveSelectionMenu.GameSaveSlot;
using UnityEngine;
using UserInterface.GameSaveSelectionMenu.GameSaveSelectionMenuScreen;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.GameSaveSelectionMenuScreen
{

    public class GameSaveSelectionMenuView : MonoBehaviour, IGameSaveSelectionView
    {

        #region - - - - - - Fields - - - - - -

        [Header("GameSave Selection Menu View")] 
        [SerializeField] private GameObject m_ContentGroup;
        [SerializeField] private CanvasGroup m_CanvasGroup;

        [Header("Game Slots")]
        [SerializeField] private List<GameSaveSlotView> m_GameSaveSlotCollection;
        
        [Space]
        [SerializeField] private GameSaveSelectionMenuButtonsView m_MenuButtons;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        void IGameSaveSelectionView.BindToViewModel(IGameSaveSelectionNotifier viewModelNotifier)
        {
            GameSaveSelectionMenuButtonsView _MenuButtons = this.m_MenuButtons;
            _MenuButtons.NewGameButton.onClick.AddListener(viewModelNotifier.CreateNewGameCommand.Execute);
            _MenuButtons.ClearButton.onClick.AddListener(viewModelNotifier.ClearGameSaveSlotCommand.Execute);
            _MenuButtons.EditButton.onClick.AddListener(viewModelNotifier.EditGameSaveSlotCommand.Execute);
            _MenuButtons.QuitButton.onClick.AddListener(viewModelNotifier.QuitGameCommand.Execute);

            viewModelNotifier.OnDisableViewInteraction += this.DisableViewInteraction;
            viewModelNotifier.OnEnableViewInteraction += this.EnableViewInteraction;
            viewModelNotifier.OnShowEmptySlotButtonOptions += this.ShowEmptySlotMenuButtonOptions;
            viewModelNotifier.OnShowEditSlotButtonOptions += this.ShowEditGameSaveSlotMenuButtonOptions;
        }

        int IGameSaveSelectionView.GetAllGameSlotCount()
            => this.m_GameSaveSlotCollection.Count;

        GameSaveSlotView IGameSaveSelectionView.GetGameSaveSlotViewAtIndex(int index)
            => this.m_GameSaveSlotCollection.ElementAt(index);

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

        private void ShowEmptySlotMenuButtonOptions()
        {
            GameSaveSelectionMenuButtonsView _MenuButtons = this.m_MenuButtons;
            _MenuButtons.ClearButton.interactable = false;
            _MenuButtons.EditButton.interactable = false;
            _MenuButtons.NewGameButton.interactable = true;
        }

        private void ShowEditGameSaveSlotMenuButtonOptions()
        {
            GameSaveSelectionMenuButtonsView _MenuButtons = this.m_MenuButtons;
            _MenuButtons.ClearButton.interactable = true;
            _MenuButtons.EditButton.interactable = true;
            _MenuButtons.NewGameButton.interactable = false;
        }

        #endregion Methods
    }

}