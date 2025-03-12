using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.GameplayHUD
{

    public class GameplayHUDView : MonoBehaviour
    {
        
        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_ContentGroup;
        
        [Header("Health Bars")]
        [SerializeField] private Slider m_PlatingHealthBar;
        [SerializeField] private Slider m_ShieldHealthBar;

        [Header("Weapon HUD Elements")]
        [SerializeField] private Slider m_WeaponCooldownBar;

        [Header("Movement HUD Elements")]
        [SerializeField] private FadableSlider m_AfterburnFillBar;
        
        [Space]
        [SerializeField] private Button m_PauseButton;

        private float m_MaxPlatingHealth;
        private float m_MaxShieldHealth;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public Button PauseButton { get => this.m_PauseButton; }

        #endregion Properties
  
        #region - - - - - - Health Methods - - - - - -

        public void SetMaxHealthValues(float maxPlating, float maxShield)
        {
            this.m_MaxPlatingHealth = maxPlating;
            this.m_MaxShieldHealth = maxShield;
        }
        
        public void UpdateHealthBars(float platingHealth, float shieldHealth)
        {
            this.m_PlatingHealthBar.value = platingHealth / this.m_MaxPlatingHealth;
            this.m_ShieldHealthBar.value = shieldHealth / this.m_MaxShieldHealth;
        }

        #endregion Health Methods

        #region - - - - - - Weapon Methods - - - - - -

        public void SetDefaultWeaponValues() 
            => this.m_WeaponCooldownBar.value = 0;

        public void UpdateWeaponCooldown(float currentHeatLevel, float maxHeatLevel) 
            => this.m_WeaponCooldownBar.value = currentHeatLevel / maxHeatLevel;

        #endregion Weapon Methods

        #region - - - - - - Movement Methods - - - - - -

        public void UpdateAfterburnFill(float currentFill, float maxFill)
        {
            this.m_AfterburnFillBar.value = currentFill / maxFill;
            this.m_AfterburnFillBar.FadeIn();
        }

        public void HideAfterburn()
            => this.m_AfterburnFillBar.FadeOut();

        #endregion Movement Methods
  
        #region - - - - - - HUD Methods - - - - - -

        public void ShowHUD()
            => this.m_ContentGroup.SetActive(true);

        public void HideHUD()
            => this.m_ContentGroup.SetActive(false);

        #endregion HUD Methods

    }

}