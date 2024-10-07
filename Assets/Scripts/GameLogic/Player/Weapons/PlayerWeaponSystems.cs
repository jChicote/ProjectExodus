using ProjectExodus.GameLogic.Common.Timers;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.Weapons
{

    public class PlayerWeaponSystems : PausableMonoBehavior, IPlayerWeaponSystems
    {

        #region - - - - - - Fields - - - - - -

        [Header("Weapon Characteristics")]
        [SerializeField] private float m_FireRate;
        [SerializeField] private float m_Temperature;
        [SerializeField] private float m_ReloadPeriod;
        [SerializeField] private int m_AmmoSize;

        [Header("Weapon Locations")]
        [SerializeField] private Transform[] m_WeaponLocations;

        [Header("Projectile")]
        [SerializeField] private GameObject m_Projectile;

        private bool m_IsFiring;
        private bool m_IsFiringFirstRound;
        private bool m_IsReloading;
        private int m_AmmoRemaining;
        private EventTimer m_FireIntervalTimer;
        private EventTimer m_ReloadTimer;

        #endregion Fields

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start()
        {
            // Subscribe to event
            this.m_FireIntervalTimer = new EventTimer(this.m_FireRate, Time.deltaTime, this.FireWeapons);
            this.m_ReloadTimer = new EventTimer(this.m_ReloadPeriod, Time.deltaTime, this.ReloadWeapon);

            this.m_AmmoRemaining = this.m_AmmoSize;
        }

        private void Update()
        {
            if (this.m_IsPaused) return;
            
            if (this.m_IsFiring && !this.m_IsReloading)
                this.m_FireIntervalTimer.TickTimer();
            if (this.m_IsReloading)
                this.m_ReloadTimer.TickTimer();
        }

        #endregion Unity Lifecycle Methods

        #region - - - - - - Methods - - - - - -

        void IPlayerWeaponSystems.ToggleWeaponFire(bool isWeaponLive)
        {
            this.m_IsFiring = isWeaponLive;

            if (!this.m_IsFiring || this.m_IsReloading) return;
            this.m_FireIntervalTimer.ResetTimer();
        }

        // Note: In future ships configured with multiple weapon systems we be able to toggle between them.
        private void FireWeapons()
        {
            // Validate that it CAN fire
            if (!this.m_IsFiring) return;
            
            foreach (Transform _WeaponLocation in this.m_WeaponLocations)
            {
                Object.Instantiate(this.m_Projectile, _WeaponLocation.position, _WeaponLocation.rotation);
                this.m_AmmoRemaining -= 1;
            }

            if (this.m_AmmoRemaining <= 0)
                this.m_IsReloading = true;
        }

        private void ReloadWeapon()
        {
            this.m_AmmoRemaining = this.m_AmmoSize;
            this.m_IsReloading = false;
            
            this.m_FireIntervalTimer.ResetTimer();
            this.m_ReloadTimer.ResetTimer();
        }

        #endregion Methods
  
    }

}