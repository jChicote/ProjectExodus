using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Common.Timers;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectExodus.GameLogic.Weapons
{

    public class Turrent : PausableMonoBehavior, IWeapon
    {

        #region - - - - - - Fields - - - - - -

        [Header("Weapon Characteristics")]
        [SerializeField] private float m_FireRate;
        [SerializeField] private float m_Temperature;
        [SerializeField] private float m_ReloadPeriod;
        [SerializeField] private int m_AmmoSize;
        [SerializeField] private Transform m_FiringPoint;
        
        [Header("Projectile")]
        [SerializeField] private GameObject m_Projectile;
        
        private int m_AmmoRemaining;
        private bool m_IsFiring;
        private bool m_IsFiringFirstShot;
        private bool m_IsReloading;
        private EventTimer m_FirstShotTimer;
        private EventTimer m_HeldFireTimer;
        private EventTimer m_ReloadTimer;
        
        // Temporary
        private int m_AssignedBayID = 999;

        #endregion Fields

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start()
        {
            this.m_HeldFireTimer = new EventTimer(this.m_FireRate, Time.deltaTime, this.FireWeapons);
            this.m_FirstShotTimer = new EventTimer(this.m_FireRate, Time.deltaTime, this.ResetFirstRoundFire);
            this.m_ReloadTimer = new EventTimer(this.m_ReloadPeriod, Time.deltaTime, this.ReloadWeapon);
        }
        
        private void Update()
        {
            if (this.m_IsPaused) return;

            if (this.m_IsFiringFirstShot)
                this.m_FirstShotTimer.TickTimer();
            
            this.m_HeldFireTimer.TickTimer();
            
            if (this.m_IsReloading)
                this.m_ReloadTimer.TickTimer();
        }

        #endregion Unity Lifecycle Methods

        #region - - - - - - Methods - - - - - -

        void IWeapon.InitializeWeapon(WeaponModel weaponModel)
        {
            this.m_FireRate += weaponModel.FireRateModifier;
            this.m_ReloadPeriod += weaponModel.ReloadPeriodModifier;
            this.m_AmmoRemaining += weaponModel.AmmoSizeModifier;
            
            this.m_AmmoRemaining = this.m_AmmoSize;
        }

        void IWeapon.ToggleWeaponFire(bool isFiring)
        {
            this.m_IsFiring = isFiring;
            this.FireFirstShot();
        }

        private void FireFirstShot()
        {
            if (this.m_IsFiringFirstShot) return;
            
            this.FireWeapons();
            this.m_IsFiringFirstShot = true;
            this.m_HeldFireTimer.ResetTimer();
        }

        private void FireWeapons()
        {
            if (!this.m_IsFiring || this.m_IsReloading) return;
            
            Object.Instantiate(this.m_Projectile, this.m_FiringPoint.position, this.m_FiringPoint.rotation);
            this.m_AmmoRemaining -= 1;

            if (this.m_AmmoRemaining <= 0)
                this.m_IsReloading = true;
        }

        private void ReloadWeapon()
        {
            this.m_AmmoRemaining = this.m_AmmoSize;
            this.m_IsReloading = false;
            
            this.m_HeldFireTimer.ResetTimer();
            this.m_ReloadTimer.ResetTimer();
        }

        private void ResetFirstRoundFire()
            => this.m_IsFiringFirstShot = false;

        #endregion Methods
  
    }

}