using System;
using System.Linq;
using MBT;
using ProjectExodus.GameLogic.Weapons;
using ProjectExodus.Utility.GameValidation;
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
    [SerializeField] private GameObject[] m_WeaponObjects;

    private IWeapon[] m_Weapons;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        string _SourceObjectName = this.gameObject.name;
        GameValidator.NotNull(this.m_Blackboard, nameof(m_Blackboard), sourceObjectName: _SourceObjectName);

        this.m_Weapons = this.m_WeaponObjects.Select(w => w.GetComponent<IWeapon>()).ToArray();
    }

    #endregion Unity Methods
  
    #region - - - - - - Methods - - - - - -

    public void EnableWeaponFire()
    {
        for (int i = 0; i < this.m_Weapons.Length; i++)
            this.m_Weapons[i].ToggleWeaponFire(true);
    }

    public void DisableWeaponFire()
    {
        for (int i = 0; i < this.m_Weapons.Length; i++)
            this.m_Weapons[i].ToggleWeaponFire(false);
    }

    #endregion Methods
  
}
