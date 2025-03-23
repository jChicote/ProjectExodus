using System.Collections.Generic;
using UnityEngine;

public class EnemyLocatorHUDView : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    [SerializeField] private GameObject m_MarkerPrefab;

    [SerializeField] private GameObject m_CanvasGroup;
    private Dictionary<int, RectTransform> m_Markers = new();

    #endregion Fields
  
    #region - - - - - - Methods - - - - - -

    public void AddMarker(int id)
    {
        GameObject _MarkerInstance = Instantiate(this.m_MarkerPrefab, this.m_CanvasGroup.transform);
        _MarkerInstance.SetActive(true);
        this.m_Markers.Add(id, _MarkerInstance.GetComponent<RectTransform>());
    }

    public void UpdateMarker(int id, Vector2 newScreenPosition)
    {
        RectTransform _Marker = this.m_Markers[id];
        _Marker.position = newScreenPosition;
    }

    #endregion Methods
  
}
