using System.Collections.Generic;
using UnityEngine;

public class EnemyLocatorHUDView : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    [SerializeField] private GameObject m_MarkerPrefab;
    [SerializeField] private GameObject m_CanvasGroup;
    [SerializeField, RequiredField] private LineShapeGenerator m_LineShapeGenerator;
    private readonly Dictionary<int, RectTransform> m_Markers = new();

    #endregion Fields
  
    #region - - - - - - Methods - - - - - -

    public void AddMarker(int id)
    {
        GameObject _MarkerInstance = Instantiate(this.m_MarkerPrefab, this.m_CanvasGroup.transform);
        _MarkerInstance.SetActive(true);
        this.m_Markers.Add(id, _MarkerInstance.GetComponent<RectTransform>());
    }

    public void RemoveMarker(int id)
    {
        GameObject _MarkerInstance = this.m_Markers[id]?.gameObject;
        this.m_Markers.Remove(id);
        
        Destroy(_MarkerInstance);
    }

    public void RenderHUDShape(Vector2 shapeDimensions, EnemyLocatorHUDShape selectedShape) 
        => this.m_LineShapeGenerator.DrawShape(shapeDimensions, EnemyLocatorHUDShapes.GetShape(selectedShape).Function);

    public void UpdateMarker(int id, Vector2 newScreenPosition) 
        => this.m_Markers[id].position = newScreenPosition;

    #endregion Methods
  
}
