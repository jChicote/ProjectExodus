using ProjectExodus.UserInterface.GameSaveSelectionMenu.GameSaveSlot;

namespace UserInterface.GameSaveSelectionMenu.GameSaveSelectionMenuScreen
{

    public interface IGameSaveSelectionView
    {

        #region - - - - - - Methods - - - - - -

        void BindToViewModel(IGameSaveSelectionNotifier viewModelNotifier);

        int GetAllGameSlotCount();

        GameSaveSlotView GetGameSaveSlotViewAtIndex(int index);

        #endregion Methods

    }

}