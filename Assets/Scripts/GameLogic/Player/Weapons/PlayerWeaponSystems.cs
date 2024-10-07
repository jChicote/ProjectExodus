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

        [Header("Weapon Locations")]
        [SerializeField] private Transform[] m_WeaponLocations;

        [Header("Projectile")]
        [SerializeField] private GameObject m_Projectile;

        private bool m_CanFire;
        private EventTimer m_FireIntervalTimer;

        #endregion Fields

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start()
        {
            // Subscribe to event
            this.m_FireIntervalTimer = new EventTimer(this.m_FireRate, Time.deltaTime, this.FireWeapons);
        }

        private void Update()
        {
            if (this.m_IsPaused || !this.m_CanFire) return;
            
            this.m_FireIntervalTimer.TickTimer();
        }

        #endregion Unity Lifecycle Methods

        #region - - - - - - Methods - - - - - -

        void IPlayerWeaponSystems.ToggleWeaponFire(bool isFiring) 
            => this.m_CanFire = isFiring;

        // Note: In future ships configured with multiple weapon systems we be able to toggle between them.
        private void FireWeapons()
        {
            foreach (Transform _WeaponLocation in this.m_WeaponLocations)
                Object.Instantiate(this.m_Projectile, _WeaponLocation.position, _WeaponLocation.rotation);
        }

        #endregion Methods
  
    }

}