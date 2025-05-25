using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for holding reference to all active enemies within the scene.
/// </summary>
public class EnemyCollection : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    [SerializeField] private List<GameObject> m_Enemies;

    #endregion Fields
  
    #region - - - - - - Properties - - - - - -

    public List<GameObject> Enemies => this.m_Enemies;

    #endregion Properties
  
    #region - - - - - - Methods - - - - - -

    public void AddEnemy(GameObject enemy)
        => this.m_Enemies.Add(enemy);

    public void RemoveEnemy(GameObject enemy)
        => this.m_Enemies.Remove(enemy);

    /// <remarks>This only clears the reference to enemies and does not destroy its instance.</remarks>
    public void Clear()
        => this.m_Enemies.Clear();

    #endregion Methods
  
}
