using MBT;
using ProjectExodus.Utility.GameValidation;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode(name = "Tasks/Spawn Enemies")]
public class SpawnEnemies : Leaf
{

    #region - - - - - - Fields - - - - - -

    [SerializeField, RequiredField] private EnemySpawnDirector m_SpawnDirector;

    #endregion Fields
  
    #region - - - - - - Unity Methods - - - - - -

    private void Start() 
        => GameValidator.NotNull(this.m_SpawnDirector, nameof(m_SpawnDirector), sourceObjectName: this.transform.root.gameObject.name);

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    public override NodeResult Execute()
    {
        this.m_SpawnDirector.SpawnEnemies();
        return NodeResult.success;
    }

    #endregion Methods
  
}
