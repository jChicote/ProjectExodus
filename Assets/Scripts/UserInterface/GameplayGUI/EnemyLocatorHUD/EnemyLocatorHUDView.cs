using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyLocatorHUDView : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    [SerializeField] private GameObject m_MarkerPrefab;

    [SerializeField] private GameObject m_CanvasGroup;
    private Dictionary<int, RectTransform> m_Markers = new();

    private RectTransform m_CanvasRect;
    
    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_CanvasRect = this.m_CanvasGroup.GetComponent<RectTransform>();
    }

    #endregion Unity Methods
  
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
        // Vector2 _AdjustedScreenPosition = new Vector2(
        //     newScreenPosition )

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            this.m_CanvasRect,
            newScreenPosition,
            null,
            out Vector2 _LocalPoint);
        
        _Marker.anchoredPosition = newScreenPosition;
        Debug.Log(_LocalPoint);
    }

    #endregion Methods
  
}
