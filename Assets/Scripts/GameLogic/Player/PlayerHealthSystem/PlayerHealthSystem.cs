using System;
using ProjectExodus.GameLogic.Common.Health;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerHealthSystem
{

    public class PlayerHealthSystem : MonoBehaviour, IDamageable, IPlayerHealthSystem
    {

        #region - - - - - - Fields - - - - - -

        private float m_CurrentPlatingHealth;
        private float m_CurrentShieldHealth;
        private float m_MaxPlatingHealth;
        private float m_MaxShieldHealth;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        bool IDamageable.CanDamage() 
            => this.m_CurrentShieldHealth != 0 && this.m_CurrentPlatingHealth != 0;

        void IDamageable.SendDamage(float damage)
        {
            float _PlatingDamage = 
                this.m_CurrentShieldHealth > damage ? 0 : damage - this.m_CurrentShieldHealth;
            
            if (this.m_CurrentShieldHealth >= 0)
                this.m_CurrentShieldHealth -= damage;
            else
                this.m_CurrentPlatingHealth -= _PlatingDamage;
            
            Math.Clamp(this.m_CurrentShieldHealth, 0, this.m_MaxShieldHealth);
            Math.Clamp(this.m_CurrentPlatingHealth, 0, this.m_MaxPlatingHealth);
        }

        void IPlayerHealthSystem.SetHealth(float platingHealth, float shieldHealth)
        {
            this.m_CurrentPlatingHealth = platingHealth;
            this.m_CurrentShieldHealth = shieldHealth;
            this.m_MaxPlatingHealth = platingHealth;
            this.m_MaxShieldHealth = shieldHealth;
        }

        #endregion Methods
  
    }

}