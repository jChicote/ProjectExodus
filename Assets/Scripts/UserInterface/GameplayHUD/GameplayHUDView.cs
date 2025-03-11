using System.Collections;
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
        [SerializeField] private CanvasGroup m_AfterburnGroup;
        [SerializeField] private Slider m_AfterburnFillBar;
        private bool m_IsFadingIn;
        private bool m_IsFadingOut;
        
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
        {
            this.m_WeaponCooldownBar.value = 0;
        }

        public void UpdateWeaponCooldown(float currentHeatLevel, float maxHeatLevel) 
            => this.m_WeaponCooldownBar.value = currentHeatLevel / maxHeatLevel;

        #endregion Weapon Methods

        #region - - - - - - Movement Methods - - - - - -

        public void UpdateAfterburnFill(float currentFill, float maxFill) 
            => this.m_AfterburnFillBar.value = currentFill / maxFill;

        public void FadeInAfterburnFill()
        {
            if (Mathf.Approximately(this.m_AfterburnGroup.alpha, 1) || this.m_IsFadingIn) return;
            this.StartCoroutine(this.AnimateFade(FadeDirection.In, 0.2f));
            this.m_IsFadingIn = true;
        }

        public void FadeOutAfterburnFill()
        {
            this.m_IsFadingIn = false;
            this.StartCoroutine(this.AnimateFade(FadeDirection.Out, 0.8f));
        }

        private IEnumerator AnimateFade(FadeDirection fadeDirection, float fadeTime)
        {
            float _CurrentFadeTime = 0;
            while (_CurrentFadeTime < fadeTime)
            {
                this.m_AfterburnGroup.alpha = fadeDirection == FadeDirection.In
                    ? _CurrentFadeTime / fadeTime
                    : fadeTime - _CurrentFadeTime / fadeTime;

                _CurrentFadeTime += Time.deltaTime;
                yield return null;
            }
        }

        #endregion Movement Methods
  
        #region - - - - - - HUD Methods - - - - - -

        public void ShowHUD()
            => this.m_ContentGroup.SetActive(true);

        public void HideHUD()
            => this.m_ContentGroup.SetActive(false);

        #endregion HUD Methods

    }

    public enum FadeDirection
    {
        In,
        Out
    }

}