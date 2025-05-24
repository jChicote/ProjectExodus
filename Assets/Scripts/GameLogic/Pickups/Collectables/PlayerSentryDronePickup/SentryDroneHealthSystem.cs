using Codice.Client.BaseCommands.Changelist;
using MBT;
using ProjectExodus.GameLogic.Common.Health;
using ProjectExodus.Utility.GameValidation;
using UnityEngine;

public class SentryDroneHealthSystem : MonoBehaviour, IDamageable
{

    #region - - - - - - Fields - - - - - -

    [SerializeField] private Blackboard m_Blackboard;

    private BoolVariable m_IsDeadVariable;
    private float m_Health = 100f; // TODO: Change to set the value through a scriptable object.

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_IsDeadVariable = this.m_Blackboard.GetVariable<BoolVariable>("IsDead");
        
        string _SourceObjectName = this.gameObject.name;
        GameValidator.NotNull(this.m_Blackboard, nameof(m_Blackboard), sourceObjectName: _SourceObjectName);
        GameValidator.NotNull(this.m_IsDeadVariable, nameof(m_IsDeadVariable), sourceObjectName: _SourceObjectName);

        this.m_IsDeadVariable.AddListener(this.DestroySelf);
    }

    #endregion Unity Methods
  
    #region - - - - - - Methods - - - - - -

    public bool CanDamage() 
        => this.m_Health > 0;

    public void SendDamage(float damage) 
        => this.m_Health -= damage;

    private void DestroySelf(bool oldValue, bool newValue)
        => Destroy(this.gameObject);

    #endregion Methods

}
