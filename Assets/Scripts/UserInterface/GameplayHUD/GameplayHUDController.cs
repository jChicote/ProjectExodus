using UnityEngine;

namespace ProjectExodus.UserInterface.GameplayHUD
{

    public class GameplayHUDController : MonoBehaviour, IGameplayHUDController, IScreenStateController
    {

        #region - - - - - - Fields - - - - - -

        private GameplayHUDView m_View;

        #endregion Fields
  
        #region - - - - - - Initializers - - - - - -

        void IGameplayHUDController.Initialize()
        {
            this.m_View = this.GetComponent<GameplayHUDView>();
            this.m_View.PauseButton.onClick.AddListener(this.PauseGame);
        }

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        void IGameplayHUDController.SetMaxHealthValues(float maxPlating, float maxShield)
            => this.m_View.SetMaxHealthValues(maxPlating, maxShield);

        void IGameplayHUDController.SetHealthValues(float platingHealth, float shieldHealth)
            => this.m_View.UpdateHealthBars(platingHealth, shieldHealth);
        
        void IScreenStateController.HideScreen() 
            => this.m_View.HideHUD();

        void IScreenStateController.ShowScreen()
            => this.m_View.ShowHUD();

        private void PauseGame() 
            => Debug.LogWarning("[WARNING]: No pause behaviour implemented.");

        #endregion Methods

    }

}