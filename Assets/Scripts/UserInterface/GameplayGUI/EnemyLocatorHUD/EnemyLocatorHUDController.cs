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

    private EnemyLocatorHUDView m_View;
    private Dictionary<int, Transform> m_TargetEnemies = new();
    private Transform m_PlayerTransform;
    // private float m_MaxTrackingRadius = 100f;
    
    public float m_EllipseWidth = 200f;  // Max width (2a)
    public float m_EllipseHeight = 100f; // Max height (2b)

    #endregion Fields

    #region - - - - - - Initializers - - - - - -

    /// <summary>
    /// Initialization method reserved for execution before Unity Start.
    /// </summary>
    public void Initialize()
    {
        SceneManager.Instance.PlayerObserver.OnPlayerSpawned.AddListener(playerTransform =>
            this.m_PlayerTransform = playerTransform.transform);
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
    }
    
    private void Update()
    {
        if (this.m_IsPaused || this.m_PlayerTransform == null) return;
        
        this.DrawIntoEllipse();
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    private void AddEnemy(Transform enemyTransform)
    {
        int _EnemyID = enemyTransform.gameObject.GetInstanceID();
        this.m_TargetEnemies.Add(_EnemyID, enemyTransform);
        this.m_View.AddMarker(_EnemyID);
    }

    private void DrawIntoCircle()
    {
        
    }

    private void DrawIntoEllipse()
    {
        for (int _I = 0; _I < this.m_TargetEnemies.Count; _I++)
        {
            Vector2 _Direction = this.CalculateDirectionToEnemy(this.m_TargetEnemies.ElementAt(_I).Key);
            Vector2 _NormalizedDirection = _Direction.sqrMagnitude > 0.0001f ? _Direction.normalized : Vector2.zero;
            Vector2 _CameraWorldPosition = new Vector2(
                Camera.main.transform.position.x,
                Camera.main.transform.position.y);

            var _SemiMajorAxis = this.m_EllipseWidth / 2;
            var _SemiMinorAxis = this.m_EllipseHeight / 2;
            
            // Calculate the scale constraint from the semi-major and semi-minor axis
            float _Scale = (_SemiMajorAxis * _SemiMinorAxis) / Mathf.Sqrt(
                ((_SemiMinorAxis * _NormalizedDirection.x) * (_SemiMinorAxis * _NormalizedDirection.x)) +
                ((_SemiMajorAxis * _NormalizedDirection.y) * _SemiMajorAxis * _NormalizedDirection.y));

            this.m_View.UpdateMarker(
                this.m_TargetEnemies.ElementAt(_I).Key,
                _NormalizedDirection * _Scale + _CameraWorldPosition);
        }
    }

    private Vector2 CalculateDirectionToEnemy(int id)
    {
        Transform _EnemyTransform = this.m_TargetEnemies[id];
        // Vector2 _ScreenCenter =  RectTransformUtility.WorldToScreenPoint(
        //     Camera.main, 
        //     this.m_PlayerTransform.position);

        // Vector2 _CameraPosition = new Vector2(
        //     Camera.main.transform.position.x,
        //     Camera.main.transform.position.y);
        // Vector2 _EnemyScreenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, _EnemyTransform.position);

        Vector3 _ViewportCameraPosition = Camera.main.WorldToViewportPoint(Camera.main.transform.position);
        Vector3 _ViewportScreenPosition = Camera.main.WorldToViewportPoint(_EnemyTransform.position);
        
        _ViewportScreenPosition.x = Mathf.Clamp(_ViewportScreenPosition.x, 0, 1);
        _ViewportScreenPosition.y = Mathf.Clamp(_ViewportScreenPosition.y, 0, 1);

        Vector2 _CameraScreenPosition = new Vector2(
            // Screen.width / 2,
            // Screen.height / 2);
            _ViewportCameraPosition.x,
            _ViewportCameraPosition.y);
        
        Vector2 _EnemyScreenPosition = new Vector2(
            _ViewportScreenPosition.x,
            _ViewportScreenPosition.y);
        
        return _EnemyScreenPosition - _CameraScreenPosition;
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
            float angle = (i / (float)_Segments) * Mathf.PI * 2; // Full circle
            float x = (m_EllipseWidth / 2) * Mathf.Cos(angle);    // X position
            float y = (m_EllipseHeight / 2) * Mathf.Sin(angle);   // Y position

            Vector3 _NewPoint = transform.position + new Vector3(x, y, 0);

            if (i > 0)
                Gizmos.DrawLine(_PreviousPoint, _NewPoint); // Draw line segment

            _PreviousPoint = _NewPoint;
        }
        
        // Draw initial direction
        for (int _I = 0; _I < this.m_TargetEnemies.Count; _I++)
        {
            Vector2 _Direction = this.CalculateDirectionToEnemy(this.m_TargetEnemies.ElementAt(_I).Key);
            Vector2 _NormalizedDirection = _Direction.sqrMagnitude > 0.0001f ? _Direction.normalized : Vector2.zero;
            Vector3 _ViewportCameraPosition = Camera.main.WorldToViewportPoint(Camera.main.transform.position);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_ViewportCameraPosition, _Direction * 10);
            
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(_ViewportCameraPosition, _NormalizedDirection * 10);
            
            float _EllipseScaleConstraint = 0;
            if (_NormalizedDirection.sqrMagnitude > 0.0001f)
            {
                _EllipseScaleConstraint = Mathf.Sqrt(
                    1.0f / ((_NormalizedDirection.x * _NormalizedDirection.x) / (m_EllipseWidth * m_EllipseWidth / 4) +
                            (_NormalizedDirection.y * _NormalizedDirection.y) / (m_EllipseHeight * m_EllipseHeight / 4)));
            }
            
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_NormalizedDirection * Mathf.Min(_Direction.magnitude, _EllipseScaleConstraint), 2);
        }
    }

    #endregion Gizmos
  
}

public enum EnemyLocatorHUDShape
{
    Circle,
    Ellipse,
    HUD
}
