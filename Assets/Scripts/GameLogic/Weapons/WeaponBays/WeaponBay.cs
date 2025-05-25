using ProjectExodus.GameLogic.Enumeration;
using UnityEngine;

namespace ProjectExodus.GameLogic.Weapons.WeaponBays
{

    public class WeaponBay : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public int Identifier;
        
        private IPlayerWeapon m_AttachedWeapon;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void LoadWeaponToBay(GameObject weaponPrefab)
        {
            GameObject _SpawnedWeapon = Instantiate(weaponPrefab, this.transform);
            _SpawnedWeapon.layer = GameLayer.Player;
            this.m_AttachedWeapon = _SpawnedWeapon.GetComponent<IPlayerWeapon>();
        }

        public IPlayerWeapon GetAttachedWeapon() 
            => this.m_AttachedWeapon;

        #endregion Methods
  
    }

}