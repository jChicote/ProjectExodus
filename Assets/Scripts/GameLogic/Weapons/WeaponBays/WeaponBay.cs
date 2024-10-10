using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectExodus.GameLogic.Weapons.WeaponBays
{

    public class WeaponBay : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public int Identifier;
        
        private IWeapon m_AttachedWeapon;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void LoadWeaponToBay(GameObject weaponPrefab)
        {
            GameObject _SpawnedWeapon = Object.Instantiate(weaponPrefab, this.transform);
            this.m_AttachedWeapon = _SpawnedWeapon.GetComponent<IWeapon>();
        }

        public IWeapon GetAttachedWeapon() 
            => this.m_AttachedWeapon;

        #endregion Methods
  
    }

}