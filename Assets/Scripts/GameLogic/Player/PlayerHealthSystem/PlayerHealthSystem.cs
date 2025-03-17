using System;
using ProjectExodus.GameLogic.Common.Health;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.UserInterface.GameplayHUD;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerHealthSystem
{

    public class PlayerHealthSystem : MonoBehaviour, IDamageable, IPlayerHealthSystem
    {

        #region - - - - - - Fields - - - - - -

        private const float MAXIMUM_SUPPORTED_PLATING = 500f;
        private const float MAXIMUM_SUPPORTED_SHIELDS = 500f;

        private IUIEventMediator m_GameplayHUDMediator;
        private IPlayerObserver m_PlayerObserver;

        private bool m_IsInvincible;
        private float m_CurrentPlatingHealth;
        private float m_CurrentShieldHealth;
        private float m_MaxPlatingHealth;
        private float m_MaxShieldHealth;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IPlayerHealthSystem.Initializer(
            IGameplayHUDController gameplayHUDController,
            IPlayerObserver playerObserver,
            float platingHealth, 
            float shieldHealth)
        {
            this.m_GameplayHUDMediator = UserInterfaceManager.Instance.EventMediator;
            this.m_PlayerObserver = playerObserver
                ?? throw new ArgumentNullException(nameof(playerObserver));
            
            this.m_CurrentPlatingHealth = platingHealth;
            this.m_CurrentShieldHealth = shieldHealth;
            this.m_MaxPlatingHealth = platingHealth;
            this.m_MaxShieldHealth = shieldHealth;
        }

        #endregion Initializers

        #region - - - - - - Unity Methods - - - - - -

        private void Start() 
            => this.UpdateHealthUI();

        #endregion Unity Methods
  
        #region - - - - - - Methods - - - - - -

        bool IDamageable.CanDamage() 
            => this.m_CurrentShieldHealth != 0 && this.m_CurrentPlatingHealth != 0;

        void IDamageable.SendDamage(float damage)
        {
            if (this.m_IsInvincible) return;
            
            // TODO: The health system needs to be changed to allow for damage of both shields and plating in one single hit.
            float _PlatingDamage = 
                this.m_CurrentShieldHealth > damage ? 0 : damage - this.m_CurrentShieldHealth;

            if (this.m_CurrentShieldHealth > 0)
                this.m_CurrentShieldHealth = 
                    Math.Clamp(this.m_CurrentShieldHealth - damage, 0, this.m_MaxShieldHealth);
            else
                this.m_CurrentPlatingHealth = 
                    Math.Clamp(this.m_CurrentPlatingHealth - _PlatingDamage, 0, this.m_MaxPlatingHealth);
            
            if (this.m_CurrentPlatingHealth <= 0 && this.m_CurrentShieldHealth <= 0)
                this.DestroyPlayer();
            
            this.UpdateHealthUI();
        }

        void IPlayerHealthSystem.UpgradePlating(float upgradeValue)
        {
            this.m_MaxPlatingHealth =
                Math.Clamp(this.m_MaxPlatingHealth + upgradeValue, 0, MAXIMUM_SUPPORTED_PLATING);
            this.m_CurrentPlatingHealth = this.m_MaxPlatingHealth;
            
            this.UpdateHealthUI();
        }

        void IPlayerHealthSystem.UpgradeShields(float upgradeValue)
        {
            this.m_MaxShieldHealth =
                Math.Clamp(this.m_MaxShieldHealth + upgradeValue, 0, MAXIMUM_SUPPORTED_SHIELDS);
            this.m_CurrentShieldHealth = this.m_MaxShieldHealth;
            
            this.UpdateHealthUI();
        }

        void IPlayerHealthSystem.MakeInvincible(bool isInvincible)
        {
            this.m_IsInvincible = isInvincible;
            this.m_CurrentShieldHealth = this.m_MaxShieldHealth;
            this.m_CurrentPlatingHealth = this.m_MaxPlatingHealth;
        }

        private void DestroyPlayer()
        {
            this.m_PlayerObserver.OnPlayerDeath?.Invoke();
            Destroy(this.gameObject);
        }

        private void UpdateHealthUI()
        {
            HealthDto _Health = new HealthDto()
            {
                CurrentPlating = this.m_CurrentPlatingHealth,
                CurrentShield = this.m_CurrentShieldHealth,
                MaxPlating = this.m_MaxPlatingHealth,
                MaxShield = this.m_MaxShieldHealth
            };
            
            this.m_GameplayHUDMediator.Dispatch(GameplayHUDEvents.SetupHealthHUD.ToString(), _Health);
            this.m_GameplayHUDMediator.Dispatch(GameplayHUDEvents.UpdateHealth.ToString(), _Health);
        }

        #endregion Methods
  
    }

}
