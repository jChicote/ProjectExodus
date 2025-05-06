using System.Collections.Generic;
using ProjectExodus;
using ProjectExodus.Utility.GameValidation;
using UnityEngine;

public class EnemySpawnDirector : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    private IEnemySpawner m_EnemySpawner;
    private Camera m_MainCamera;

    [SerializeField] private float m_ControlRadius;
    [SerializeField] private int m_GroupCount;
    [SerializeField] private EnemySpawnFilter m_EnemySpawnTypes;

    private float m_Width;
    private float m_Height;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_MainCamera = Camera.main;
        this.m_EnemySpawner = EnemyManager.Instance.EnemySpawner;

        GameValidator.NotNull(this.m_MainCamera, nameof(m_MainCamera), sourceObjectName: this.gameObject.name);
        GameValidator.NotNull(this.m_EnemySpawner, nameof(m_EnemySpawner), sourceObjectName: this.gameObject.name);
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    public void SpawnEnemies()
    {
        for (int i = 0; i < this.m_GroupCount; i++)
        {
            
        }
    }

    private void GenerateSpawnPosition()
    {
        Vector2 _RandomPosition = new(
            x: Random.Range(this.m_MainCamera.transform.position.x - this.m_Width / 2,
                this.m_MainCamera.transform.position.y + this.m_Width  / 2),
            y: Random.Range(this.m_MainCamera.transform.position.y - this.m_Height / 2,
                this.m_MainCamera.transform.position.y + this.m_Height / 2));
    }

    private void CalculateScaledScreenDimensions()
    {
        float _ScreenWidth = Screen.width;
        Vector3 _LeftWidthPoint =
            this.m_MainCamera.ScreenToWorldPoint(new Vector3(0, _ScreenWidth / 2, this.m_MainCamera.nearClipPlane));
        Vector3 _RightWidthPoint = 
            this.m_MainCamera.ScreenToWorldPoint(new Vector3(_ScreenWidth, _ScreenWidth / 2, this.m_MainCamera.nearClipPlane));
        this.m_Width = Vector3.Distance(_LeftWidthPoint, _RightWidthPoint);
    }

    #endregion Methods
    
  
    #region - - - - - - Gizmos - - - - - -

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, this.m_ControlRadius);
    }

    #endregion Gizmos
  
}
