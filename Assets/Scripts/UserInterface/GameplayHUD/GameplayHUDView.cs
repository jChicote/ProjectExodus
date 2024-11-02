using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.GameplayHUD
{

    public class GameplayHUDView : MonoBehaviour, IGameplayHUDView
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private Slider m_PlatingHealthBar;
        [SerializeField] private Slider m_ShieldHealthBar;
        [SerializeField] private Slider m_WeaponAmmoCountBar;
        
        [Space]
        [SerializeField] private Button m_PauseButton;

        private int m_MaxAmmoCount;
        private float m_MaxPlatingHealth;
        private float m_MaxShieldHealth;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IGameplayHUDView.Initialize(int maxAmmoCount, float maxPlatingHealth, float maxShieldHealth)
        {
            this.m_MaxAmmoCount = maxAmmoCount;
            this.m_MaxPlatingHealth = maxPlatingHealth;
            this.m_MaxShieldHealth = maxShieldHealth;
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -
        void IGameplayHUDView.BindToViewModel(IGameplayHUDNotifyEvents viewNotifyEvents)
        {
            viewNotifyEvents.OnAmmoCountUpdate += this.UpdateAmmoCount;
            viewNotifyEvents.OnShipHealthUpdate += this.UpdateHealthBars;
            
            this.m_PauseButton.onClick.AddListener(viewNotifyEvents.PauseGameCommand.Execute);
        }

        private void UpdateHealthBars(HealthBarsStatusDto healthBarsDto)
        {
            this.m_PlatingHealthBar.value = healthBarsDto.PlatingHealth / this.m_MaxPlatingHealth;
            this.m_ShieldHealthBar.value = healthBarsDto.ShieldHealth / this.m_MaxShieldHealth;
        }

        private void UpdateAmmoCount(int ammoCount) 
            => this.m_WeaponAmmoCountBar.value = ammoCount / (float)this.m_MaxAmmoCount;

        #endregion Methods

    }

}