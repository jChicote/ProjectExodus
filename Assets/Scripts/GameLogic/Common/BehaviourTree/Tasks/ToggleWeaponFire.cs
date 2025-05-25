using MBT;
using ProjectExodus.Utility.GameValidation;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode(name = "Tasks/Toggle Weapon Fire")]
public class ToggleWeaponFire : Leaf
{

    #region - - - - - - Fields - - - - - -

    private IWeaponSystem m_WeaponSystem;
    
    [SerializeField] private bool m_CanFire;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_WeaponSystem = this.transform.root.GetComponent<IWeaponSystem>();
        
        string _SourceObjectName = this.transform.root.gameObject.name;
        GameValidator.NotNull(this.m_WeaponSystem, nameof(m_WeaponSystem), sourceObjectName: _SourceObjectName);
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    public override NodeResult Execute()
    {
        if (this.m_WeaponSystem == null) return NodeResult.failure;
        
        if (this.m_CanFire)
            this.m_WeaponSystem.EnableWeaponFire();
        else
            this.m_WeaponSystem.DisableWeaponFire();

        return NodeResult.success;
    }

    #endregion Methods
  
}
