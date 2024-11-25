using System;
using ProjectExodus.GameLogic.Common.Health;
using ProjectExodus.UserInterface.GameplayHUD;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerHealthSystem
{

    public class PlayerHealthSystem : MonoBehaviour, IDamageable, IPlayerHealthSystem
    {

        #region - - - - - - Fields - - - - - -

        private const float MAXIMUM_SUPPORTED_PLATING = 500f;
        private const float MAXIMUM_SUPPORTED_SHIELDS = 500f;

        private IGameplayHUDController m_GameplayHUDController;

        private float m_CurrentPlatingHealth;
        private float m_CurrentShieldHealth;
        private float m_MaxPlatingHealth;
        private float m_MaxShieldHealth;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IPlayerHealthSystem.Initializer(
            IGameplayHUDController gameplayHUDController, 
            float platingHealth, 
            float shieldHealth)
        {
            this.m_GameplayHUDController =
                gameplayHUDController ?? throw new ArgumentNullException(nameof(gameplayHUDController));
            
            this.m_CurrentPlatingHealth = platingHealth;
            this.m_CurrentShieldHealth = shieldHealth;
            this.m_MaxPlatingHealth = platingHealth;
            this.m_MaxShieldHealth = shieldHealth;
            
            this.m_GameplayHUDController.SetMaxHealthValues(platingHealth, shieldHealth);
            this.m_GameplayHUDController.SetHealthValues(platingHealth, shieldHealth);
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        bool IDamageable.CanDamage() 
            => this.m_CurrentShieldHealth != 0 && this.m_CurrentPlatingHealth != 0;

        void IDamageable.SendDamage(float damage)
        {
            float _PlatingDamage = 
                this.m_CurrentShieldHealth > damage ? 0 : damage - this.m_CurrentShieldHealth;

            if (this.m_CurrentShieldHealth > 0)
                this.m_CurrentShieldHealth = 
                    Math.Clamp(this.m_CurrentShieldHealth - damage, 0, this.m_MaxShieldHealth);
            else
                this.m_CurrentPlatingHealth = 
                    Math.Clamp(this.m_CurrentPlatingHealth - _PlatingDamage, 0, this.m_MaxPlatingHealth);
            
            this.m_GameplayHUDController.SetHealthValues(this.m_CurrentPlatingHealth, this.m_CurrentShieldHealth);
        }

        void IPlayerHealthSystem.UpgradePlating(float upgradeValue)
        {
            this.m_MaxPlatingHealth =
                Math.Clamp(this.m_CurrentPlatingHealth + upgradeValue, 0, MAXIMUM_SUPPORTED_PLATING);
            this.m_CurrentPlatingHealth = this.m_MaxPlatingHealth;
            
            // TODO: The GUD controller needs to animate and await for the health to approach full bar.
            this.m_GameplayHUDController.SetHealthValues(this.m_CurrentPlatingHealth, this.m_CurrentShieldHealth);
        }

        void IPlayerHealthSystem.UpgradeShields(float upgradeValue)
        {
            this.m_MaxShieldHealth =
                Math.Clamp(this.m_CurrentShieldHealth + upgradeValue, 0, MAXIMUM_SUPPORTED_SHIELDS);
            this.m_CurrentShieldHealth = this.m_MaxShieldHealth;
            
            // TODO: The GUD controller needs to animate and await for the health to approach full bar.
            this.m_GameplayHUDController.SetHealthValues(this.m_CurrentPlatingHealth, this.m_CurrentShieldHealth);
        }
        
        #endregion Methods
  
    }

}
