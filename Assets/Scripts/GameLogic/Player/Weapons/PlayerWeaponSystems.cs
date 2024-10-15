using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Infrastructure.Providers;
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

        [Header("Debug")] 
        [SerializeField] private GameObject m_WeaponPrefab;

        private List<IWeapon> m_Weapons = new();
        
        #endregion Fields

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start()
        {
            IServiceLocator _ServiceLocator = GameManager.Instance.ServiceLocator;
            IWeaponAssetProvider _WeaponAssetProvider = _ServiceLocator.GetService<IWeaponAssetProvider>();
            
            // Debug Only 
            List<WeaponModel> _DebugWeaponData = new List<WeaponModel>
            {
                new() { AssetID = 0, AssignedBayID = 999 },
                new() { AssetID = 0, AssignedBayID = 888 },
                new() { AssetID = 0, AssignedBayID = 777 }
            };
            
            // Allocate weapons to weapon bays
            foreach (WeaponModel _WeaponModel in _DebugWeaponData)
            {
                WeaponBay _WeaponBay = this.m_WeaponBays.First(wb => wb.Identifier == _WeaponModel.AssignedBayID);
                _WeaponBay.LoadWeaponToBay(_WeaponAssetProvider.Provide(_WeaponModel.AssetID).Asset);

                IWeapon _LoadedWeapon = _WeaponBay.GetAttachedWeapon();
                _LoadedWeapon.InitializeWeapon(_WeaponModel);
                this.m_Weapons.Add(_LoadedWeapon);
            }
        }

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