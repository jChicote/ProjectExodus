using UnityEngine;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenuController
{

    public class GameSaveSelectionMenuController : MonoBehaviour, IGameSaveSelectionMenuController
    {

        #region - - - - - - Fields - - - - - -

        [Header("GameSave Selection Menu View")] 
        [SerializeField] private GameObject m_ContentGroup;
        

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        void IScreenStateController.HideScreen() 
            => this.m_ContentGroup.SetActive(false);

        void IScreenStateController.ShowScreen() 
            => this.m_ContentGroup.SetActive(true);

        #endregion Methods
  
    }

}