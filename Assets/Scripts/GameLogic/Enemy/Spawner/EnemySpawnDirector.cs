using System;
using ProjectExodus;
using ProjectExodus.GameLogic.Common.Timers;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.Management.SceneManager;
using ProjectExodus.Utility.GameValidation;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnDirector : PausableMonoBehavior
{

    #region - - - - - - Fields - - - - - -

    private IEnemySpawner m_EnemySpawner;
    private Transform m_PlayerTransform;
    private Camera m_MainCamera;

    [SerializeField] private float m_ControlRadius;
    [SerializeField] private int m_GroupCount;
    [SerializeField] private int m_EnemySpawnTypes; // TODO: Change this into a layermask based bitmask
    [SerializeField] private float m_SpawnIntervalTimeLength;

    private EventTimer m_Timer;
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

        this.m_Timer = new EventTimer(this.m_SpawnIntervalTimeLength, Time.deltaTime, this.SpawnEnemies, canRun: true);
        this.m_PlayerTransform = SceneManager.Instance.SceneController.PlayerProvider.GetActivePlayer().transform;
        SceneManager.Instance.PlayerObserver.OnPlayerSpawned.AddListener(player => this.m_PlayerTransform = player.transform);
        
        this.CalculateScaledScreenDimensions();
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -
    
    public void SpawnEnemies()
    {
        Vector2 _DirectionToPlayer = new Vector2(
            (this.m_PlayerTransform.position - this.transform.position).x,
            (this.m_PlayerTransform.position - this.transform.position).y);
        
        for (int i = 0; i < this.m_GroupCount; i++)
        {
            this.m_EnemySpawner.Spawn(new SpawnerRequest
            {
                SpawnCenterPosition = this.GenerateSpawnPosition(),
                SpawnCount = 10,
                SpawnDirection = this.m_PlayerTransform == null ? Vector2.zero : _DirectionToPlayer,
                SpawnRadius = this.m_ControlRadius
            }, (EnemySpawnFilter)this.m_EnemySpawnTypes);
        }
        
        Debug.Log("Enemies Spawned");
    }

    private void CalculateScaledScreenDimensions()
    {
        float _ScreenWidth = Screen.width;
        Vector3 _LeftWidthPoint =
            this.m_MainCamera.ScreenToWorldPoint(new Vector3(0, _ScreenWidth / 2, this.m_MainCamera.nearClipPlane));
        Vector3 _RightWidthPoint = 
            this.m_MainCamera.ScreenToWorldPoint(new Vector3(_ScreenWidth, _ScreenWidth / 2, this.m_MainCamera.nearClipPlane));
        this.m_Width = Vector3.Distance(_LeftWidthPoint, _RightWidthPoint);

        float _ScreenHeight = Screen.height;
        Vector3 _TopHeightPoint =
            this.m_MainCamera.ScreenToWorldPoint(new Vector3(_ScreenHeight / 2, 0, this.m_MainCamera.nearClipPlane));
        Vector3 _BottomHeightPoint =
            this.m_MainCamera.ScreenToWorldPoint(new Vector3(_ScreenHeight / 2, _ScreenHeight, this.m_MainCamera.nearClipPlane));
        this.m_Height = Vector3.Distance(_TopHeightPoint, _BottomHeightPoint);
    }

    private Vector2 GenerateSpawnPosition()
    {
        Vector2 _RandomPosition = new(
            x: Random.Range(this.m_MainCamera.transform.position.x - this.m_Width / 2,
                this.m_MainCamera.transform.position.y + this.m_Width  / 2),
            y: Random.Range(this.m_MainCamera.transform.position.y - this.m_Height / 2,
                this.m_MainCamera.transform.position.y + this.m_Height / 2));
        return _RandomPosition;
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
