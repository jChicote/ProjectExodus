
// TODO: This component sole responsibility is to add to the EnemyCollection. There might be other ways that track the Enemy object without needing a specific component

using System;
using ProjectExodus;
using ProjectExodus.Utility.GameValidation;
using UnityEngine;

public class EnemyCollectionEntity : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    private EnemyCollection m_SceneEnemyCollection;

    #endregion Fields
  
    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_SceneEnemyCollection = EnemyManager.Instance.EnemyCollection;

        GameValidator.NotNull(
            this.m_SceneEnemyCollection, 
            nameof(m_SceneEnemyCollection),
            sourceObjectName: this.gameObject.name);
        
        this.m_SceneEnemyCollection.AddEnemy(this.gameObject);
    }

    private void OnDestroy() 
        => this.m_SceneEnemyCollection.RemoveEnemy(this.gameObject);

    #endregion Unity Methods

}
