using UnityEngine;

namespace ProjectExodus.GameLogic.Weapons.WeaponBays
{

    public class WeaponBay : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_AttachedWeapon;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public IWeapon GetAttachedWeapon() 
            => this.m_AttachedWeapon.GetComponent<IWeapon>();

        #endregion Methods
  
    }

}