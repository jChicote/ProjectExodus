using UnityEngine;

namespace ProjectExodus.UserInterface.PauseScreen
{

    public class PauseScreenPresenter : MonoBehaviour, IPauseScreenPresenter, IScreenStateController
    {

        #region - - - - - - Fields - - - - - -

        private PauseScreenView m_View; 

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IPauseScreenPresenter.Initialize()
        {
            this.m_View = this.GetComponent<PauseScreenView>();
            this.BindMethodsToView();
        }

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        void IScreenStateController.HideScreen() 
            => this.m_View.HideScreen();

        void IScreenStateController.ShowScreen() 
            => this.m_View.ShowScreen();

        private void BindMethodsToView()
        {
            this.m_View.ResumeButton.onClick.AddListener(this.ResumeGame);
            this.m_View.OptionsButton.onClick.AddListener(this.OpenOptionsMenu);
            this.m_View.ExitButton.onClick.AddListener(this.ExitGameplay);
        }

        private void ResumeGame()
        {
            
        }

        private void OpenOptionsMenu()
        {
            
        }

        private void ExitGameplay()
        {
            
        }

        #endregion Methods

    }

}