using UnityEngine;
using UnityEngine.Events;

public class EnemyObserver : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    private UnityEvent<EnemyDeathInfo> m_OnEnemyDeath = new();

    #endregion Fields

    #region - - - - - - Properties - - - - - -

    public UnityEvent<EnemyDeathInfo> OnEnemyDeath 
        => this.m_OnEnemyDeath;

    #endregion Properties

}

public class EnemyDeathInfo
{

    #region - - - - - - Properties - - - - - -
    
    public bool CanAddPoints { get; set; }
    
    public bool CanShowPoints { get; set; }

    public int Points { get; set; }
    
    public Vector2 Position { get; set; }
    
    #endregion Properties
  
}
