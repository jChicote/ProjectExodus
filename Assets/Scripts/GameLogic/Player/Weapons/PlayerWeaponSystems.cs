using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.GameLogic.Weapons;
using ProjectExodus.GameLogic.Weapons.WeaponBays;
using ProjectExodus.Management.UserInterfaceManager;
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

        #region - - - - - - Methods - - - - - -

        void IPlayerWeaponSystems.InitialiseWeaponSystems(
            IWeaponAssetProvider weaponAssetProvider, 
            List<WeaponModel> weapons)
        {
            // Allocate weapons to weapon bays
            foreach (WeaponModel _WeaponModel in weapons)
            {
                WeaponBay _WeaponBay = this.m_WeaponBays
                    .FirstOrDefault(wb => wb.Identifier == _WeaponModel.AssignedBayID);

                if (!_WeaponBay)
                {
                    Debug.LogError("[ERROR]: Cannot assign to weapon bay as no identifier can be matches.");
                    return;
                }
                
                _WeaponBay.LoadWeaponToBay(weaponAssetProvider.Provide(_WeaponModel.AssetID).Asset);

                IWeapon _LoadedWeapon = _WeaponBay.GetAttachedWeapon();
                _LoadedWeapon.InitializeWeapon(_WeaponModel);
                this.m_Weapons.Add(_LoadedWeapon);

                IUIEventMediator _EventMediator = UserInterfaceManager.Instance.EventMediator;
                _EventMediator.Dispatch(GameplayHUDEvents.AddWeaponIndicator.ToString(), new WeaponInfo
                {
                    ID = _LoadedWeapon.GetWeaponID(),
                    WeaponType = _LoadedWeapon.WeaponType
                });
            }
        }

        void IPlayerWeaponSystems.ToggleWeaponFire(bool isFiring)
        {
            foreach (IWeapon _Weapon in this.m_Weapons)
                _Weapon.ToggleWeaponFire(isFiring);
        }

        #endregion Methods
  
    }

}