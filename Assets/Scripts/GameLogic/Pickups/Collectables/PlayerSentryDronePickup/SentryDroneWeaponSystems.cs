using System.Linq;
using MBT;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
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

public class SentryDroneWeaponSystems : PausableMonoBehavior, IWeaponSystem
{

    #region - - - - - - Fields - - - - - -

    [SerializeField] private Blackboard m_Blackboard;
    [SerializeField] private GameObject[] m_WeaponObjects;

    private BoolVariable m_IsOutOfAmmo;
    private ISentryWeapon[] m_Weapons;
    private bool m_IsWeaponsActive;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_IsOutOfAmmo = this.m_Blackboard.GetVariable<BoolVariable>("IsOutOfAmmo");
        
        string _SourceObjectName = this.gameObject.name;
        GameValidator.NotNull(this.m_Blackboard, nameof(m_Blackboard), sourceObjectName: _SourceObjectName);
        GameValidator.NotNull(this.m_IsOutOfAmmo, nameof(m_IsOutOfAmmo), sourceObjectName: _SourceObjectName);

        this.m_Weapons = this.m_WeaponObjects.Select(w => w.GetComponent<ISentryWeapon>()).ToArray();
    }

    private void Update()
    {
        if (this.m_IsPaused) return;

        if (this.m_IsWeaponsActive)
            this.m_IsOutOfAmmo.Value = this.m_Weapons.All(w => w.IsOutOfAmmo);
    }

    #endregion Unity Methods
  
    #region - - - - - - Methods - - - - - -

    public void EnableWeaponFire()
    {
        for (int i = 0; i < this.m_Weapons.Length; i++)
            this.m_Weapons[i].ToggleWeaponFire(true);
        
        this.m_IsWeaponsActive = true;
    }

    public void DisableWeaponFire()
    {
        for (int i = 0; i < this.m_Weapons.Length; i++)
            this.m_Weapons[i].ToggleWeaponFire(false);
        
        this.m_IsWeaponsActive = false;
    }

    #endregion Methods
  
}
