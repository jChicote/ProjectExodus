using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.GameplayHUD
{

    public class GameplayHUDView : MonoBehaviour
    {
        
        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_ContentGroup;
        
        [Space]
        [SerializeField] private Slider m_PlatingHealthBar;
        [SerializeField] private Slider m_ShieldHealthBar;
        
        [Space]
        [SerializeField] private Button m_PauseButton;

        private float m_MaxPlatingHealth;
        private float m_MaxShieldHealth;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public Button PauseButton { get => this.m_PauseButton; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

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

        public void ShowHUD()
            => this.m_ContentGroup.SetActive(true);

        public void HideHUD()
            => this.m_ContentGroup.SetActive(false);

        #endregion Methods

    }

}