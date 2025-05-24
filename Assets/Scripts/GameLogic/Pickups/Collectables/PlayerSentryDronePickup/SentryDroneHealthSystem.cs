using ProjectExodus.GameLogic.Common.Health;
using UnityEngine;

public class SentryDroneHealthSystem : MonoBehaviour, IDamageable
{

    #region - - - - - - Fields - - - - - -

    private float m_Health = 100f; // TODO: Change to set the value through a scriptable object.

    #endregion Fields
  
    #region - - - - - - Methods - - - - - -

    public bool CanDamage() 
        => this.m_Health > 0;

    public void SendDamage(float damage) 
        => this.m_Health -= damage;

    #endregion Methods
  
}
