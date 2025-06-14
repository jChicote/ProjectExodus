using System.Collections.Generic;
using System.Linq;
using ProjectExodus;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.Management.UserInterfaceManager;
using UnityEngine;
using SceneManager = ProjectExodus.Management.SceneManager.SceneManager;

public class EnemyLocatorHUDController : PausableMonoBehavior, IInitialize
{

    #region - - - - - - Fields - - - - - -

    // Inspector Fields
    [SerializeField] private EnemyLocatorHUDShape m_HUDShape;

    // Required Dependency Fields
    private Transform m_PlayerTransform;
    private Camera m_MainCamera;
    private EnemyLocatorHUDView m_View;
    
    // Runtime Fields
    private Dictionary<int, Transform> m_TargetEnemies = new();
    
    // Circle Fields
    [Header("Circle Fields")]
    [SerializeField] private float m_CircleRadius = 7;
    
    // Ellipse Fields
    [Header("Ellipse Fields")]
    [SerializeField] private float m_EllipseWidth = 20f;  // Max width (2a)
    [SerializeField] private float m_EllipseHeight = 10f; // Max height (2b)
    private Vector2 m_CameraWorldPosition = Vector2.zero;
    private Vector2 m_Direction = Vector2.zero;
    private Vector2 m_NormalizedDirection = Vector2.zero;
    private float m_SemiMajorAxis;
    private float m_SemiMinorAxis;
    
    #endregion Fields

    #region - - - - - - Initializers - - - - - -

    /// <summary>
    /// Initialization method reserved for execution before Unity Start.
    /// </summary>
    public void Initialize()
    {
        this.m_MainCamera = Camera.main;
        SceneManager.Instance.PlayerObserver.OnPlayerSpawned.AddListener(playerTransform =>
            this.m_PlayerTransform = playerTransform.transform);
        
        this.m_SemiMajorAxis = this.m_EllipseWidth / 2;
        this.m_SemiMinorAxis = this.m_EllipseHeight / 2;
    }

    #endregion Initializers
  
    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_View = this.GetComponent<EnemyLocatorHUDView>();
        
        IUIEventCollection _UIEventCollection = UserInterfaceManager.Instance.EventCollectionRegistry;
        _UIEventCollection.RegisterEvent(
            EnemyLocatorHUDEventConstants.AddEnemyMarker, 
            enemyTransform => this.AddEnemy(enemyTransform as Transform));
        _UIEventCollection.RegisterEvent(
            EnemyLocatorHUDEventConstants.RemoveEnemyMarker,
            enemy => this.RemoveEnemy(enemy as GameObject));
        
        // Draw the HUD Shape
        this.m_View.RenderHUDShape(this.m_HUDShape == EnemyLocatorHUDShape.Circle
            ? new Vector2(this.m_CircleRadius, this.m_CircleRadius)
            : new Vector2(this.m_EllipseWidth, this.m_EllipseHeight)
            ,this.m_HUDShape);
    }
    
    private void Update()
    {
        if (this.m_IsPaused || this.m_PlayerTransform == null) return;
        
        if (this.m_HUDShape == EnemyLocatorHUDShape.Circle)
            this.DrawIntoCircle();
        else if (this.m_HUDShape == EnemyLocatorHUDShape.Ellipse)
            this.DrawIntoEllipse();
        // TODO: Support squared shaped HUDs
    }

    private void OnValidate()
    {
        if (this.m_View == null) return;
        
        this.m_SemiMajorAxis = this.m_EllipseWidth / 2;
        this.m_SemiMinorAxis = this.m_EllipseHeight / 2;
        
        // Draw the HUD Shape
        this.m_View.RenderHUDShape(this.m_HUDShape == EnemyLocatorHUDShape.Circle
                ? new Vector2(this.m_CircleRadius, this.m_CircleRadius)
                : new Vector2(this.m_EllipseWidth, this.m_EllipseHeight)
            ,this.m_HUDShape);
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    private void AddEnemy(Transform enemyTransform)
    {
        int _EnemyID = enemyTransform.gameObject.GetInstanceID();
        this.m_TargetEnemies.Add(_EnemyID, enemyTransform);
        this.m_View.AddMarker(_EnemyID);
    }

    private void RemoveEnemy(GameObject enemy)
    {
        int _EnemyID = enemy.GetInstanceID();
        this.m_TargetEnemies.Remove(_EnemyID);
        this.m_View.RemoveMarker(_EnemyID);
    }

    private void DrawIntoCircle()
    {
        for (int _I = 0; _I < this.m_TargetEnemies.Count; _I++)
        {
            this.m_Direction = this.CalculateDirectionToEnemy(this.m_TargetEnemies.ElementAt(_I).Key);
            this.m_NormalizedDirection = this.m_Direction.sqrMagnitude > 0.0001f ? this.m_Direction.normalized : Vector2.zero;
            this.m_CameraWorldPosition = new Vector2(
                this.m_MainCamera.transform.position.x,
                this.m_MainCamera.transform.position.y);
            
            this.m_View.UpdateMarker(
                this.m_TargetEnemies.ElementAt(_I).Key,
                this.m_NormalizedDirection * this.m_CircleRadius + this.m_CameraWorldPosition);
        }
    }

    private void DrawIntoEllipse()
    {
        for (int _I = 0; _I < this.m_TargetEnemies.Count; _I++)
        {
            this.m_Direction = this.CalculateDirectionToEnemy(this.m_TargetEnemies.ElementAt(_I).Key);
            this.m_NormalizedDirection = this.m_Direction.sqrMagnitude > 0.0001f ? this.m_Direction.normalized : Vector2.zero;
            this.m_CameraWorldPosition = new Vector2(
                this.m_MainCamera.transform.position.x,
                this.m_MainCamera.transform.position.y);

            // Calculate the ellipse scale from the semi-major and semi-minor axis
            float _Scale = (this.m_SemiMajorAxis * this.m_SemiMinorAxis) / Mathf.Sqrt(
                ((this.m_SemiMinorAxis * this.m_NormalizedDirection.x) * (this.m_SemiMinorAxis * this.m_NormalizedDirection.x)) +
                ((this.m_SemiMajorAxis * this.m_NormalizedDirection.y) * this.m_SemiMajorAxis * this.m_NormalizedDirection.y));

            this.m_View.UpdateMarker(
                this.m_TargetEnemies.ElementAt(_I).Key,
                this.m_NormalizedDirection * _Scale + this.m_CameraWorldPosition);
        }
    }

    private Vector2 CalculateDirectionToEnemy(int id)
    {
        Transform _EnemyTransform = this.m_TargetEnemies[id];
        Vector3 _ViewportScreenPosition = this.m_MainCamera.WorldToViewportPoint(_EnemyTransform.position);
        _ViewportScreenPosition.x = Mathf.Clamp(_ViewportScreenPosition.x, 0, 1);
        _ViewportScreenPosition.y = Mathf.Clamp(_ViewportScreenPosition.y, 0, 1);

        Vector2 _EnemyScreenPosition = new Vector2(
            _ViewportScreenPosition.x,
            _ViewportScreenPosition.y);
        
        return _EnemyScreenPosition - new Vector2(0.5f, 0.5f); // (0.5, 0.5) is the camera's position in viewport space
    }

    #endregion Methods

    #region - - - - - - Gizmos - - - - - -

    private void OnDrawGizmos()
    {
        // Draw Circle
        Gizmos.color = Color.green;
        int _Segments = 100;
        
        Vector3 _PreviousPoint = Vector3.zero;
        
        for (int i = 0; i <= _Segments; i++)
        {
            if (this.m_HUDShape == EnemyLocatorHUDShape.Circle)
            {
                float angle = (i / (float)_Segments) * Mathf.PI * 2; // Full circle
                float x = this.m_CircleRadius * Mathf.Cos(angle);    // X position
                float y = this.m_CircleRadius * Mathf.Sin(angle);   // Y position
                
                Vector3 _NewPoint = transform.position + new Vector3(x, y, 0);

                if (i > 0)
                    Gizmos.DrawLine(_PreviousPoint, _NewPoint); // Draw line segment

                _PreviousPoint = _NewPoint;
            }
            else if (this.m_HUDShape == EnemyLocatorHUDShape.Ellipse)
            {
                float angle = (i / (float)_Segments) * Mathf.PI * 2; // Full circle
                float x = (this.m_EllipseWidth / 2) * Mathf.Cos(angle);    // X position
                float y = (this.m_EllipseHeight / 2) * Mathf.Sin(angle);   // Y position

                Vector3 _NewPoint = transform.position + new Vector3(x, y, 0);

                if (i > 0)
                    Gizmos.DrawLine(_PreviousPoint, _NewPoint); // Draw line segment

                _PreviousPoint = _NewPoint;
            }
        }
    }

    #endregion Gizmos
  
}

public enum EnemyLocatorHUDShape
{
    Circle,
    Ellipse
}
