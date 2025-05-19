using System;
using MBT;
using ProjectExodus.GameLogic.Weapons;
using UnityEngine;

public interface IWeaponSystem
{

    #region - - - - - - Methods - - - - - -

    void EnableWeaponFire();
        
    void DisableWeaponFire();

    #endregion Methods

}

public class SentryDroneWeaponSystems : MonoBehaviour, IWeaponSystem
{

    #region - - - - - - Fields - - - - - -

    [SerializeField] private Blackboard m_Blackboard;
    [SerializeField] private GameObject m_WeaponObjects;

    private IWeapon[] m_Weapons;

    #endregion Fields

    #region - - - - - - Methods - - - - - -

    public void EnableWeaponFire()
    {
        throw new NotImplementedException();
    }

    public void DisableWeaponFire()
    {
        throw new NotImplementedException();
    }

    #endregion Methods
  
}
