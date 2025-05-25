using System;
using System.Collections.Generic;
using System.Linq;
using MBT;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.GameLogic.Weapons;
using UnityEngine;

namespace ProjectExodus
{

    public interface IEnemyWeaponSystem
    {

        #region - - - - - - Methods - - - - - -

        void EnableWeaponFire();
        
        void DisableWeaponFire();

        #endregion Methods

    }
    
    public class EnemyWeaponSystem : PausableMonoBehavior, IEnemyWeaponSystem, IInitialize<EnemyWeaponSystemInitializerData>
    {

        #region - - - - - - Fields - - - - - -

        [RequiredField]
        [SerializeField]
        private Blackboard m_BehaviourTree;
        
        private List<IWeapon> m_Weapons;
        private WeaponSystemsInfo m_WeaponSystemsInfo;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        public void Initialize(EnemyWeaponSystemInitializerData initializerData)
        {
            // this.m_BehaviourTree = this.GetComponent<Blackboard>() 
            //     ?? throw new NullReferenceException(nameof(Blackboard));
            this.m_Weapons = this.GetComponentsInChildren<IWeapon>().ToList();
            this.m_WeaponSystemsInfo = this.GetComponent<WeaponSystemsInfo>()
                                       ?? throw new NullReferenceException(nameof(WeaponSystemsInfo));

            // Setup behaviour tree variables
            WeaponSystemsInfoVariable _WeaponSystemsInfoVariable = this.m_BehaviourTree
                .GetVariable<WeaponSystemsInfoVariable>("WeaponSystemsInfo");
            _WeaponSystemsInfoVariable.Value = this.m_WeaponSystemsInfo;
            this.m_WeaponSystemsInfo.EnableWeaponFireAction = this.EnableWeaponFire;
            this.m_WeaponSystemsInfo.DisableWeaponFireAction = this.DisableWeaponFire;
            
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
        
        // ***********************************************************************************************
        // Note: There is no manual fire as the weapon system only manages whether a weapon 'can fire'.
        // ***********************************************************************************************
        
        public void EnableWeaponFire()
        {
            foreach (IWeapon _Weapon in this.m_Weapons)
                _Weapon.ToggleWeaponFire(true);

            this.m_WeaponSystemsInfo.IsFiring = true;
        }

        public void DisableWeaponFire()
        {
            foreach (IWeapon _Weapon in this.m_Weapons)
                _Weapon.ToggleWeaponFire(false);

            this.m_WeaponSystemsInfo.IsFiring = false;
        }

        #endregion Methods

    }

    public class EnemyWeaponSystemInitializerData
    {
    }

}