using System;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

namespace ProjectExodus.UserInterface.GameplayHUD
{

    [Obsolete]
    public class GameplayHUDView : MonoBehaviour, IGameplayHUDView
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
  
        #region - - - - - - Methods - - - - - -
        
        void IGameplayHUDView.BindToViewModel(IGameplayHUDNotifyEvents viewNotifyEvents)
        {
            viewNotifyEvents.OnShipHealthUpdate += this.UpdateHealthBars;
            viewNotifyEvents.OnShowGui += () => { this.m_ContentGroup.SetActive(true); };
            viewNotifyEvents.OnHideGui += () => { this.m_ContentGroup.SetActive(false);};
            
            this.m_PauseButton.onClick.AddListener(viewNotifyEvents.PauseGameCommand.Execute);
        }

        void IGameplayHUDView.SetGameplayHUDValues(GameplayHUDValues values)
        {
            this.m_MaxPlatingHealth = values.MaxPlatingHealth;
            this.m_MaxShieldHealth = values.MaxShieldHealth;
        }
        
        private void UpdateHealthBars(HealthBarsStatusDto healthBarsDto)
        {
            this.m_PlatingHealthBar.value = healthBarsDto.PlatingHealth / this.m_MaxPlatingHealth;
            this.m_ShieldHealthBar.value = healthBarsDto.ShieldHealth / this.m_MaxShieldHealth;
        }

        #endregion Methods

    }

    public class GameplayHUDValues
    {

        #region - - - - - - Properties - - - - - -

        public float MaxPlatingHealth { get; set; }
        
        public float MaxShieldHealth { get; set; }

        #endregion Properties
  
    }

}