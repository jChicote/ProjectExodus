using System.Linq;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.GameLogic.Weapons;
using ProjectExodus.GameLogic.Weapons.WeaponBays;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.Weapons
{

    public class PlayerWeaponSystems : PausableMonoBehavior, IPlayerWeaponSystems
    {

        #region - - - - - - Fields - - - - - -

        [Header("Weapons")] 
        [SerializeField] private WeaponBay[] m_WeaponBays;

        private IWeapon[] m_Weapons;

        #endregion Fields

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start() 
            => this.m_Weapons = this.m_WeaponBays
                .Select(_WeaponBay => _WeaponBay.GetAttachedWeapon())
                .ToArray();

        #endregion Unity Lifecycle Methods
  
        #region - - - - - - Methods - - - - - -

        void IPlayerWeaponSystems.ToggleWeaponFire(bool isFiring)
        {
            foreach (IWeapon _Weapon in this.m_Weapons)
                _Weapon.ToggleWeaponFire(isFiring);
        }

        #endregion Methods
  
    }

}