using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.GameLogic.Weapons;

namespace ProjectExodus
{

    public interface IEnemyWeaponSystem
    {

        #region - - - - - - Methods - - - - - -

        void ToggleWeaponFire();

        #endregion Methods

    }
    
    public class EnemyWeaponSystem : PausableMonoBehavior, IEnemyWeaponSystem, IInitialize<EnemyWeaponSystemInitializerData>
    {

        #region - - - - - - Fields - - - - - -

        private List<IWeapon> m_Weapons;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        public void Initialize(EnemyWeaponSystemInitializerData initializerData)
        {
            this.m_Weapons = this.GetComponentsInChildren<IWeapon>().ToList();

            foreach (IWeapon _Weapon in this.m_Weapons)
            {
                _Weapon.InitializeWeapon(new WeaponModel() // This weapon model basically has no real use.
                {
                    AmmoSizeModifier = 999, // Intended to have max ammo
                    AssetID = 0,
                    AssignedBayID = 0,
                    FireRateModifier = 0,
                    ID = Guid.NewGuid(),
                    ReloadPeriodModifier = 0
                });
            }
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -
        
        public void ToggleWeaponFire()
        {
            foreach (IWeapon _Weapon in this.m_Weapons)
                _Weapon.ToggleWeaponFire(true);
        }

        #endregion Methods

    }

    public class EnemyWeaponSystemInitializerData
    {
    }

}